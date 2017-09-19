using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    public class Stock : BaseModel
    {                
        public virtual ICollection<Product> Products { get; set; }                
        public decimal TotalAmount { get; set; }
    }
}
