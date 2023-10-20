using EntityFramework.Data;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repositories
{
	public interface IUoW
	{
		Repository<Acount> AcountRepo { get; set; }
		Repository<Category> CategoryRepo { get; set; }
		Repository<Cost> CostRepo { get; set; }
		Repository<Incoum> IncoumRepo { get; set;}
	}
	public class UnitOfWork
	{
		private static FinanceManagerDbContext context = new FinanceManagerDbContext();
		private Repository<Acount> acountRepo;
		private Repository<Category> categoryRepo;
		private Repository<Cost> costRepo;
		private Repository<Incoum> incoumRepo;

		public Repository<Acount> AcountRepo
		{
			get
			{
				if (this.acountRepo == null)
				{
					this.acountRepo = new Repository<Acount>(context);
				}
				return acountRepo;
			}
		}
		public Repository<Category> CategoryRepo
		{
			get
			{
				if (this.categoryRepo == null)
				{
					this.categoryRepo = new Repository<Category>(context);
				}
				return categoryRepo;
			}
		}
		public Repository<Cost> CostRepo
		{
			get
			{
				if (this.costRepo == null)
				{
					this.costRepo = new Repository<Cost>(context);
				}
				return costRepo;
			}
		}
		public Repository<Incoum> IncoumRepo
		{
			get
			{
				if (this.incoumRepo == null)
				{
					this.incoumRepo = new Repository<Incoum>(context);
				}
				return incoumRepo;
			}
		}
		public void Save()
		{
			context.SaveChanges();
		}
		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}
