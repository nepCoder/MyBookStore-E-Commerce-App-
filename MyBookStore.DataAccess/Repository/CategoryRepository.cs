﻿using MyBookStore.DataAccess.Repository.IRepository;
using MyBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _db;

        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);    
        }
    }
}
