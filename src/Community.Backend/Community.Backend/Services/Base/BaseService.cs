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
                        Result = Result.AddMessage("entity delete successfully");
                    }
                    else
                    {
                        Result = Result.AddErrorMessage("").AppendTaskResultData(result);
                         
                    }
                }
                return Result;
            }
            catch (Exception ex)
            {
                return Result.AddErrorMessage("Error deleting this entity from DB", ex);
            }
        }

        public virtual async Task<Result> Delete(Tmodel entity)
        {
            try
            {
                var result = await ValidateOnDelete(entity);
                if(result.ExecutedSuccesfully){
                    await Repository.Delete(entity);
                    await Repository.CommitChanges();
                    return Result.AddMessage("entity delete successfully");
                } else {
                    return Result.AddErrorMessage("").AppendTaskResultData(result);
                }
            }
            catch (Exception ex)
            {
                return Result.AddErrorMessage("Error deleting this entity from DB",ex);
            }
        }

        public virtual async Task<Result> DeleteRange(IEnumerable<Tmodel> entities)
        {
            try
            {
                var result = new Result();
                foreach (var entity in entities)
                {
                    result = await ValidateOnDelete(entity);
                    if(!result.ExecutedSuccesfully){
                        break;
                    }
                }
                if(result.ExecutedSuccesfully){
                    await Repository.DeleteRange(entities);
                    await Repository.CommitChanges();
                    return Result.AddMessage("Entittes deleted successfully !");
                } else {
                    return Result.AddErrorMessage("").AppendTaskResultData(result);
                }
            }
            catch (Exception ex)
            {
                return Result.AddErrorMessage("Error on delete the list of entities",ex);
            }
        }

        public virtual async Task<Result> Update(Tmodel entity)
        {
            try
            {
                var result = await ValidateOnUpdate(entity);
                if(result.ExecutedSuccesfully){
                    await Repository.Update(entity);
                    await Repository.CommitChanges();
                    Result = Result.AddMessage($"{entity.GetType().Name} entity update successfully !");
                } else {
                    Result = Result.AddErrorMessage($"").AppendTaskResultData(result);
                }
                return Result;
            }
            catch (Exception ex)
            {
                return Result.AddErrorMessage("",ex);
            }
        }

        public virtual async Task<Result> UpdateRange(IEnumerable<Tmodel> entities)
        {
            try
            {
                var result = new Result();
                foreach (var entity in entities)
                {
                    result = await ValidateOnUpdate(entity);
                    if(!result.ExecutedSuccesfully){
                        break;
                    }
                }
                if(!result.ExecutedSuccesfully){
                    Result = Result.AddErrorMessage($"error when try to eliminate {typeof(Tmodel).Name} entity list").AppendTaskResultData(result);
                } else {
                    await Repository.UpdateRange(entities);
                    await Repository.CommitChanges();
                    Result = Result.AddMessage($"list of {typeof(Tmodel).Name} entities update succefully !");
                }
                return Result;
            }
            catch (Exception ex)
            {
                return Result.AddErrorMessage($"error when try to update the list of {typeof(Tmodel)} entities in database",ex);
            }
        }

        public abstract Task<Result> ValidateOnCreate(Tmodel entity);

        public abstract Task<Result> ValidateOnDelete(Tmodel entity);

        public abstract Task<Result> ValidateOnUpdate(Tmodel entity);

        public abstract Task<Result> ValidateOnDelete(object id);
    }
}
