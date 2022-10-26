using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewWebApi.Models
{
    public interface ISymbolRepository
    {
        decimal Get24AvgPrice(string symbol);
        decimal SimpleMovingAverage(string symbol, int n, string p, DateTime s);
    }
}
