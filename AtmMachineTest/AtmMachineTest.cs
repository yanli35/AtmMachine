using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Repository;
using Business.Access;
using System.Collections.Generic;
using Data.Access;

namespace AtmMachineTest
{
    [TestClass]
    public class AtmMachineTest
    {
        private readonly string insufficientFundsMessage = "Failure: insufficient funds";
        private readonly string successMessage = "Success: Dispensed";

        [TestMethod]
        public void RestockTest()
        {
            AtmMachine atmMachine = new AtmMachine();
            atmMachine.Restock();

            Assert.IsTrue(atmMachine.NumberOfBills(50) == 10);
        }

        [TestMethod]
        public void DisplayDenominationsTest()
        {
            AtmMachine atmMachine = new AtmMachine();
            atmMachine.Restock();

            List<int> denominations = new List<int> { 100, 50 };
            var result = atmMachine.DisplayDenominations(denominations);

            Assert.IsTrue(result.IndexOf("$50 - 10") != -1);
        }

        
        [TestMethod]
        public void EnoughBalanceNoOneDollarBills()
        {
            AtmMachine atmMachine = new AtmMachine();
            atmMachine.AddBills(1000, 10);
            atmMachine.AddBills(50, 10);
            atmMachine.AddBills(20, 10);
            atmMachine.AddBills(10, 10);
            atmMachine.AddBills(5, 10);
            atmMachine.AddBills(1, 0);

            var result = atmMachine.Dispense(1);

            Assert.AreEqual(result, insufficientFundsMessage);
        }

        [TestMethod]
        public void EnoughBalanceNoFiveDollarBillsEnoughOnes()
        {
            AtmMachine atmMachine = new AtmMachine();
            atmMachine.AddBills(1000, 10);
            atmMachine.AddBills(50, 10);
            atmMachine.AddBills(20, 10);
            atmMachine.AddBills(10, 10);
            atmMachine.AddBills(5, 0);
            atmMachine.AddBills(1, 10);

            var result = atmMachine.Dispense(5);

            Assert.IsTrue(result.IndexOf(successMessage) != -1);
        }

        [TestMethod]
        public void EnoughBalanceNoFiveDollarBillsNotEnoughOnes()
        {
            AtmMachine atmMachine = new AtmMachine();
            atmMachine.AddBills(1000, 10);
            atmMachine.AddBills(50, 10);
            atmMachine.AddBills(20, 10);
            atmMachine.AddBills(10, 10);
            atmMachine.AddBills(5, 0);
            atmMachine.AddBills(1, 3);

            var result = atmMachine.Dispense(5);

            Assert.AreEqual(result, insufficientFundsMessage);
        }

       /* [TestMethod]
        public void EnoughBalanceNoTenDollarBillsEnoughOnesAndFives()
        {

        }
        [TestMethod]
        public void EnoughBalanceNoTenDollarBillsNotEnoughOnesAndFives()
        {

        }

        [TestMethod]
        public void EnoughBalanceNoTwentyDollarBillsEnoughOnesAndFivesAndTens()
        {

        }

        [TestMethod]
        public void EnoughBalanceNoTwentyDollarBillsNotEnoughOnesAndFivesAndTens()
        {

        }

        [TestMethod]
        public void EnoughBalanceNoFiftyDollarBillsEnoughOnesAndFivesAndTensAndTwenties()
        {

        }

        [TestMethod]
        public void EnoughBalanceNoFiftyDollarBillsNotEnoughOnesAndFivesAndTensAndTwenties()
        {

        } */
    }
}
