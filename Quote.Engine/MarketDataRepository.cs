using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Engine
{
  public class MarketDataRepository : IMarketDataRepository
  {
    private readonly string path;
    private IEnumerable<MarketData> marketData;

    public MarketDataRepository(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        throw new ArgumentNullException("path", "The specified path cannot be null or empty");

      this.path = path;
    }

    public IEnumerable<MarketData> GetMarketData()
    {
      if (marketData == null)
      {
        if (!File.Exists(path))
          throw new FileNotFoundException("Market Data File does not exits", path);

        if (new FileInfo(path).Length == 0)
          throw new FileNotFoundException("Market Data File is empty", path);

        marketData = File.ReadAllLines(path)
            .Where(s => s.Count() > 1)
            .Skip(1)
            .Select(s => s.Split(','))
            .Select(s => new MarketData()
            {
              Lender = s[0],
              Rate = Convert.ToDouble(s[1]),
              Available = Convert.ToInt32(s[2])
            });        
      }

      return marketData;
    }
  }
}
