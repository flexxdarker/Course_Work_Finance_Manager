using EntityFramework.Data;
using EntityFramework.Entities;
using EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repositories
{
	public interface IUoW
	{
		IRepository<Acount> AcountRepo { get; set; }
		IRepository<Category> CategoryRepo { get; set; }
		IRepository<Cost> CostRepo { get; set; }
		IRepository<Incoum> IncoumRepo { get; set;}
	}
	public class UnitOfWork
	{
		private static FinanceManagerDbContext context = new FinanceManagerDbContext();
		private IRepository<Acount> acountRepo;
		private IRepository<Category> categoryRepo;
		private IRepository<Cost> costRepo;
		private IRepository<Incoum> incoumRepo;

		public IRepository<Acount> AcountRepo
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
		public IRepository<Category> CategoryRepo
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
		public IRepository<Cost> CostRepo
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
		public IRepository<Incoum> IncoumRepo
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
