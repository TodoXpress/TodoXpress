using TodoXpress.Domain.Common.Contracts;

namespace TodoXpress.Application.Contracts.Persistence.Common;

public interface IUpdateableDataService<T> where T : IIdentifieable
{
    Task<Guid> UpdateAsync(T entity);
}
