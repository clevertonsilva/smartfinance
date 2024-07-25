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
        public DateTime? MovementDate { get; set; }
        public MovementType Type { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }

        private void AccountMovement(int amount, string description, MovementType type, int categoryId, int transactionTypeId, DateTime? movementDate)
        {
            Amount = amount;
            Description = description;
            Type = type;
            CategoryId = categoryId;
            TransactionTypeId = transactionTypeId;
            MovementDate = movementDate;
        }

        public void Credit(int amount, string description, int categoryId, int transactionTypeId, DateTime? movementDate)
        {
            this.AccountMovement(amount, description, MovementType.Credit, categoryId, transactionTypeId, movementDate);
        }

        public void Debit(int amount, string description, int categoryId, int transactionTypeId, DateTime? movementDate)
        {
            this.AccountMovement(amount * -1, description, MovementType.Debit, categoryId, transactionTypeId, movementDate);
        }
    }
}
