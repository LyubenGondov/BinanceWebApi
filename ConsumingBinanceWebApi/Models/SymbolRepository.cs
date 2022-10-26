using ConsumingBinanceWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumingBinanceWebApi.Models
{
    public class SymbolRepository : ISymbolRepository
    {
        private Context _context;
        public SymbolRepository(Context context)
        {
            _context = context;
        }
        public void Add(Symbols symbol)
        {
            _context.Symbols.Add(symbol);
            _context.SaveChanges();            
        }  
    }
}
