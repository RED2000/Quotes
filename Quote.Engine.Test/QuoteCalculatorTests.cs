using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Quote.Engine.Test
{
  [TestClass]
  public class QuoteCalculatorTests
  {
    [TestMethod]
    public void Quote_LessThen1000()
    {
      var mockIMarketDataRepository = new Mock<IMarketDataRepository>();
      var mockIMarketCalculator = new Mock<IMarketCalculator>();

      IQuoteCalculator quoteCalculator = new QuoteCalculator(mockIMarketDataRepository.Object, mockIMarketCalculator.Object);

      QuoteResult result = quoteCalculator.GetQuote(100);

      Assert.AreEqual(QuoteResultCode.Fail, result.QuoteResultCode);
      Assert.AreEqual("Loan amount cannot be less than 1000", result.Message);
    }

    [TestMethod]
    public void Quote_GreaterThen15000()
    {
      var mockIMarketDataRepository = new Mock<IMarketDataRepository>();
      var mockIMarketCalculator = new Mock<IMarketCalculator>();

      IQuoteCalculator quoteCalculator = new QuoteCalculator(mockIMarketDataRepository.Object, mockIMarketCalculator.Object);

      QuoteResult result = quoteCalculator.GetQuote(15100);

      Assert.AreEqual(QuoteResultCode.Fail, result.QuoteResultCode);
      Assert.AreEqual("Loan amount cannot be more than 15000", result.Message);
    }

    [TestMethod]
    public void Quote_InrementOf100()
    {
      var mockIMarketDataRepository = new Mock<IMarketDataRepository>();
      var mockIMarketCalculator = new Mock<IMarketCalculator>();

      IQuoteCalculator quoteCalculator = new QuoteCalculator(mockIMarketDataRepository.Object, mockIMarketCalculator.Object);

      QuoteResult result = quoteCalculator.GetQuote(1036);

      Assert.AreEqual(QuoteResultCode.Fail, result.QuoteResultCode);
      Assert.AreEqual("Loan amount must be an increment of 100", result.Message);
    }

    [TestMethod]
    public void Quote_InsufficientFunds()
    {
      var mockIMarketDataRepository = new Mock<IMarketDataRepository>();
      var mockIMarketCalculator = new Mock<IMarketCalculator>();

      IQuoteCalculator quoteCalculator = new QuoteCalculator(mockIMarketDataRepository.Object, mockIMarketCalculator.Object);

      QuoteResult result = quoteCalculator.GetQuote(15000);

      Assert.AreEqual(QuoteResultCode.Fail, result.QuoteResultCode);
      Assert.AreEqual("Insuffiecnet funds avaialble for the requested amount", result.Message);
    }

    [TestMethod]
    public void Quote_Successfull()
    {
      var mockIMarketDataRepository = new Mock<IMarketDataRepository>();
      var marketCalculator = new MarketCalculator();

      mockIMarketDataRepository.Setup(x => x.GetMarketData()).Returns(TestData.MarketData);

      IQuoteCalculator quoteCalculator = new QuoteCalculator(mockIMarketDataRepository.Object, marketCalculator);

      QuoteResult result = quoteCalculator.GetQuote(1000);

      Assert.AreEqual(QuoteResultCode.Success, result.QuoteResultCode);
      Assert.AreEqual(30.78, Math.Round(result.Quote.MonthlyRepayment, 2));
      Assert.AreEqual(7, Math.Round(result.Quote.Rate * 100, 1));
      Assert.AreEqual(1000, result.Quote.RequestedAmount);
      Assert.AreEqual(1108.1, Math.Round(result.Quote.TotalRepayment, 2));
    }
  }
}
