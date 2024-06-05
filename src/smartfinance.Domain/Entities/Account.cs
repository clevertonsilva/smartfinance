using smartfinance.Domain.Entities.Shared;

namespace smartfinance.Domain.Entities
{
    public class Account : EntityBase
    {
        public string Name { get; set; }
        AccountMovement Account_Movement = new AccountMovement();
    }
}
