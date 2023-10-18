using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Entities
{
    public class Acount
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal Profit { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Incoum> Incoums { get; set; }
    }
}
