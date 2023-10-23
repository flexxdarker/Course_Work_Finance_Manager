using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Entities
{
    public class Limitt
    {
        public int Id { get; set; }
        public decimal Limit { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}
