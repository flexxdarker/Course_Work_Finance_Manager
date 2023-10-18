﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Entities
{
    public class Incoum
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int AcountId { get; set; }
        public virtual Acount Acounts { get; set; }
    }
}
