using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Entities
{
   public class Category
    {
        
            public int Id { get; set; }
            public string Name { get; set; }
            public int Summ { get; set; }
            public virtual ICollection<Cost> Costs{ get; set; }
            public int AcountId { get; set; }
            public virtual Acount Acount { get; set; }

    }
}
