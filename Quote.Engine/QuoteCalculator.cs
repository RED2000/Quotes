using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Engine
{
  public class QuoteCalculator : IQuoteCalculator
  {
    private const int minimumLoanAmount = 1000;
    private const int maximumLoanAmount = 15000;
    private const int allowedIncrement = 100;
    private const int loanPeriod = 36;

    private readonly IMarketDataRepository marketDataRepository;

    private readonly IMarketCalculator marketCalculator;

    public QuoteCalculator(
      IMarketDataRepository marketDataRepository,
      IMarketCalculator marketCalculator)
    {
      this.marketDataRepository = marketDataRepository;
      this.marketCalculator = marketCalculator;
    }

    public QuoteResult GetQuote(int loanAmount)
    {
      if (loanAmount < minimumLoanAmount)
        return new FailedQuoteResult($"Loan amount cannot be less than {minimumLoanAmount}");

      if (loanAmount > maximumLoanAmount)
        return new FailedQuoteResult($"Loan amount cannot be more than {maximumLoanAmount}");

      if(loanAmount % allowedIncrement != 0)
        return new FailedQuoteResult($"Loan amount must be an increment of {allowedIncrement}");

      var market = marketDataRepository.GetMarketData();
      
      if (market.Sum(x => x.Available) < loanAmount)
        return new FailedQuoteResult($"Insuffiecnet funds avaialble for the requested amount");

      // Calculate the best market available for the loan amount.
      var availableMarket = marketCalculator.AvailableMarket(loanAmount, market);

      // Calculate the intrest rate
      var rate = marketCalculator.CalculateIntrestRate(availableMarket);

      // Calculate the monthly repayment
      var monthlyRepayment = marketCalculator.CalulateMonthlyRepayments(loanAmount, rate, loanPeriod);

      // Calculate the total repayments
      var totalRepayment = marketCalculator.CalculateTotalRepayments(monthlyRepayment, loanPeriod);
      
      var quote = new Quote();

      quote.RequestedAmount = loanAmount;
      quote.TotalRepayment = totalRepayment;
      quote.MonthlyRepayment = monthlyRepayment;
      quote.Rate = rate;

      return new SuccessfulQuoteResult(quote);
    }

    
  }
}
