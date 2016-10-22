using Business.Access;
using Data.Access;
using System;

namespace AtmMachineApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            AtmMachine machine = new AtmMachine();
            machine.Restock();
            AtmMachineManager manager = new AtmMachineManager(machine);

            string input = Console.ReadLine();
            while (string.Compare(input, "Q", false) != 0 )
            {
                manager.DoWork(input);
                
                input = Console.ReadLine();
            }
        }
    }
}
