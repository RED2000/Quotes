using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Quote.Engine.Test
{
  [TestClass]
  public class MarketCalculatorTests
  {
    [TestMethod]
    public void Calculate_BestAvailableFunds()
    {
      IMarketCalculator marketCalculator = new MarketCalculator();

      var availableMarket = marketCalculator.AvailableMarket(1000, TestData.MarketData);

      Assert.AreEqual(2, availableMarket.Count());

      Assert.AreEqual(0.069, availableMarket.ToArray()[0].Rate);
      Assert.AreEqual(0.071, availableMarket.ToArray()[1].Rate);
    }

    [TestMethod]
    public void Calculate_CalculateIntrestRate()
    {
      IMarketCalculator marketCalculator = new MarketCalculator();

      var availableMarket = marketCalculator.AvailableMarket(1000, TestData.MarketData);

      var intrestRate = marketCalculator.CalculateIntrestRate(availableMarket);

      Assert.AreEqual(0.070039999999999991, intrestRate);
    }

    [TestMethod]
    public void Calculate_MonthlyRepaymentsExample()
    {

      var loan = 1000.0;
      var rate = 0.07004;
      var months = 36;

      IMarketCalculator marketCalculator = new MarketCalculator();

      var monthlyRepayment = marketCalculator.CalulateMonthlyRepayments(loan, rate, months);

      Assert.AreEqual(30.78, Math.Round(monthlyRepayment, 2));
    }

    [TestMethod]
    public void Calculate_TotalRepaymentsExample()
    {
      var loan = 1000.0;
      var rate = 0.07004;
      var months = 36;

      IMarketCalculator marketCalculator = new MarketCalculator();

      var monthlyRepayment = marketCalculator.CalulateMonthlyRepayments(loan, rate, months);

      var totalRepayments = marketCalculator.CalculateTotalRepayments(monthlyRepayment, 36);

      Assert.AreEqual(1108.10, Math.Round(totalRepayments, 2));
    }
  }
}
