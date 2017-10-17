using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Engine
{
  public class QuoteResultFormatter : IQuoteResultFormatter
  {
    public string Format(QuoteResult quoteResult)
    {
      var output = new StringBuilder(90);

      if (quoteResult.QuoteResultCode == QuoteResultCode.Success)
      {
        output.AppendLine($"Requested amount: £{quoteResult.Quote.RequestedAmount}");
        output.AppendLine($"Rate: {quoteResult.Quote.Rate:P1}");
        output.AppendLine($"Monthly repayment: £{quoteResult.Quote.MonthlyRepayment:0.00}");
        output.AppendLine($"Total repayment: £{quoteResult.Quote.TotalRepayment:0.00}");
      }
      else
      {
        output.AppendLine("Unable to find you a quote:");
        output.AppendLine(quoteResult.Message);
      }

      return output.ToString();
    }
  }
}
