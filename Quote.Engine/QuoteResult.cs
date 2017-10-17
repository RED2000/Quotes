using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Engine
{
  public enum QuoteResultCode
  {
    Success = 1,
    Fail = 2
  }

  public abstract class QuoteResult
  {
    public QuoteResultCode QuoteResultCode { get; set; }

    public string Message { get; set; }

    public Quote Quote { get; set; }
  }

  public class SuccessfulQuoteResult : QuoteResult
  {
    public SuccessfulQuoteResult(Quote quote)
    {
      this.QuoteResultCode = QuoteResultCode.Success;
      this.Quote = quote;
    }
  }

  public class FailedQuoteResult : QuoteResult
  {
    public FailedQuoteResult(string message)
    {
      this.QuoteResultCode = QuoteResultCode.Fail;
      this.Message = message;
    }
  }
}
