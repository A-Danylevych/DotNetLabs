using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Abstracts.IRepository.Base
{
    public interface IBaseRepository<in TKey, TEntity> : IDisposable
    {
        //CREATE
        public Task<TEntity> Create(TEntity entity);

        //READ
        public Task<List<TEntity>> GetAll();
        public Task<TEntity> GetById(TKey id);

        //UPDATE
        public void Update(TEntity entity);

        //DELETE
        public void Delete(TEntity entity);
    }
}