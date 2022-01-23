using BioProductStore.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //Get all data
        Task<List<TEntity>> GetAll();
        //Querable - ca in sql, un fel de select String
        
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> condition);

        IQueryable<TEntity> GetAllAsQueryable();

        //Create data
        void Create(TEntity entity);
        //Create async
        Task CreateAsync(TEntity entity);
        //Create range
        void CreateRange(IEnumerable<TEntity> entities);
        //Create range async
        Task CreateRangeAsync(IEnumerable<TEntity> entities);



        //Update
        void Update(TEntity entity);
        //Update Range
        void UpdateRange(IEnumerable<TEntity> entities);



        //Delete
        void Delete(TEntity entity);
        //Delete Range
        void DeleteRange(IEnumerable<TEntity> entities);



        //Find by Id
        TEntity FindById(object id);
        
        //Find by condition
        TEntity FindBy(Func<TEntity, bool> condition);

        //find by Id Async
        Task<TEntity> FindByIdAsync(object id);



        //Save 
        bool Save();
        //Save async
        Task<bool> SaveAsync();
    }
}
