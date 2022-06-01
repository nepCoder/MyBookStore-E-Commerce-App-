﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBookStore.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAllValues();
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

    }
}
