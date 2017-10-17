using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Engine
{
  public class Quote
  {
    public int RequestedAmount { get; set; }

    public double Rate { get; set; }

    public double MonthlyRepayment { get; set; }

    public double TotalRepayment { get; set; }
  }
}
