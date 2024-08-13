using smartfinance.Domain.Enums;

namespace smartfinance.Domain.Models.AccountMovement.Create
{
    public class MovementCreateViewModel
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime MovementDate { get; set; }
        public MovementType Type { get; set; }
        public int CategoryId { get; set; }
    }
}
