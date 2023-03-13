using FinanceApp.Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var budgetService = new BudgetService(1000);
            var income = new Income("Salary", 5000);

            // Act
            budgetService.AddToBudget(income);

            // Assert
            Assert.Contains(income, budgetService.GetIncomes());
        }
    }
}
