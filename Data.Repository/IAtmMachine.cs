using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IAtmMachine
    {
        void Restock();
        string DisplayBalance();
        string DisplayDenominations(IEnumerable<int> denominations);
        
        string Dispense(int amount);

        bool IsValidBill(int bill);
    }
}
