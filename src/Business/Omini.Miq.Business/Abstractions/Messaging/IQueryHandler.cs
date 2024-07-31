using FluentValidation.Results;
using MediatR;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, PagedResult<TResponse>>
    where TQuery : IQuery<TResponse>
{

}

public interface IQueryWithValidationHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse, ValidationResult>>
    where TQuery : IQueryWithValidation<TResponse>
{

}