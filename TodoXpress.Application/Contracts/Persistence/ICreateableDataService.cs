using TodoXpress.Domain.Common.Contracts;

namespace TodoXpress.Application.Contracts.Persistence;

public interface ICreateableDataService<T> where T : IIdentifieable
{
    Task<Guid> CreateAsync(T entity);
}
