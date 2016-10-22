using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Access
{
    public class AtmMachine : IAtmMachine
    {
        private Dictionary<int, int> _bills = new Dictionary<int, int>();
        public AtmMachine()
        {

        }

        #region IAtmMachine Members

        public void Restock()
        {
            _bills.Clear();

            _bills.Add(100, 10);
            _bills.Add(50, 10);
            _bills.Add(20, 10);
            _bills.Add(10, 10);
            _bills.Add(5, 10);
            _bills.Add(1, 10);
        }

        public void AddBills(int denomination, int numberOfBills)
        {
            if (_bills.ContainsKey(denomination))
            {
                int existingNumOfBills = _bills[denomination];
                _bills[denomination] = existingNumOfBills + numberOfBills;
            }
            else
            {
                _bills[denomination] = numberOfBills;
            }
        }

        public string DisplayBalance()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Machine balance:\n");
            
            result.Append(DisplayDenominations(_bills.Keys));

            return result.ToString();
        }

        public string DisplayDenominations(IEnumerable<int> denominations)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                foreach (var item in denominations)
                {
                    result.Append(string.Format("${0} - {1}\n", item, _bills[item]));
                }

                return result.ToString();
            }
            catch(Exception ex)
            {
                return "Failure: one or more denominations are wrong";
            }
        }

        public bool IsValidBill(int bill)
        {
            return _bills.Keys.Contains(bill);
        }

        public int RemainingBalance
        {
            get
            {
                return _bills.Sum(p => p.Key * p.Value);
            }
        }

        public string Dispense(int amount)
        {

            if (amount > RemainingBalance)
            {
                return insufficientFundsMessage;
            }

            if (amount == RemainingBalance)
            {
                ClearBills();
                return ComponseSuccessMessage(amount);
            }

            if (IsNotDoableAmount(amount))
            {
                return insufficientFundsMessage;
            }

            return AttemptWithdraw(amount);
        }
#endregion

        #region HELPER METHODS

        public int NumberOfBills(int denomination)
        {
            return _bills.ContainsKey(denomination) ? _bills[denomination] : 0;
        }

        private void ClearBills()
        {
            _bills.Select(p => p.Key)
                  .ToList()
                  .ForEach(k => SetDenomination(k, 0));
        }

        private void SetDenomination(int denomination, int numberOfBills)
        {
            _bills[denomination] = numberOfBills;
        }

        private string AttemptWithdraw(int amountToWithdraw)  
        {
            int amount = amountToWithdraw;
            int numberOfBillsNeeded, numberOfBillsTaken;
            Dictionary<int, int> billsToTake = new Dictionary<int, int>();

            foreach (var item in _bills)
            {
                numberOfBillsNeeded = amount / item.Key;

                numberOfBillsTaken = numberOfBillsNeeded > item.Value ? item.Value : numberOfBillsNeeded;

                billsToTake.Add(item.Key, numberOfBillsTaken);

                amount -= item.Key * numberOfBillsTaken;

                if (amount == 0)
                {
                    break;
                }
            }

            if (amount == 0)
            {
                Withdraw(billsToTake);

                return ComponseSuccessMessage(amountToWithdraw);
            }
            return insufficientFundsMessage;
        }

        private void Withdraw(Dictionary<int, int> billsToTake)
        {
            foreach(var item in billsToTake)
            {
                SetDenomination(item.Key, _bills[item.Key] - item.Value);
            }
        }

        private bool IsNotDoableAmount(int amount)
        {
            return (_bills[1] == 0 && amount % 5 > 0) || //no dollar bills
                   (_bills[5] == 0 && amount % 10 > _bills[1]) || // no 5-dollar bills and not enough 1 dollar bills
                   (_bills[10] == 0 && amount % 20 > (_bills[1] + _bills[5])) || // no 10-dollar bills and not enough 5 and 1 dollar bills
                   (_bills[20] == 0 && amount % 50 > (_bills[1] + _bills[5] + _bills[10])) ||// no 20-dollar bills and not enough 10, 5 and 1 dollar bills
                   (_bills[50] == 0 && amount % 100 > (_bills[1] + _bills[5] + _bills[10] + _bills[20])); // no 50-dollar bills and not enough 20, 10, 5 and 1 dollar bills
        }

        private string ComponseSuccessMessage(int amount)
        {
            return string.Format(successMessage, amount) + DisplayBalance();
        }

        private readonly string successMessage = "Success: Dispensed ${0}\n";
        private readonly string insufficientFundsMessage = "Failure: insufficient funds";

#endregion
    }
}
