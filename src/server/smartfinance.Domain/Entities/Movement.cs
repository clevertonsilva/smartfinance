using smartfinance.Domain.Entities.Shared;
using smartfinance.Domain.Enums;

namespace smartfinance.Domain.Entities
{
    public class Movement : EntityBase
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime MovementDate { get; set; }
        public MovementType Type { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        private void AccountMovement(int amount, string description, int categoryId, MovementType type, DateTime movementDate)
        {
            Amount = amount;
            Description = description;
            CategoryId = categoryId;
            MovementDate = movementDate;
        }

        public void Credit(int amount, string description, int categoryId, DateTime movementDate)
        {
            this.AccountMovement(amount, description, categoryId, MovementType.Credit, movementDate);
        }

        public void Debit(int amount, string description, int categoryId, DateTime movementDate)
        {
            this.AccountMovement(amount * -1, description, categoryId, MovementType.Debit, movementDate);
        }
    }
}
