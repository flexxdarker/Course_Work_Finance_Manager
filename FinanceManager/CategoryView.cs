using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancingManager
{
    internal class CategoryView
    {
        public string Name { get; set; }
        public decimal Summ { get; set; }
        public decimal Persent { get; set; }

        public CategoryView(string name, decimal summ, decimal persent)
        {
            Name = name;
            Summ = Math.Round(summ, 2);
            Persent = Math.Round(persent, 2);
        }
    }
}
