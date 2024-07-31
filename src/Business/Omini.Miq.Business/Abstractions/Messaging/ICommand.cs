using FluentValidation.Results;
using MediatR;
using Omini.Miq.Shared.Entities;


namespace Omini.Miq.Application.Abstractions.Messaging;

public interface ICommand<TResponse> : IRequest<Result<TResponse, ValidationResult>>{

}