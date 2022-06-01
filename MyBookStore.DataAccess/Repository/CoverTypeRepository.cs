using MyBookStore.DataAccess.Repository.IRepository;
using MyBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookStore.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private AppDbContext _db;

        public CoverTypeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj); 
        }
    }
}
