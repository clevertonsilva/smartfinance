using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace smartfinance.Domain.Entities
{
    public class AccountMovement
    {
        private List<Movement> Movements = new List<Movement>();
        private int Balance = 0;

        public void Deposit(int Ammount, string Description, MovementCategory MoveCategory)
        {
            Movement movement = new Movement(Ammount, Description, MoveCategory, MovementType.Credit, DateTime.Now);
            Movements.Add(movement);

            Balance += Ammount;
        }

        public void Withdraw(int Ammount, string Description, MovementCategory MoveCategory)
        {
            Movement movement = new Movement(Ammount, Description, MoveCategory, MovementType.Debit, DateTime.Now);
            Movements.Add(movement);

            Balance -= Ammount;
        }

        public int getBalance => Balance;
    }
}
