using FluentValidation.Results;
using MediatR;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Application.Abstractions.Messaging;


public interface IQuery<TResponse> : IRequest<PagedResult<TResponse>>
{

}

public interface IQueryWithValidation<TResponse> : IRequest<Result<TResponse, ValidationResult>>
{

}