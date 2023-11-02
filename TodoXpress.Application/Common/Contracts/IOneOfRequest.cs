using MediatR;
using OneOf;
using TodoXpress.Domain;

namespace TodoXpress.Application;

/// <summary>
/// Defines a request for a mediatR handler.
/// </summary>
/// <typeparam name="TResponse">The type of the successfull response from the handler.</typeparam>
public interface IOneOfRequest<TResponse> 
    : IRequest<OneOf<TResponse, IError>>
{

}
