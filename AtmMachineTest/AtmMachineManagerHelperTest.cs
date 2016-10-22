using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Business.Access;
using Data.Access;

namespace AtmMachineTest
{
    [TestClass]
    public class AtmMachineManagerHelperTest
    {
        [TestMethod]
        public void IsValidCommandTestWithValidCommand()
        {
            var input = "W$10";
            string command;

            bool result = AtmMachineManagerHelper.IsValidCommand(input, out command);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidCommandTestWithInvalidCommand()
        {
            var input = "K";
            string command;

            bool result = AtmMachineManagerHelper.IsValidCommand(input, out command);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetDenominationsTestWithAllValidBills()
        {
            AtmMachine machine = GetTestAtmMachine();

            var result = AtmMachineManagerHelper.GetDenominations("I $50 $20 $100", machine);

            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        public void GetDenominationsTestWithSomeInvalidBills()
        {
            AtmMachine machine = GetTestAtmMachine();

            var result = AtmMachineManagerHelper.GetDenominations("I $50 $37 $100", machine);

            Assert.IsTrue(result.Count() == 2);
        }

        private AtmMachine GetTestAtmMachine()
        {
            AtmMachine machine = new AtmMachine();

            machine.AddBills(100, 10);
            machine.AddBills(50, 10);
            machine.AddBills(20, 10);

            return machine;
        }
    }
}
