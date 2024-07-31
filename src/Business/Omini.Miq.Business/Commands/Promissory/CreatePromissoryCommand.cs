using FluentValidation.Results;
using Omini.Miq.Application.Abstractions.Messaging;
using Omini.Miq.Domain.Repositories;
using Omini.Miq.Domain.Sales;
using Omini.Miq.Domain.Transactions;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Business.Commands;

public sealed record CreatePromissoryCommand : ICommand<Promissory>
{
    public float TotalAmount { get; set; }
    public string Status { get; set; }
    public string ExternalStatus { get; set; }
    public string Company { get; set; }

    internal sealed class CreatePromissoryCommandHandler : ICommandHandler<CreatePromissoryCommand, Promissory>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPromissoryRepository _promissoryRepository;
        public CreatePromissoryCommandHandler(IUnitOfWork unitOfWork, IPromissoryRepository promissoryRepository)
        {
            _unitOfWork = unitOfWork;
            _promissoryRepository = promissoryRepository;
        }

        public async Task<Result<Promissory, ValidationResult>> Handle(CreatePromissoryCommand request, CancellationToken cancellationToken)
        {
            // var hospital = new Promissory(
            //     name: new CompanyName(request.LegalName, request.TradeName),
            //     cnpj: request.Cnpj,
            //     comments: request.Comments
            // );

            // await _hospitalRepository.Add(hospital, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return new Promissory();
        }
    }
}