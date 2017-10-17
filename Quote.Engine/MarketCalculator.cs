using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Engine
{
  public class MarketCalculator: IMarketCalculator
  {
    public double CalculateIntrestRate(IEnumerable<MarketData> availableFunds)
    {
      var totalAvailable = availableFunds.Select(l => l.Available).Sum();
      var averageInterest = 0.0;

      foreach (var fund in availableFunds)
        averageInterest += (fund.Rate * fund.Available) / totalAvailable;      

      return averageInterest;
    }

    public double CalulateMonthlyRepayments(double principle, double intrestRate, int months)
    {
      var monthlyRate = Math.Pow((1.0 + intrestRate), (1.0 / 12)) - 1.0;

      return (monthlyRate * principle) / (1.0 - Math.Pow((1 + monthlyRate), months * -1.0));
    }

    public double CalculateTotalRepayments(double monthlyRepayments, double months)
    {
      return monthlyRepayments * months;
    }

    public IEnumerable<MarketData> AvailableMarket(int loanAmount, IEnumerable<MarketData> market)
    {
      var sum = 0;

      return market.OrderBy(x => x.Rate).TakeWhile(x => { var funds = sum; sum += x.Available; return funds < loanAmount; }).ToList();
    }

  }
}
