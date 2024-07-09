using smartfinance.Domain.Models.Authentication.Create;

namespace smartfinance.Domain.Models.Account.Create
{
    public class AccountCreateViewModel : CreateIdentityUserViewModel
    {
        public string LastName { get; set; }
    }
}
