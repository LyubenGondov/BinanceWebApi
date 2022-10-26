using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewWebApi.Models
{
    public class Symbols
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int ID { get; set; }
        
        public string EventType { get; set; }
        
        public long EventTime { get; set; }
        
        public string Symbol { get; set; }
        
        public long AgregateTradeId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }
        
        public long FirstTradeID { get; set; }
        
        public long LastTradeId { get; set; }
        
        public long TradeTime { get; set; }
        
        public bool IsBuyer { get; set; }
        
        public bool Ignore { get; set; }

        public DateTime DateInsert { get; set; }
    }
}
