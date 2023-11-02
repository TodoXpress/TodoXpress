using MediatR;
using OneOf;
using TodoXpress.Domain;

namespace TodoXpress.Application;

/// <summary>
/// Defines a handler of a mediatR request.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the successfull response.</typeparam>
public interface IOneOfRequestHandler<TRequest, TResponse> 
    : IRequestHandler<TRequest, OneOf<TResponse, IError>>
    where TRequest : IRequest<OneOf<TResponse, IError>>
{

}
