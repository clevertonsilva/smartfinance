using smartfinance.Domain.Entities.Shared;

namespace smartfinance.Domain.Entities
{
    public class Account : EntityBase
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public IEnumerable<Movement> Movements { get; set; } = Enumerable.Empty<Movement>();
    }
}
