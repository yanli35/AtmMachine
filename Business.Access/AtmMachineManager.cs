using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Access
{
    public class AtmMachineManager
    {
        private IAtmMachine _machine;

        public AtmMachineManager(IAtmMachine machine)
        {
           _machine = machine;
        }

        public void DoWork(string input)
        {
            string command = string.Empty;
            if (!AtmMachineManagerHelper.IsValidCommand(input, out command))
            {
                Console.WriteLine(invalidCommand);
                return;
            }
 
            switch (command)
            {
                case "R":
                    DoRestock();
                    break;
                case "W":
                    DoWithdraw(input);
                    break;
                case "I":
                    DoDisplayDenominations(input);
                    break;
            }

        }
        public void DoRestock()
        {
            _machine.Restock();
            Console.Write(_machine.DisplayBalance());
        }

        public void DoDisplayDenominations(string denominationString)
        {
            IEnumerable<int> denominations = AtmMachineManagerHelper.GetDenominations(denominationString, _machine);

            if (denominations.Any())
            {
                Console.WriteLine(_machine.DisplayDenominations(denominations));
            }
        }

        public void DoWithdraw(string input)
        {
            int amount;
            string amountString = input.Substring(input.IndexOf("$") + 1);

            string message = int.TryParse(amountString, out amount)
                              ? _machine.Dispense(amount)
                              : invalidAmount;

            Console.WriteLine(message);
        } 

        private readonly string invalidAmount = "Failure: withdraw amount should be a number";
        private readonly string invalidCommand = "Failure: Invalid Command";
        private readonly List<string> _validCommands = new List<string> { "R", "W", "I" };
    }
}
