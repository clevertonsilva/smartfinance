using smartfinance.Domain.Interfaces.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartfinance.Infra.Identity.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string Name => throw new NotImplementedException();

        public bool IsAuthenticated => throw new NotImplementedException();
    }
}
