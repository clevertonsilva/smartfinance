using AutoMapper;
using FluentValidation;
using smartfinance.Application.Interfaces;
using smartfinance.Domain.Common;
using smartfinance.Domain.Common.Extensions;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Domain.Interfaces.Services.Authentication;
using smartfinance.Domain.Models.Account.Create;
using smartfinance.Domain.Models.Account.Model;
using smartfinance.Domain.Models.Account.Update;
using smartfinance.Domain.Models.Authentication.Create;
using smartfinance.Domain.Models.Authentication.Model;

namespace smartfinance.Application.Apps
{
    public class AccountApp : IAccountApp
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IIdentityUserService _identityUserService;
        private readonly IValidator<Account> _accountValidator;
        private readonly IMapper _mapper;

        public AccountApp(IAccountRepository accountRepository,
            IIdentityUserService identityUserService,
            IValidator<Account> accountValidator,
            IMapper mapper
            )
        {
            _accountRepository = accountRepository;
            _accountValidator = accountValidator;
            _mapper = mapper;
            _identityUserService = identityUserService;
        }

        public async Task<OperationResult<bool>> ActiveAsync(int id, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.FindByIdAsync(id, cancellationToken);

            if (account == null)
            {
                return null;
            }

            account.IsActive = true;

            await _accountRepository.UpdateAsync(account);
            await _accountRepository.UnitOfWork.Commit();

            return OperationResult<bool>.Succeeded();
        }

        public async Task<OperationResult<bool>> ConfirmEmailAsync(ConfirmEmailViewModel model)
        {
            var identityResult = await _identityUserService.ConfirmEmail(model);

            if (!identityResult.Success)
            {
                return OperationResult<bool>
                     .Failed(identityResult.Errors, identityResult.Message);
            }

            return OperationResult<bool>.Succeeded();
        }

        public async Task<OperationResult<int>> CreateAsync(AccountCreateViewModel model, CancellationToken cancellationToken = default)
        {
            var account = _mapper.Map<Account>(model);

            var validateResult = await _accountValidator.ValidateAsync(account, cancellationToken);

            if (!validateResult.IsValid)
            {
                return OperationResult<int>.Failed(validateResult.AsReponseErrors());
            }

             var validateIdentityUserResult = await _identityUserService.ValidateUserAsync(_mapper.Map<AppIdentityUserViewModel>(model));

            if (!validateIdentityUserResult.Success)
            {
                return OperationResult<int>
                        .Failed(validateIdentityUserResult.Errors);
            }

            await _accountRepository.AddAsync(account, cancellationToken);
           
            await _accountRepository.UnitOfWork.Commit();

            var identityModel = _mapper.Map<CreateIdentityUserViewModel>(model);

            var identityResult = await _identityUserService.Register(identityModel);

            if (!identityResult.Success)
            {
                return OperationResult<int>
                        .Failed(identityResult.Errors, identityResult.Message);
            }

            return OperationResult<int>.Succeeded(account.Id);
        }

        public async Task<OperationResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.FindByIdAsync(id, cancellationToken);

            if (account == null)
            {
                return null;
            }

            var result = await _accountValidator.ValidateAsync(account, cancellationToken);

            if (!result.IsValid)
            {
                return OperationResult<bool>.Failed(result.AsReponseErrors());
            }

            account.IsDeleted = true;

            await _accountRepository.UpdateAsync(account);
            await _accountRepository.UnitOfWork.Commit();

            return OperationResult<bool>.Succeeded();
        }

        public async Task<OperationResult<AccountViewModel>> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.FindByIdAsync(id, cancellationToken);

            if (account == null)
            {
                return null;
            }

            var viewModel = _mapper.Map<AccountViewModel>(account);

            return OperationResult<AccountViewModel>.Succeeded(viewModel);
        }

        public async Task<OperationResult<IdentityUserViewModel>> Login(LoginViewModel model)
        {
            var identityResult = await _identityUserService.Login(model);
          
            if (!identityResult.Success)
            {
                return OperationResult<IdentityUserViewModel>
                     .Failed(identityResult.Errors, identityResult.Message);
            }

            return OperationResult<IdentityUserViewModel>.Succeeded(identityResult.Model);
        }

        public async Task<OperationResult<bool>> UpdateAsync(int id, AccountUpdateViewModel model, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.FindByIdAsync(id, cancellationToken);

            if (account == null)
            {
                return null;
            }

            account = _mapper.Map<Account>(model);

            var result = await _accountValidator.ValidateAsync(account, cancellationToken);

            if (!result.IsValid)
            {
                return OperationResult<bool>.Failed(result.AsReponseErrors());
            }

            account.IsDeleted = true;

            await _accountRepository.UpdateAsync(account);
            await _accountRepository.UnitOfWork.Commit();

            return OperationResult<bool>.Succeeded();
        }
    }
}
