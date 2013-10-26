using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data;
using MyMvc.Context;
using PagedList;
using MyMvc.IRepository;
using System.Web.Mvc;
namespace MyMvc.Repository
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        internal TContext context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(TContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// 不需要分页的数据查询
        /// </summary>
        /// <param name="filter">过滤条件lambda表达式</param>
        /// <param name="orderBy">排序条件lambda表达式</param>
        /// <param name="includeProperties">查询当中需要包括的子查询的属性名称</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetData(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="filter">过滤条件lambda表达式</param>
        /// <param name="orderBy">排序条件lambda表达式</param>
        /// <param name="includeProperties">查询当中需要包括的子查询的属性名称</param>
        /// <param name="pageSize">每页显示的条数，默认10</param>
        /// <param name="pageNumber">当前页码，默认1</param>
        /// <returns></returns>
        public virtual IPagedList<TEntity> GetPagedData(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = "",
          int pageSize = 10, 
          int pageNumber = 1)
        {
            return GetData(filter: filter, orderBy: orderBy, includeProperties: includeProperties).ToPagedList(pageNumber, pageSize);
        }

        /// <summary>
        /// 分页查询数据，用于直接输出Json格式数据
        /// </summary>
        /// <param name="filter">过滤条件lambda表达式</param>
        /// <param name="orderBy">排序条件lambda表达式</param>
        /// <param name="includeProperties">查询当中需要包括的子查询的属性名称</param>
        /// <param name="pageSize">每页显示的条数，默认10</param>
        /// <param name="pageNumber">当前页码，默认1</param>
        /// <returns></returns>
        public virtual JsonResult GetPagedJsonData(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = "",
          int pageSize = 10,
          int pageNumber = 1) {
              IPagedList<TEntity> data = GetPagedData(filter: filter, orderBy: orderBy, includeProperties: includeProperties, pageSize: pageSize, pageNumber: pageNumber);
              //因为EasyUi支持的分页属性是 total和rows所以这里格式化成这种格式
            return new JsonResult() { Data = new { total = data.TotalItemCount, rows = data.ToArray() } };
        }

        /// <summary>
        /// 使用原始Sql语句进行查询
        /// </summary>
        /// <param name="query">Sql语句</param>
        /// <param name="parameters">对应Sql语句中的参数值</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).ToList();
        }

        /// <summary>
        /// 通过id查询实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity">当前对应的实体</param>
        /// <param name="checkSave">是否需要SaveChange，默认是true,当涉及到事务的时候需要设置成false,然后用SaveChange()方法同时提交</param>
        public virtual void Create(TEntity entity, bool checkSave = true)
        {
            dbSet.Add(entity);
            if(checkSave) context.SaveChanges();
        }

        /// <summary>
        /// 通过id删除
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="checkSave">是否需要SaveChange，默认是true,当涉及到事务的时候需要设置成false,然后用SaveChange()方法同时提交</param>
        public virtual void Delete(object id, bool checkSave = true)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete, checkSave: checkSave);
        }

        /// <summary>
        /// 通过实体直接进行删除
        /// </summary>
        /// <param name="entityToDelete">实体</param>
        /// <param name="checkSave">是否需要SaveChange，默认是true,当涉及到事务的时候需要设置成false,然后用SaveChange()方法同时提交</param>
        public virtual void Delete(TEntity entityToDelete, bool checkSave = true)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            if (checkSave) context.SaveChanges();
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entityToUpdate">当前实体</param>
        /// <param name="checkSave">是否需要SaveChange，默认是true,当涉及到事务的时候需要设置成false,然后用SaveChange()方法同时提交</param>
        public virtual void Update(TEntity entityToUpdate, bool checkSave = true)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            if (checkSave) context.SaveChanges();
        }

        /// <summary>
        /// 提交Change
        /// </summary>
        public virtual void SaveChange() 
        {
            context.SaveChanges();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
