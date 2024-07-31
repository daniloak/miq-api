using FluentValidation.Results;
using MediatR;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse, ValidationResult>>
    where TCommand : ICommand<TResponse>
{

}