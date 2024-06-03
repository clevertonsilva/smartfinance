using smartfinance.Domain.Common;
using smartfinance.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartfinance.Domain.Interfaces.Services.Authentication
{
    public interface IIdentityService
    {

        Task<OperationResult<bool>> CreateUser(CreateIdentityUserViewModel request);
        Task<OperationResult<bool>> Login(CreateIdentityUserViewModel request);
    }
}
