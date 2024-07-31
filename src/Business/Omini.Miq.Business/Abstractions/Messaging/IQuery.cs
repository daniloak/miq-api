using MediatR;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Application.Abstractions.Messaging;


public interface IQuery<TResponse> : IRequest<PagedResult<TResponse>>{

}