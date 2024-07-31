using FluentValidation.Results;
using Omini.Miq.Application.Abstractions.Messaging;
using Omini.Miq.Domain;
using Omini.Miq.Domain.Authentication;
using Omini.Miq.Domain.Externals.Repositories;
using Omini.Miq.Domain.Repositories;
using Omini.Miq.Domain.Sales;
using Omini.Miq.Domain.Transactions;
using Omini.Miq.Shared.Entities;
using Omini.Miq.Shared.Formatters;

namespace Omini.Miq.Business.Commands;

public sealed record CreateUserCommand : ICommand<User>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Company { get; set; }
    public string Password { get; set; }
    public string TaxId { get; set; }
    public string ExternalId { get; set; }
    public Role Role { get; set; }

    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IAuthenticationService authenticationService, ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<User, ValidationResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationFailures = new List<ValidationFailure>();
            var taxIdNormalized = request.TaxId.GetDigits();
            var company = await _companyRepository.GetByTaxId(taxIdNormalized, cancellationToken);

            if (company is null)
            {
                validationFailures.Add(new ValidationFailure("TaxId", "Usuário não ativo"));
            }

            if (validationFailures.Any())
            {
                return new ValidationResult(validationFailures);
            }

            var externalId = await _authenticationService.Register(request.Email, request.Password);

            var user = new User(request.Email, request.Password, externalId);

            await _userRepository.Create(user);

            await _unitOfWork.Commit(cancellationToken);

            return user;
        }
    }
}