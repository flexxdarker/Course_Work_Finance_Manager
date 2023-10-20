﻿using EntityFramework.Entities;
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
        public FinanceManagerDbContext() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Acount>().HasData(new[]
            {
                new Acount() { Id = 1, Login = "qwerty", Password = "qwerty-1", Profit = 0 }
            
            });

            modelBuilder.Entity<Category>().HasData(new[]
            { 
                new Category() { Id = 1, Name = "Food", Summ = 0, AcountId = 1 },           
                new Category() { Id = 2, Name = "Cafe", Summ = 0, AcountId = 1 },           
                new Category() { Id = 3, Name = "Entertainment", Summ = 0, AcountId = 1 },            
                new Category() { Id = 4, Name = "Transport", Summ = 0, AcountId = 1 },            
                new Category() { Id = 5, Name = "Health", Summ = 0, AcountId = 1 },            
                new Category() { Id = 6, Name = "Pet", Summ = 0, AcountId = 1 },            
                new Category() { Id = 7, Name = "Family", Summ = 0, AcountId = 1 },            
                new Category() { Id = 8, Name = "Clothes", Summ = 0, AcountId = 1 }            
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





        DbSet<Acount> Acounts { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Cost> Costs { get; set; }
        DbSet<Incoum> Incoums { get; set;}
    }


}
