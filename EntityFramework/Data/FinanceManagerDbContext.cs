using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data
{
    public class FinanceManagerDbContext : DbContext
    {
        DbSet<Acount> Acounts { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Cost> Costs { get; set; }
        DbSet<Incoum> Incoums { get; set;}
    }
}
