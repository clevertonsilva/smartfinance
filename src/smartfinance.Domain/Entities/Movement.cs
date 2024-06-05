using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartfinance.Domain.Entities
{
    public enum MovementCategory
    {
        Bill,
    };

    public enum MovementType
    {
        Credit,
        Debit
    };
    public struct Movement
    {
        public int Ammount { get; set; }
        public string Description { get; set; }
        public MovementCategory Category { get; set; }
        public MovementType Type { get; set; }
        public DateTime Date { get; set; }

        public Movement(int Ammount, string Description, MovementCategory Category, MovementType Type, DateTime Date)
        {
            this.Type = Type;
            this.Ammount = Ammount;
            this.Description = Description;
            this.Category = Category;
            this.Date = Date;
        }
    }
}
