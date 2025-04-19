using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace StudyTaskManager.Persistence.Repository.Generic
{
    public abstract class TRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region GetFromDBAsync

        #region получение одной сущности
        #region TBaseEntityWithID
        /// <summary>
        /// Получение сущности по Id (только для BaseEntityWithID)
        /// </summary>
        private async Task<TBaseEntityWithID?> GetFromDBAsync<TBaseEntityWithID>(
            Guid id,
            CancellationToken cancellationToken
            ) where TBaseEntityWithID : BaseEntityWithID =>
                await _dbContext
                    .Set<TBaseEntityWithID>()
                    .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        /// <summary>
        /// Получение результата поиска по Id с учетом ошибки (только для BaseEntityWithID)
        /// </summary>
        protected async Task<Result<TBaseEntityWithID>> GetFromDBAsync<TBaseEntityWithID>(
            Guid id,
            Error IdEmpty,
            Error NotFound,
            CancellationToken cancellationToken
            ) where TBaseEntityWithID : BaseEntityWithID
        {
            if (id == Guid.Empty) return Result.Failure<TBaseEntityWithID>(IdEmpty);

            TBaseEntityWithID? entity = await GetFromDBAsync<TBaseEntityWithID>(id, cancellationToken);
            if (entity == null) return Result.Failure<TBaseEntityWithID>(NotFound);

            return Result.Success(entity);
        }
        #endregion

        #region TBaseEntity
        /// <summary>
        /// Получение сущности любого типа по предикату
        /// </summary>
        private async Task<TBaseEntity?> GetFromDBAsync<TBaseEntity>(
            Expression<Func<TBaseEntity, bool>> predicate,
            CancellationToken cancellationToken
            ) where TBaseEntity : BaseEntity =>
                await _dbContext
                    .Set<TBaseEntity>()
                    .FirstOrDefaultAsync(predicate, cancellationToken);
        /// <summary>
        /// Получение сущности любого типа по предикату с учетом ошибки
        /// </summary>
        /// <param name="NotFound">Ошибка, в случае в базе данных не будет найдена сущность</param>
        /// <returns></returns>
        protected async Task<Result<TBaseEntity>> GetFromDBAsync<TBaseEntity>(
            Expression<Func<TBaseEntity, bool>> predicate,
            Error NotFound,
            CancellationToken cancellationToken
            ) where TBaseEntity : BaseEntity
        {
            TBaseEntity? entity = await GetFromDBAsync(predicate, cancellationToken);
            if (entity == null) return Result.Failure<TBaseEntity>(NotFound);

            return Result.Success(entity);
        }
        #endregion

        #region сущности типа репозитория
        /// <summary>
        /// Получение сущности типа репозитория по предикату
        /// </summary>
        private async Task<T?> GetFromDBAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken
            ) => await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        /// <summary>
        /// Получение сущности типа репозитория по предикату с учетом ошибки
        /// </summary>
        protected async Task<Result<T>> GetFromDBAsync(
            Expression<Func<T, bool>> predicate,
            Error NotFound,
            CancellationToken cancellationToken
            )
        {
            T? entity = await GetFromDBAsync(predicate, cancellationToken);
            if (entity == null) return Result.Failure<T>(NotFound);

            return Result.Success(entity);
        }
        #endregion
        #endregion

        #region Получение списка
        #region Любого типа
        /// <summary>
        /// Получение сущностей любого типа в количестве <paramref name="count"/> начиная от <paramref name="startIndex"/> 
        /// </summary>
        protected async Task<Result<List<TBaseEntity>>> GetFromDBWhereAsync<TBaseEntity>(
            int startIndex,
            int count,
            CancellationToken cancellationToken) where TBaseEntity : BaseEntity
        {
            if (count < 1) return Result.Failure<List<TBaseEntity>>(PersistenceErrors.IncorrectCount);
            if (startIndex < 0) return Result.Failure<List<TBaseEntity>>(PersistenceErrors.IncorrectStartIndex);

            var result = await _dbContext
                .Set<TBaseEntity>()
                .AsNoTracking()
                .Skip(startIndex)
                .Take(count)
                .ToListAsync(cancellationToken);

            return Result.Success(result);
        }
        /// <summary>
        /// Получение сущностей любого типа в количестве <paramref name="count"/> начиная от <paramref name="startIndex"/> по предикату <paramref name="predicate"/>
        /// </summary>
        protected async Task<Result<List<TBaseEntity>>> GetFromDBWhereAsync<TBaseEntity>(
            int startIndex,
            int count,
            Expression<Func<TBaseEntity, bool>> predicate,
            CancellationToken cancellationToken) where TBaseEntity : BaseEntity
        {
            if (count < 1) return Result.Failure<List<TBaseEntity>>(PersistenceErrors.IncorrectCount);
            if (startIndex < 0) return Result.Failure<List<TBaseEntity>>(PersistenceErrors.IncorrectStartIndex);

            var result = await _dbContext
                .Set<TBaseEntity>()
                .AsNoTracking()
                .Where(predicate)
                .Skip(startIndex)
                .Take(count)
                .ToListAsync(cancellationToken);

            return Result.Success(result);
        }
        #endregion

        #region типа репозитория
        /// <summary>
        /// Получение сущностей в количестве <paramref name="count"/> начиная от <paramref name="startIndex"/> 
        /// </summary>
        protected async Task<Result<List<T>>> GetFromDBWhereAsync(
            int startIndex,
            int count,
            CancellationToken cancellationToken)
        {
            if (count < 1) return Result.Failure<List<T>>(PersistenceErrors.IncorrectCount);
            if (startIndex < 0) return Result.Failure<List<T>>(PersistenceErrors.IncorrectStartIndex);

            var result = await _dbSet
                .AsNoTracking()
                .Skip(startIndex)
                .Take(count)
                .ToListAsync(cancellationToken);

            return Result.Success(result);
        }
        /// <summary>
        /// Получение сущностей в количестве <paramref name="count"/> начиная от <paramref name="startIndex"/> по предикату <paramref name="predicate"/>
        /// </summary>
        protected async Task<Result<List<T>>> GetFromDBWhereAsync(
            int startIndex,
            int count,
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken)
        {
            if (count < 1) return Result.Failure<List<T>>(PersistenceErrors.IncorrectCount);
            if (startIndex < 0) return Result.Failure<List<T>>(PersistenceErrors.IncorrectStartIndex);

            var result = await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .Skip(startIndex)
                .Take(count)
                .ToListAsync(cancellationToken);

            return Result.Success(result);
        }
        #endregion
        #endregion
        #endregion

        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public TRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        protected abstract Task<Result> VerificationBeforeAddingAsync(T entity, CancellationToken cancellationToken);
        protected abstract Task<Result> VerificationBeforeUpdateAsync(T entity, CancellationToken cancellationToken);
        protected abstract Task<Result> VerificationBeforeRemoveAsync(T entity, CancellationToken cancellationToken);

        #region Добавление изменение удаление реализация
        public virtual async Task<Result> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            Result result = await VerificationBeforeAddingAsync(entity, cancellationToken);
            if (result.IsFailure) return result;

            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            Result result = await VerificationBeforeUpdateAsync(entity, cancellationToken);
            if (result.IsFailure) return result;

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public virtual async Task<Result> RemoveAsync(T entity, CancellationToken cancellationToken = default)
        {
            Result result = await VerificationBeforeRemoveAsync(entity, cancellationToken);
            if (result.IsFailure) return result;

            return await RemoveWithoutVerificationAsync(entity, cancellationToken);
        }
        #endregion

        protected async Task<Result> RemoveWithoutVerificationAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        #region GetAllAsync реализация
        public async Task<Result<List<T>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Result<List<T>>> GetAllAsync(
            int startIndex,
            int count,
            CancellationToken cancellationToken = default)
        {
            return await GetFromDBWhereAsync(startIndex, count, cancellationToken);
        }

        public async Task<Result<List<T>>> GetAllAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken); ;
        }

        public async Task<Result<List<T>>> GetAllAsync(
            int startIndex,
            int count,
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await GetFromDBWhereAsync(startIndex, count, predicate, cancellationToken);
        }
        #endregion
    }
}
