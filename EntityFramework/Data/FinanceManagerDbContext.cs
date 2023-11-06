using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data
{
    public class FinanceManagerDbContext : DbContext
    {
        public FinanceManagerDbContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string conn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FinanceManagerDB;Integrated Security=True;Connect Timeout=30;";
            optionsBuilder.UseSqlServer(conn);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<Category>()
            .Property(t => t.Color)
            .HasConversion<ColorToInt32Converter>();

            modelBuilder.Entity<Acount>().HasData(new[]
            {
                new Acount() { Id = 1, Login = "qwerty", Password = "qwerty-1", Profit = 0 }
            
            });

            modelBuilder.Entity<Limit>().HasData(new[]
            {
                new Limit() { Id = 1, Value = 10000}
            });

            modelBuilder.Entity<Category>().HasData(new[]
            { 
                new Category() { Id = 1, Name = "Food",Color = Color.PaleGreen, Summ = 0, AcountId = 1, LimitId = 1},           
                new Category() { Id = 2, Name = "Cafe", Color = Color.PaleGoldenrod,Summ = 0, AcountId = 1 , LimitId = 1},           
                new Category() { Id = 3, Name = "Entertainment",Color = Color.IndianRed, Summ = 0, AcountId = 1, LimitId = 1 },            
                new Category() { Id = 4, Name = "Transport",Color = Color.PaleTurquoise, Summ = 0, AcountId = 1, LimitId = 1 },            
                new Category() { Id = 5, Name = "Health",Color = Color.LightSeaGreen, Summ = 0, AcountId = 1, LimitId = 1 },            
                new Category() { Id = 6, Name = "Pet", Color = Color.Aquamarine,Summ = 0, AcountId = 1, LimitId = 1 },            
                new Category() { Id = 7, Name = "Family", Color = Color.PaleVioletRed, Summ = 0, AcountId = 1, LimitId = 1 },            
                new Category() { Id = 8, Name = "Clothes",  Color = Color.LightSlateGray, Summ = 0, AcountId = 1, LimitId = 1 }            
            });

            modelBuilder.Entity<Cost>().HasData(new[]
            {
                new Cost() { Id = 1, Name = "Cheese", Price = 85, CategoryId = 1 },
                new Cost() { Id = 2, Name = "Jacket", Price = 2000, CategoryId = 8},
                new Cost() { Id = 3, Name = "Vitamins", Price = 500, CategoryId = 5}
            });

            modelBuilder.Entity<Incoum>().HasData(new[]
            {
                new Incoum() { Id = 1, Name = "Salary", Price = 10200, AcountId= 1},
                new Incoum() { Id = 2, Name = "Hospital", Price = 3000, AcountId = 1}
            });
        }


        public DbSet<Acount> Acounts { get; set; }
        public DbSet<Limit> Limits { get; set;}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Incoum> Incoums { get; set;}
    }
    class ColorToInt32Converter : ValueConverter<Color, int> {
    public ColorToInt32Converter()
        : base(c => c.ToArgb(), v => Color.FromArgb(v)) { }
}

}
