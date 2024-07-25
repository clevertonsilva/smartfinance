using smartfinance.Domain.Entities.Shared;

namespace smartfinance.Domain.Entities
{
    public class TransactionType : EntityBase
    {
        public string Name { get; set; }
        public Movement Movement { get; set; }
    }
}
