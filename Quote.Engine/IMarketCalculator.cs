using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Engine
{
  public interface IMarketCalculator
  {
    double CalculateIntrestRate(IEnumerable<MarketData> availableFunds);

    double CalulateMonthlyRepayments(double principle, double intrestRate, int months);

    double CalculateTotalRepayments(double monthlyRepayments, double months);

    IEnumerable<MarketData> AvailableMarket(int loanAmount, IEnumerable<MarketData> market);
  }
}
