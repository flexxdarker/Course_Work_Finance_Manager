using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Entities
{
    public class Cost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Limits { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
      
    }
}
