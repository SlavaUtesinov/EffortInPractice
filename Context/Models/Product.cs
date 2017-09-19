using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    public class Product : BaseModel
    {        
        public decimal Price { get; set; }
        [StringLength(32)]
        public string Category { get; set; }   

        public virtual Stock Stock { get; set; }
        public int StockId { get; set; }
    }
}
