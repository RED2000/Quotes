using Quote.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote
{
  class Program
  {
    static void Main(string[] args)
    {
      int exitCode = 0;
      int loanAmount = 0;

      try
      {
        if (args.Length < 2)
        {
          Console.WriteLine("Not enough parameters - expected [market_file] [loan_amount]");
        }
        else if (!int.TryParse(args[1], out loanAmount))
        {
          Console.WriteLine("Loan Amount must be a valid integer");
        }
        else
        {          
          var marketData = args[0];

          if (!int.TryParse(args[1], out loanAmount))
            Console.WriteLine("Loan Amount must be a valid integer");
          
          IMarketCalculator marketCalculator = new MarketCalculator();
          IMarketDataRepository marketDataRepository = new MarketDataRepository(marketData);
          IQuoteCalculator quoteCalculator = new QuoteCalculator(marketDataRepository, marketCalculator);
          IQuoteResultFormatter resultFormatter = new QuoteResultFormatter();

          var quoteResult = quoteCalculator.GetQuote(loanAmount);

          Console.WriteLine(resultFormatter.Format(quoteResult));
        }
      }
      catch (Exception exception)
      {
        Console.WriteLine("Oops! an error occured retrieving your quote");
        Console.WriteLine(exception.Message);
        exitCode = 1;
      }

      Console.ReadKey();

      Environment.Exit(exitCode);
    }
  }
}
