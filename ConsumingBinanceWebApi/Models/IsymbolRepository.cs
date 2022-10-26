using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumingBinanceWebApi.Models
{
    public interface ISymbolRepository
    {        
        void Add(Symbols symbol);        
    }
}
