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
    public class LimitConfig: IEntityTypeConfiguration<Limit>
    {
         public void Configure(EntityTypeBuilder<Limit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Categories).WithOne(x => x.Limit).HasForeignKey(x => x.LimitId);
        }
    }
}
