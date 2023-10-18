using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data.Configurations
{
    internal class IncoumConfig : IEntityTypeConfiguration<Incoum>
    {
        public void Configure(EntityTypeBuilder<Incoum> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasOne(x => x.Acount).WithMany(x => x.Incoums).HasForeignKey(x => x.AcountId);
        }
    }
}
