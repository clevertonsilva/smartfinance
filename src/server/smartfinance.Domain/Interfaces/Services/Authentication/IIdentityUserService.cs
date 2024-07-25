﻿using smartfinance.Domain.Common;
using smartfinance.Domain.Models.Authentication.Create;
using smartfinance.Domain.Models.Authentication.Model;

namespace smartfinance.Domain.Interfaces.Services.Authentication
{
    public interface IIdentityUserService
    {
        Task<OperationResult<bool>> Register(CreateIdentityUserViewModel model);
        Task<OperationResult<IdentityUserViewModel>> Login(LoginViewModel model);
        Task<OperationResult<bool>> ConfirmEmail(ConfirmEmailViewModel model);
        Task<OperationResult<IdentityUserViewModel>> LoginWithouPassword(string userId);

    }
}