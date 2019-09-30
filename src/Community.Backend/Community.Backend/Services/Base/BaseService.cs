using Community.Backend.Database.Repositories.Base;
using Community.Backend.Database.Repositories.Constructor;
using Community.Backend.Services.Infraestructure;
using Comunity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Backend.Services.Base
{
    public interface IBaseService<Tmodel> where Tmodel : class, IBaseModel
    {
        Task<Result> ValidateOnCreate(Tmodel entity);
        Task<Result> ValidateOnUpdate(Tmodel entity);
        Task<Result> ValidateOnDelete(Tmodel entity);
        Task<Result> ValidateOnDelete(object id);

        Task<Result> Update(Tmodel entity);
        Task<Result> UpdateRange(IEnumerable<Tmodel> entities);
        Task<Result> Create(Tmodel entity);
        Task<Result> CreateRange(IEnumerable<Tmodel> entities);
        Task<Result> Delete(object id);
        Task<Result> Delete(Tmodel entity);
        Task<Result> DeleteRange(IEnumerable<Tmodel> entities);

        Task<TResult> AdvanceQuery<TResult>(TResult result,Func<IRepositoryConstructor,Task<TResult>> operation) where TResult : class;
    }
    public abstract class BaseService<Tmodel> : IBaseService<Tmodel> where Tmodel : class, IBaseModel
    {
        protected Result Result { get; set; }
        protected readonly IRepositoryConstructor Constructor;
        protected IBaseRepository<Tmodel> Repository { get => Constructor.GetRepository<Tmodel>(); }

        public BaseService(IRepositoryConstructor constructor)
        {
            Result = new Result();
            Constructor = constructor;
        }

        public virtual async Task<TResult> AdvanceQuery<TResult>(TResult result, Func<IRepositoryConstructor, Task<TResult>> operation) where TResult : class => await operation(Constructor);

        public virtual async Task<Result> Create(Tmodel entity)
        {
            try
            {
                if ((await ValidateOnCreate(entity)).ExecutedSuccesfully)
                {
                    await Repository.Insert(entity);
                    await Repository.CommitChanges();
                    return Result.AddMessage($"{typeof(Tmodel).Name} inserted successfully !");
                } else
                {
                    return Result.AddErrorMessage("Fail ");
                }
            }
            catch (Exception ex)
            {
                Result.AddErrorMessage("", ex);
                return Result;
            }
        }

        public virtual async Task<Result> CreateRange(IEnumerable<Tmodel> entities)
        {
            var errorFounds = 0;
            try
            {
                foreach(var model in entities)
                {
                    if ((await ValidateOnCreate(model)).ExecutedSuccesfully)
                    {
                        errorFounds++;
                    }
                }

                if (errorFounds > 0)
                {
                    return Result.AddErrorMessage($"Error in data for insert: {errorFounds}");
                } else
                {
                    await Repository.InsertRange(entities);
                    await Repository.CommitChanges();
                    return Result.AddMessage("models save successfully");
                }
            }
            catch (Exception ex)
            {
                return Result.AddErrorMessage("Error creating entities in DB", ex);
            }
        }

        public virtual async Task<Result> Delete(object id)
        {
            try
            {
                var entity = await Repository.GetById(id);
                if (entity != null)
                {
                    var result = await ValidateOnDelete(entity);
                    if (result.ExecutedSuccesfully)
                    {
                        await Repository.Delete(id);
                        await Repository.CommitChanges();
                        return Result.AddMessage("entity delete successfully");
                    }
                    else
                    {
                        return Result.AddErrorMessage("").AppendTaskResultData(result);
                    }
                }
            }
            catch (Exception ex)
            {
                return Result.AddErrorMessage("Error deleting this entity from DB", ex);
            }
        }

        public virtual async Task<Result> Delete(Tmodel entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Result> DeleteRange(IEnumerable<Tmodel> entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Result> Update(Tmodel entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Result> UpdateRange(IEnumerable<Tmodel> entities)
        {
            throw new NotImplementedException();
        }

        public abstract Task<Result> ValidateOnCreate(Tmodel entity);

        public abstract Task<Result> ValidateOnDelete(Tmodel entity);

        public abstract Task<Result> ValidateOnUpdate(Tmodel entity);

        public abstract Task<Result> ValidateOnDelete(object id);
    }
}
