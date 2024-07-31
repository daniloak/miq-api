using FluentValidation.Results;
using Omini.Miq.Application.Abstractions.Messaging;
using Omini.Miq.Domain.Repositories;
using Omini.Miq.Domain.Sales;
using Omini.Miq.Domain.Transactions;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Business.Commands;

public sealed record CancelPromissoryCommand : ICommand<Promissory>
{
    public long Id { get; set; }

    internal sealed class CancelPromissoryCommandHandler : ICommandHandler<CancelPromissoryCommand, Promissory>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPromissoryRepository _promissoryRepository;
        public CancelPromissoryCommandHandler(IUnitOfWork unitOfWork, IPromissoryRepository promissoryRepository)
        {
            _unitOfWork = unitOfWork;
            _promissoryRepository = promissoryRepository;
        }

        public async Task<Result<Promissory, ValidationResult>> Handle(CancelPromissoryCommand request, CancellationToken cancellationToken)
        {
            var validationFailures = new List<ValidationFailure>();
            var promissory = await _promissoryRepository.GetById(request.Id, cancellationToken);

            if (promissory is null)
            {
                validationFailures.Add(new ValidationFailure("Promissory", $"{request.Id} not found"));
            }

            if (validationFailures.Any())
            {
                return new ValidationResult(validationFailures);
            }

            promissory.Cancel();

            _promissoryRepository.Update(promissory, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return promissory;
        }
    }
}