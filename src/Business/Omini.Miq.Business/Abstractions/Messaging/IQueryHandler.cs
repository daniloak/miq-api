using MediatR;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, PagedResult<TResponse>>
    where TQuery : IQuery<TResponse>
{

}