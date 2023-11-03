using TodoXpress.Domain.Common.Contracts;

namespace TodoXpress.Application.Contracts.Persistence;

public interface IReadableDataService<T> where T : IIdentifieable
{
    Task<T?> ReadSingleAsync(Guid id);
}
