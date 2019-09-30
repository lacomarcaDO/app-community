using Community.Backend.Database.Repositories.Base;
using Comunity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Community.Backend.Database.Repositories.Constructor
{
    public interface IRepositoryConstructor
    {
        IBaseRepository<Tmodel> GetRepository<Tmodel>() where Tmodel : class, IBaseModel;
        TRepository GetRepositoryImplementation<TRepository, Tmodel>() where TRepository : IBaseRepository<Tmodel> where Tmodel : class, IBaseModel;
    }

    public class RepositoryConstructor : IRepositoryConstructor
    {
        private readonly DatabaseContext Context;
        public RepositoryConstructor(DatabaseContext context)
        {
            Context = context;
        }
        public IBaseRepository<Tmodel> GetRepository<Tmodel>() where Tmodel : class, IBaseModel => new BaseRepository<Tmodel>(Context);

        public TRepository GetRepositoryImplementation<TRepository, Tmodel>() where TRepository : IBaseRepository<Tmodel> where Tmodel : class, IBaseModel
        {
            Type tConcreteRepository = null;
            Assembly.GetExecutingAssembly().GetTypes().ToList().ForEach(t =>
            {
                if (typeof(TRepository).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                {
                    tConcreteRepository = t;
                }
            });
            return (TRepository)Activator.CreateInstance(tConcreteRepository,Context);
        }
    }
}
