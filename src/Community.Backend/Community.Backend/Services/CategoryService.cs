using Community.Backend.Database.Repositories.Constructor;
using Community.Backend.Services.Base;
using Community.Backend.Services.Infraestructure;
using Comunity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace Community.Backend.Services
{
    public interface ICategoryService : IBaseService<Category>
    {

    }

    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IRepositoryConstructor constructor) : base(constructor)
        {
        }

        public override async Task<Result> ValidateOnCreate(Category entity)
        {
            var result = await Repository.Get(c => c.Name == entity.Name);
            if (result.Any())
            {
                return Result.AddErrorMessage("Data exist in database");
            }
            else
            {
                return Result;
            }
        }

        public override Task<Result> ValidateOnDelete(Category entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Result> ValidateOnUpdate(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
