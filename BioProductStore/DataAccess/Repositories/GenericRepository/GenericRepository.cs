using BioProductStore.Data;
using BioProductStore.Models.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly BioProductStoreContext _context;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(BioProductStoreContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        //Get all function
        public async Task<List<TEntity>> GetAll()
        {
            // select from database
            // use AsNoTracking to get all data without modify, so we just display, not update
            // use asynchronous toList func
            return await _table.AsNoTracking().ToListAsync();
        }

        //not recomandable
        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _table.AsNoTracking();

            //try not to use toList, Ccount, etc before filtering the data
            //var entityList = _table.ToList();
            //var entityListFiltered = _table.Where(x => x.Id.ToString() != "");
            //better version the data is filtered in the query
            //select * from entity where Id is not null
            //var entitylistFiltered = _table.Where(x => x.Id.ToString() != "").ToList();
        }


        // CRUD operations:

        //Create
        public void Create(TEntity entity)
        {
            _table.Add(entity);
        }
        //Create Async
        public async Task CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }
        //Create Range
        public void CreateRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
        }
        //Create Range Async
        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);
        }



        //Update
        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }
        //Update Range
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _table.UpdateRange(entities);
        }



        //Delete
        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }
        //DeleteRange
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
        }



        //Find by Id
        public TEntity FindById(object id)
        {
            return _table.Find(id);
            //return _table.FirstOrDefault(x => x.Id.Equals(id));
        }
        //Find by Id async
        public async Task<TEntity> FindByIdAsync(object id)
        {
            return await _table.FindAsync(id);
            //return await _table.FirstOrDefault(x => x.Id.Equals(id));
        }

        public TEntity FindBy(Func<TEntity, bool> condition)
        {
            return _table.FirstOrDefault(condition);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> condition)
        {
            return _table.Where(condition);
        }

        //Save
        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
        //Save Async
        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

    }
}
