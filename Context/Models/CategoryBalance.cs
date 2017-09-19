using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    public class CategoryBalance
    {        
        [Key]
        [StringLength(32)]
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}
