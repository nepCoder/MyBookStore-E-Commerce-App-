using MyBookStore.DataAccess.Repository.IRepository;
using MyBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookStore.DataAccess.Repository
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _db;

        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
        }
    }
}
