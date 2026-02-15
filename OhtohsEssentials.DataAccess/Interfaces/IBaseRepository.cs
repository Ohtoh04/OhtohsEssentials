namespace OhtohsEssentials.DataAccess.Interfaces;

internal interface IBaseRepository<TEntity>
    where TEntity : class
{
    Task<TEntity> AddAsync(TEntity model, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>?> GetAllAsync(CancellationToken cancellationToken);
}
