using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumingBinanceWebApi.Models
{
    public class SymbolsHelp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "EventType")]
        public string e { get; set; }
        [Display(Name = "EventTime")]
        public long E { get; set; }
        [Display(Name = "Symbol")]
        public string s { get; set; }
        [Display(Name = "AgregateTradeId")]
        public long a { get; set; }
        [Display(Name = "Price")]
        public decimal p { get; set; }
        [Display(Name = "Quantity")]
        public decimal q { get; set; }
        [Display(Name = "FirstTradeID")]
        public long f { get; set; }
        [Display(Name = "LastTradeId")]
        public long l { get; set; }
        [Display(Name = "TradeTime")]
        public long T { get; set; }
        [Display(Name = "IsBuyer")]
        public bool m { get; set; }
        [Display(Name = "Ignore")]
        public bool M { get; set; }
    }
}
