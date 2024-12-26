﻿using DataAcsses.Db;
using DataAcsses.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DataAcsses.Repository
{

  
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly AppDbContext _db;
            internal DbSet<T> dbSet;
            public Repository(AppDbContext db)
            {
                _db = db;
                this.dbSet = _db.Set<T>();
                //_db.Categories == dbSet
                _db.Doctors.Include(u => u.clinic).Include(u => u.Id);

            }

            public void Add(T entity)
            {
                dbSet.Add(entity);
            }

            public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
            {
                IQueryable<T> query = dbSet;
                query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

            }

            public IEnumerable<T> GetAll(string? includeProperties = null)
            {
                IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
               
                return query.ToList();
            }

            public void Remove(T entity)
            {
                dbSet.Remove(entity);
            }

            public void RemoveRange(IEnumerable<T> entity)
            {
                dbSet.RemoveRange(entity);
            }
        }

    

}
