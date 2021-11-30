using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Abstracts.IRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.Postgres.Repository.Base
{
    public class BaseRepository<TKey, TEntity>: IBaseRepository<TKey, TEntity> where TEntity : class 
    {
        protected readonly PlaybillDbContext Context;

        public BaseRepository(PlaybillDbContext context)
        {
            Context = context;
        }

        protected DbSet<TEntity> DbSet => Context.Set<TEntity>();
        
        public async Task<TEntity> Create(TEntity entity)
        {
            var result = (await DbSet.AddAsync(entity)).Entity;
            

            return result;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }
        protected async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
        

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseRepository()
        {
            Dispose(false);
        }
    }
}