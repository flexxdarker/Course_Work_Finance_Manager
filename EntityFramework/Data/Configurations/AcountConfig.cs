using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Configurations
{
    public class AcountConfig : IEntityTypeConfiguration<Acount>
    {
        public void Configure(EntityTypeBuilder<Acount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Login).IsRequired();
          
          
        }
    }
}
