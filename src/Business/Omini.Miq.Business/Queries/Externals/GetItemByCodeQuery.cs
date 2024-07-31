using FluentValidation.Results;
using Omini.Miq.Application.Abstractions.Messaging;
using Omini.Miq.Domain.Externals.Models;
using Omini.Miq.Domain.Externals.Repositories;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Business.Queries;

public class GetItemByCode : IQueryWithValidation<Item?>
{
    public string Code { get; set; }

    public class GetItemByCodeHandler : IQueryWithValidationHandler<GetItemByCode, Item?>
    {
        private readonly IItemRepository _itemRepository;
        public GetItemByCodeHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Result<Item?, ValidationResult>> Handle(GetItemByCode request, CancellationToken cancellationToken)
        {
            var validationFailures = new List<ValidationFailure>();
            var item = await _itemRepository.GetByCode(request.Code, cancellationToken);

            if (item is null)
            {
                validationFailures.Add(new ValidationFailure("Item", "Item não encontrado"));
            }

            if (validationFailures.Any())
            {
                return new ValidationResult(validationFailures);
            }

            return item;
        }
    }
}