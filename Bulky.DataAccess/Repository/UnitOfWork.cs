using System;
using Bulky.Data;
using Bulky.DataAccess.Repository.IRepository;

namespace Bulky.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
        private ApplicationDbContext _db;
        public ICategoryRepository category { get; private set; }

        public IProductRepository Product { get; private set; }

        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }


        public void save()
        {
            //throw new NotImplementedException();
            _db.SaveChanges();
        }
    }
}

