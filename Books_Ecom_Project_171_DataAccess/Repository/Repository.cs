using Books_Ecom_Project_171_DataAccess.Data;
using Books_Ecom_Project_171_DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Books_Ecom_Project_171_DataAccess.Repository
{
    public class Repository<T>:IRepository<T> where T:class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeproperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
                query = query.Where(filter);
            if(includeproperties != null)
            {
                foreach (var inclProp in includeproperties.Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclProp);
                }
            }
            return query.FirstOrDefault();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter!=null)
                query = query.Where(filter);
            if(includeProperties!=null)
            {
                foreach (var inclProp in includeProperties.Split(new[] {','}))
                {
                    query = query.Include(inclProp);
                }
            }
            if(orderBy!=null)
                return orderBy(query).ToList();
            return query.ToList();
        }

        public void Remove(int id)
        {
            dbSet.Remove(Get(id));
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
