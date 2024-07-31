using FluentValidation.Results;
using Omini.Miq.Application.Abstractions.Messaging;
using Omini.Miq.Domain.Repositories;
using Omini.Miq.Domain.Sales;
using Omini.Miq.Domain.Transactions;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Business.Commands;

public sealed record CreatePromissoryCommand : ICommand<Promissory>
{
    public List<CreatePromissoryItem> Items { get; set; } = new();

    public class CreatePromissoryItem
    {
        public int? LineOrder { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
    }

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
            var promissory = new Promissory();

            foreach (var item in request.Items)
            {
                promissory.AddItem(item.ItemCode, item.ItemName, item.Quantity, item.UnitPrice);
            }

            await _promissoryRepository.Create(promissory, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return promissory;
        }
    }
}