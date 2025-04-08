using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using System.Linq.Expressions;

namespace StudyTaskManager.Persistence.Repository.Generic
{
    public abstract class TWithIdRepository<T> : TRepository<T>, IRepositoryWithID<T> where T : BaseEntityWithID
    {

        #region GetFromDBAsync сущности типа репозитория
        ///// <summary>
        ///// Получение сущности типа репозитория по Id
        ///// </summary>
        //private async Task<T?> GetFromDBAsync(
        //    Guid id,
        //    CancellationToken cancellationToken
        //    ) => await GetFromDBAsync<T>(id, cancellationToken);
        /// <summary>
        /// Получение сущности типа репозитория по Id с учетом ошибки
        /// </summary>
        protected async Task<Result<T>> GetFromDBAsync(
            Guid id,
            CancellationToken cancellationToken
            ) => await GetFromDBAsync<T>(
                id,
                GetErrorIdEmpty(),
                GetErrorNotFound(),
                cancellationToken);
        #endregion

        public TWithIdRepository(AppDbContext dbContext) : base(dbContext) { }

        protected abstract Error GetErrorNotFound();
        protected abstract Error GetErrorIdEmpty();

        public async Task<Result<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Error idEmpty = GetErrorIdEmpty();
            Error notFound = GetErrorNotFound();
            Result<T> entity = await GetFromDBAsync<T>(id, idEmpty, notFound, cancellationToken);
            if (entity.IsFailure) { return Result.Failure<T>(entity.Error); }
            return Result.Success(entity.Value);
        }

        public async Task<Result> RemoveAsync(Guid entityId, CancellationToken cancellationToken = default)
        {
            Error idEmpty = GetErrorIdEmpty();
            Error notFound = GetErrorNotFound();
            Result<T> entity = await GetFromDBAsync<T>(entityId, idEmpty, notFound, cancellationToken);
            if (entity.IsFailure) { return entity; }
            return await RemoveWithoutVerificationAsync(entity.Value, cancellationToken);
        }

        protected override async Task<Result> VerificationBeforeUpdateAsync(T entity, CancellationToken cancellationToken)
        {
            Error idEmpty = GetErrorIdEmpty();
            Error notFound = GetErrorNotFound();
            Result<T> result = await GetFromDBAsync<T>(entity.Id, idEmpty, notFound, cancellationToken);
            return result;
        }
        protected override async Task<Result> VerificationBeforeRemoveAsync(T entity, CancellationToken cancellationToken)
        {
            Error idEmpty = GetErrorIdEmpty();
            Error notFound = GetErrorNotFound();
            Result<T> result = await GetFromDBAsync<T>(entity.Id, idEmpty, notFound, cancellationToken);
            return result;
        }
    }
}
