using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;

namespace Business.Access
{
    public static class AtmMachineManagerHelper
    {
        private static List<string> _validCommands = new List<string> { "R", "W", "I" };

        public static bool IsValidCommand(string input, out string command)
        {
            command = string.Empty;
            if (string.IsNullOrEmpty(input))
                return false;

            command = input.Substring(0, 1);
            return _validCommands.Contains(command);
        }

        public static IEnumerable<int> GetDenominations(string input, IAtmMachine machine)
        {
            List<int> result = new List<int>();

            string[] inputStr = input.Replace(" ", "").Split('$');

            int bill;
            for (int i = 1; i < inputStr.Count(); i++)
            {
                if (int.TryParse(inputStr[i], out bill) && machine.IsValidBill(bill))
                {
                    result.Add(bill);
                }
            }

            return result;
        }
    }
}
