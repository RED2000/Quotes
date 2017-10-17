using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Engine.Test
{
  public class TestData
  {
    public static IEnumerable<MarketData> MarketData
    {
      get
      {
        var market = new List<MarketData>();

        market.Add(new MarketData() { Available = 640, Lender = "Bob", Rate = 0.075 });
        market.Add(new MarketData() { Available = 480, Lender = "Jane", Rate = 0.069 });
        market.Add(new MarketData() { Available = 520, Lender = "Fred", Rate = 0.071 });
        market.Add(new MarketData() { Available = 170, Lender = "Mary", Rate = 0.104 });
        market.Add(new MarketData() { Available = 320, Lender = "John", Rate = 0.081 });
        market.Add(new MarketData() { Available = 140, Lender = "Dave", Rate = 0.074 });
        market.Add(new MarketData() { Available = 60, Lender = "Angela", Rate = 0.071 });

        return market;
      }
    }
  }
}
