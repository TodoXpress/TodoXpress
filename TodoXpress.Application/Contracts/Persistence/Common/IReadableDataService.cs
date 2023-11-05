using TodoXpress.Domain.Common.Contracts;

namespace TodoXpress.Application.Contracts.Persistence.Common;

public interface IReadableDataService<T> where T : IIdentifieable
{
    Task<T?> ReadSingleAsync(Guid id);
}
