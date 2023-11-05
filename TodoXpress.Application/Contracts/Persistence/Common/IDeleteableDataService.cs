using TodoXpress.Domain.Common.Contracts;

namespace TodoXpress.Application.Contracts.Persistence.Common;

public interface IDeleteableDataService<T> where T : IIdentifieable
{
    Task<Guid> DeleteAsync(T entity);

    Task<Guid> DeleteAsync(Guid id);
}
