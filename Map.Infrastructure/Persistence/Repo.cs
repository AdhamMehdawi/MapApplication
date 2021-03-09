using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Map.Core.Interfaces;
using Map.Core.Shared;
using Map.Core.ViewModels;
using Map.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Map.Infrastructure.Persistence
{
    public class Repo<TEntity> : IDisposable, IRepo<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly MapDbContext _db;
        internal DbSet<TEntity> DbSet;
        public Repo(MapDbContext context)
        {
            _db = context;
            DbSet = context.Set<TEntity>();
        }


        public virtual IEnumerable<TEntity> Find(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);
            if (includeProperties != null)
                query = includeProperties(query);

            if (orderBy != null)
                query = orderBy(query);

            return query.ToList();
        }

        public async Task<TEntity> Get(int id) => await DbSet.FindAsync(id);

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, bool withDeleted = false) => DbSet.Where(predicate).FirstOrDefault();

        public async Task<TEntity> GetAsyncById(int id, params string[] include)
        {
            var item = await DbSet.FindAsync(id);
            if (item == null) return null;
            foreach (var express in include)
                _db.Entry(item).Reference(express).Load();
            return item;
        }
        public List<TEntity> GetQueryAsync(string sql)
        {
            var item = DbSet.FromSqlRaw(sql).ToList();
            return item;
        }


        public Task<List<TEntity>> GetAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            var list = DbSet.AsNoTracking().Where(predicate);
            list = include.Aggregate(list, (current, item) => current.Include(item));
            foreach (var entity in list.ToList())
                _db.Entry(entity).State = EntityState.Detached;
            return list.ToListAsync();
        } 

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null)
        {
            var list = DbSet.AsQueryable();
            if (includeProperties != null) list =   includeProperties(list);
            return await list.FirstOrDefaultAsync(predicate); 
        }

        public List<TEntity> GetAll<TProperty>(params Expression<Func<TEntity, TProperty>>[] include)
        {
            var list = DbSet.AsQueryable();
            list = include.Aggregate(list, (current, item) => current.Include(item));
            return list.ToList();
        }


        public List<TEntity> GetAllWhere(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> list = DbSet.Where(predicate);
            list = include.Aggregate(list, (current, item) => current.Include(item));
            return list.ToList();
        }

        public async Task<List<TEntity>> GetAllAsync(params string[] include)
        {
            IQueryable<TEntity> list = DbSet.AsQueryable();
            list = include.Aggregate(list, (current, item) => current.Include(item));
            return await list.ToListAsync();
        }

        public Task<List<TEntity>> GetAllSyncNew(Expression<Func<TEntity, bool>> predicate, bool withDeleted = false) => DbSet.Where(predicate).ToListAsync();

        public Task<List<TEntity>> GetAllWhereAsync(Expression<Func<TEntity, bool>> predicate) => DbSet.Where(predicate).ToListAsync();

        public Task<List<TEntity>> GetAllWhereAsync(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> list = DbSet.Where(predicate);

            list = include.Aggregate(list, (current, item) => current.Include(item));

            return list.ToListAsync();
        }

        public async Task<PageViewModel<TEntity>> GetPage(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null
         )
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                query = includeProperties(query);

            if (orderBy != null)
                query = orderBy(query);

            return new PageViewModel<TEntity>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Data = await query.ToListAsync(),
                Length = query.Count()
            };
        }

        public int Count(Expression<Func<TEntity, bool>> predicate) => DbSet.Count(predicate);

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => DbSet.CountAsync(predicate);
        public Task<int> CountAsync() => DbSet.CountAsync();

        public int Min(Expression<Func<TEntity, int>> predicate, Expression<Func<TEntity, bool>> condition) => DbSet.Where(condition).Min(predicate);

        public Task<int> MinAsync(Expression<Func<TEntity, int>> predicate, Expression<Func<TEntity, bool>> condition) => DbSet.Where(condition).MinAsync(predicate);

        public int Max(Expression<Func<TEntity, int>> predicate, Expression<Func<TEntity, bool>> condition) => DbSet.Where(condition).Max(predicate);

        public Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> predicate) => DbSet.MaxAsync(predicate);

        public TResult MaxWhere<TResult>(Expression<Func<TEntity, TResult>> predicate, Expression<Func<TEntity, bool>> condition)
        {
            IQueryable<TEntity> query = DbSet.Where(condition);

            if (!query.Any())
                return (TResult)Activator.CreateInstance(typeof(TResult));

            TResult entity = query.Max(predicate);

            return entity;
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate) => DbSet.Any(predicate);

        public double Sum(Expression<Func<TEntity, double>> predicate, Expression<Func<TEntity, bool>> condition, params string[] include)
        {
            IQueryable<TEntity> list = DbSet.Where(condition);
            list = include.Aggregate(list, (current, item) => current.Include(item));
            return list.Sum(predicate);
        }

        public Task<double> SumAsync(Expression<Func<TEntity, double>> predicate, Expression<Func<TEntity, bool>> condition, params string[] include)
        {
            IQueryable<TEntity> list = DbSet.Where(condition).AsQueryable();
            list = include.Aggregate(list, (current, item) => current.Include(item));
            return list.SumAsync(predicate);
        }
        private void FillBaseModelData(TEntity obj)
        {
            obj.CreatedAt = DateTime.UtcNow;
            obj.IsDeleted = false;
            obj.CreatedBy = 1;
        }

        private void FillBaseModelDataUpdate(TEntity obj)
        {
            obj.CreatedAt = DateTime.UtcNow;
            obj.IsDeleted = false;
            obj.CreatedBy = 1;
        }
        public TEntity Add(TEntity obj)
        {
            FillBaseModelData(obj);
            DbSet.Add(obj);
            return obj;
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            FillBaseModelData(obj);
            await DbSet.AddAsync(obj);
            return obj;
        }

        public async Task<IList<TEntity>> AddRangeAsync(IList<TEntity> obj)
        {
            foreach (var item in obj)
            {
                FillBaseModelData(item);
            }
            await DbSet.AddRangeAsync(obj);
            return obj;
        }

        public IList<TEntity> AddRange(IList<TEntity> obj)
        {
            foreach (var item in obj)
            {
                FillBaseModelData(item);
            }
            DbSet.AddRange(obj);
            return obj;
        }

        public TEntity Update(TEntity obj)
        {
            var isDetached = _db.Entry(obj).State == EntityState.Detached;
            if (isDetached)
                DbSet.Attach(obj);
            FillBaseModelDataUpdate(obj);
            DbSet.Update(obj);
            return obj;
        }



        public IList<TEntity> UpdateRange(IList<TEntity> obj)
        {
            foreach (var item in obj)
            {
                FillBaseModelData(item);
            }
            DbSet.UpdateRange(obj);
            return obj;
        }

        public TEntity Update(int id, TEntity obj)
        {
            FillBaseModelDataUpdate(obj);
            DbSet.Update(obj);
            return obj;
        }

        public Task Delete(TEntity obj)
        {
            if (obj != null)
            {
                DbSet.Remove(obj);
            }
            return Task.CompletedTask;
        }

        public Task Delete(Expression<Func<TEntity, bool>> predicate)
        {
            List<TEntity> objLst = DbSet.Where(predicate).ToList();
            return DeleteAll(objLst);
        }

        public Task DeleteAll(List<TEntity> obj)
        {
            DbSet.RemoveRange(obj);
            return Task.CompletedTask;
        }

        public Task Delete(object id)
        {
            var obj = DbSet.Find(id);
            return Delete(obj);
        }

        public void Dispose() => _db.Dispose();
        public Task DeleteAll(IEnumerable<int> obj)
        {
            foreach (var selected in obj)
                Delete(selected);
            return Task.CompletedTask;
        }
    }

}
