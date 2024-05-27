using System;
using System.Linq.Expressions;
using Bulky.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        //public void Add(Category entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Category Get(Expression<Func<Category, bool>> filter)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Category> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Remove(Category entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveRange(IEnumerable<Category> entity)
        //{
        //    throw new NotImplementedException();
        //}

        public void save()
        {
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Update(Category obj)
        {
            throw new NotImplementedException();
        }
    }
}

