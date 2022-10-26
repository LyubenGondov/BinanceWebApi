
using NewWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewWebApi.Models
{
    public class SymbolRepository : ISymbolRepository
    {
        private Context _context;
        public SymbolRepository(Context context)
        {
            _context = context;
        }

        public decimal Get24AvgPrice(string symbol)
        {
            List<Symbols> symbolList = new List<Symbols>();            
            DateTime now = DateTime.Now;
            decimal price = 0;
            int count = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
             && (x.DateInsert > now.AddHours(-24) && x.DateInsert <= now)).ToList<Symbols>().Count;
            symbolList = _context.Symbols.Where(x => x.Symbol == symbol+"USDT" 
            && (x.DateInsert > now.AddHours(-24) && x.DateInsert <= now)).ToList<Symbols>();
            foreach(var s in symbolList)
            {
                price += s.Price;
            }
            if (count != 0)
            {
                return price / count;
            }
            return 0;
        }

        public decimal SimpleMovingAverage(string symbol, int n, string p, DateTime s)
        {
            List<Symbols> symbolList = new List<Symbols>();
            DateTime now = DateTime.Now;
            decimal SMA = 0;
            decimal price = 0;
            if (p == "1w" || p == "1d" || p == "30m" || p == "5m" || p == "1m")
            {
                switch (p)
                {
                    case "1w":
                        if (s == null)
                        {
                            
                            symbolList = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
                         && (x.DateInsert > now.AddDays(-7*n) && x.DateInsert <= now)).ToList<Symbols>();
                            foreach (var sym in symbolList)
                            {
                                price += sym.Price;
                            }
                            SMA = price / 7 * n;
                        }
                        else
                        {
                            symbolList = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
                            && (x.DateInsert > s.AddDays(-7 * n) && x.DateInsert <= s)).ToList<Symbols>();
                            foreach (var sym in symbolList)
                            {
                                price += sym.Price;
                            }
                            SMA = price / 7 * n;
                        }                        
                        break;
                    case "1d":
                        if (s == null)
                        {
                            symbolList = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
                             && (x.DateInsert > now.AddDays(-n) && x.DateInsert <= now)).ToList<Symbols>();
                            foreach (var sym in symbolList)
                            {
                                price += sym.Price;
                            }
                            SMA = price / n;
                        }
                        else
                        {
                            symbolList = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
                             && (x.DateInsert > s.AddDays(-n) && x.DateInsert <= s)).ToList<Symbols>();
                            foreach (var sym in symbolList)
                            {
                                price += sym.Price;
                            }
                            SMA = price / n;
                        }
                        break;
                    case "30m":
                        if (s == null)
                        {
                            symbolList = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
                             && (x.DateInsert > now.AddMinutes(-30*n) && x.DateInsert <= now)).ToList<Symbols>();
                            foreach (var sym in symbolList)
                            {
                                price += sym.Price;
                            }
                            SMA = price / 30*n;
                        }
                        else
                        {
                            symbolList = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
                             && (x.DateInsert > s.AddMinutes(-30 * n) && x.DateInsert <= s)).ToList<Symbols>();
                            foreach (var sym in symbolList)
                            {
                                price += sym.Price;
                            }
                            SMA = price / 30 * n;
                        }
                        break;
                    case "5m":
                        if (s == null)
                        {
                            symbolList = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
                             && (x.DateInsert > now.AddMinutes(-5 * n) && x.DateInsert <= now)).ToList<Symbols>();
                            foreach (var sym in symbolList)
                            {
                                price += sym.Price;
                            }
                            SMA = price / 5 * n;
                        }
                        else
                        {
                            symbolList = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
                             && (x.DateInsert > s.AddMinutes(-5 * n) && x.DateInsert <= s)).ToList<Symbols>();
                            foreach (var sym in symbolList)
                            {
                                price += sym.Price;
                            }
                            SMA = price / 5 * n;
                        }
                        break;
                    case "1m":
                        if (s == null)
                        {
                            symbolList = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
                             && (x.DateInsert > now.AddMinutes(-n) && x.DateInsert <= now)).ToList<Symbols>();
                            foreach (var sym in symbolList)
                            {
                                price += sym.Price;
                            }
                            SMA = price / n;
                        }
                        else
                        {
                            symbolList = _context.Symbols.Where(x => x.Symbol == symbol + "USDT"
                             && (x.DateInsert > s.AddMinutes(-n) && x.DateInsert <= s)).ToList<Symbols>();
                            foreach (var sym in symbolList)
                            {
                                price += sym.Price;
                            }
                            SMA = price / n;
                        }
                        break;
                }
                return SMA;
            }
            else
            {
                return 0;
            }
            
        }
       
    }
}
