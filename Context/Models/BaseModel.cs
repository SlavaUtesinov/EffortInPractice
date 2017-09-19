using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }        
        [Required]
        [StringLength(32)]
        //[Column(TypeName = "varchar")]
        public string Name { get; set; }
    }
}
