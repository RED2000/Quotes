using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.IO;

namespace Quote.Engine.Test
{
  [TestClass]
  public class MarketDataRepositoryTests
  {
    [TestMethod]
    public void MarketData_CanLoad()
    {
      IMarketDataRepository marketDataRepo = new MarketDataRepository(@"market.csv");

      var market = marketDataRepo.GetMarketData();

      Assert.AreEqual(7, market.Count());

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void MarketData_NoFileSpecified()
    {
      IMarketDataRepository marketDataRepo = new MarketDataRepository(null);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void MarketData_FileNotFound()
    {
      IMarketDataRepository marketDataRepo = new MarketDataRepository(@"missingfile.csv");

      var market = marketDataRepo.GetMarketData();

    }
  }
}
