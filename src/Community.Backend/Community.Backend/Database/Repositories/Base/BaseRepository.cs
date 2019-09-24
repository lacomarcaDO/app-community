using Comunity.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LinqKit;

namespace Community.Backend.Database.Repositories.Base
{
    public interface IBaseRepository<Tmodel> where Tmodel :class,IBaseModel
    {
        Task<int> CommitChanges();
        Task<Tmodel> GetById(object id);
        Task<IQueryable<Tmodel>> Get(Expression<Func<Tmodel, bool>> where, string includeProperties = "");
        Task<IQueryable<Tmodel>> Get(Expression<Func<Tmodel, bool>> where, params Expression<Func<Tmodel, object>>[] include);
        Task<IQueryable<Tmodel>> Get(params Expression<Func<Tmodel, object>>[] include);
        Task<IQueryable<Tmodel>> GetAll();
        Task<int> Count();
        Task<Tmodel> Insert(Tmodel entity);
        Task<Tmodel> Update(Tmodel entity);
        Task<Tmodel> Update(Tmodel entity, object id);
        Task UpdateProperty<Type>(Expression<Func<Tmodel, Type>> property, Tmodel entity);
        Task Delete(Tmodel Entity);
        Task Delete(object id);
        Task Delete(Expression<Func<Tmodel, bool>> primaryKeys);
        Task<SqlDataReader> Run(string query);
        Task DeleteRange(IEnumerable<Tmodel> entity);
        Task<IEnumerable<Tmodel>> InsertRange(IEnumerable<Tmodel> entity);
    }
    public class BaseRepository<Tmodel>:IBaseRepository<Tmodel> where Tmodel:class,IBaseModel
    {
        protected readonly DatabaseContext Context;

        public BaseRepository(DatabaseContext context)
        {
            Context = context;
        }

        public async Task<int> CommitChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public virtual async Task<Tmodel> GetById(object id)
        {
            return await Context.Set<Tmodel>().FindAsync(id);
        }

        public virtual async Task<IQueryable<Tmodel>> Get(Expression<Func<Tmodel, bool>> where, string includeProperties = "")
        {
            return await Task.Run(()=> {
                var query = Context.Set<Tmodel>().AsQueryable();

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (where != null)
                    query = query.AsExpandable().Where(where);

                return query;
            });
        }

        public virtual async Task<IQueryable<Tmodel>> Get(Expression<Func<Tmodel, bool>> @where, params Expression<Func<Tmodel, object>>[] include)
        {
            return await Task.Run(() =>
            {
                var query = Context.Set<Tmodel>().AsQueryable();

                foreach (var includeProperty in include)
                {
                    query = query.Include(includeProperty);
                }

                if (where != null)
                    query = query.AsExpandable().Where(where);

                return query;
            });
        }

        public virtual async Task<IQueryable<Tmodel>> Get(params Expression<Func<Tmodel, object>>[] include)
        {
            return await Task.Run(() =>
            {
                var query = Context.Set<Tmodel>().AsQueryable().AsExpandable();

                foreach (var includeProperty in include)
                {
                    query = query.Include(includeProperty);
                }

                return query;
            });
        }

        public virtual async Task<IQueryable<Tmodel>> GetAll()
        {
            return await Task.Run(() => Context.Set<Tmodel>().AsQueryable());
        }

        public virtual async Task<int> Count()
        {
            return await Task.Run(()=> Context.Set<Tmodel>().Count());
        }

        public virtual async Task<Tmodel> Insert(Tmodel entity)
        {
            await Context.Set<Tmodel>().AddAsync(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<Tmodel>> InsertRange(IEnumerable<Tmodel> entity)
        {
            await Context.Set<Tmodel>().AddRangeAsync(entity);
            return entity;
        }

        public virtual async Task<Tmodel> Update(Tmodel entity)
        {
            return await Task.Run(() =>
            {
                Context.Set<Tmodel>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;

                return entity;
            });
        }

        public virtual async Task<Tmodel> Update(Tmodel entity, object id)
        {
            var entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                var attachedEntity = await GetById(id);

                if (attachedEntity != null)
                {
                    var attachedEntry = Context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
            return entity;
        }

        public virtual async Task UpdateProperty<Type>(Expression<Func<Tmodel, Type>> property, Tmodel entity)
        {
            Context.Set<Tmodel>().Attach(entity);
            Context.Entry(entity).Property(property).IsModified = true;
            await Context.SaveChangesAsync();
        }

        public virtual async Task Delete(Tmodel entity)
        {
            await Task.Run(() => Context.Set<Tmodel>().Remove(entity));
        }

        public virtual async Task DeleteRange(IEnumerable<Tmodel> entity)
        {
            await Task.Run(()=> Context.Set<Tmodel>().RemoveRange(entity));
        }


        public virtual async Task Delete(object id)
        {
            var entity = await GetById(id);
            await Delete(entity);
        }

        public virtual async Task Delete(Expression<Func<Tmodel, bool>> primaryKeys)
        {
            var entity = (await Get(primaryKeys)).FirstOrDefault();
            await Delete(entity);
        }

        public virtual async Task<SqlDataReader> Run(string query)
        {
            var connection = Context.Database.GetDbConnection();

            SqlConnection conn = new SqlConnection(connection.ConnectionString);

            SqlCommand command = new SqlCommand(query, conn);
            await conn.OpenAsync();

            return await command.ExecuteReaderAsync();
        }
    }
}
