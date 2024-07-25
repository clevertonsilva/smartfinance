using smartfinance.Domain.Entities.Shared;
using smartfinance.Domain.Enums;

namespace smartfinance.Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public MovementType Type { get; set; }
        public Movement Movement { get; set; }
    }
}
