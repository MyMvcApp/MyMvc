using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data;
using PagedList;

namespace MyMvc.IRepository
{
    public interface IBaseRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        /// <summary>
        /// 不需要分页的数据查询
        /// </summary>
        /// <param name="filter">过滤条件lambda表达式</param>
        /// <param name="orderBy">排序条件lambda表达式</param>
        /// <param name="includeProperties">查询当中需要包括的子查询的属性名称</param>
        /// <returns></returns>
         IEnumerable<TEntity> GetData(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string includeProperties = "");
        /// <summary>
        /// 分页查询数据
        /// </summary>
         /// <param name="filter">过滤条件lambda表达式</param>
         /// <param name="orderBy">排序条件lambda表达式</param>
         /// <param name="includeProperties">查询当中需要包括的子查询的属性名称</param>
        /// <param name="pageSize">每页显示的条数，默认10</param>
        /// <param name="pageNumber">当前页码，默认1</param>
        /// <returns></returns>
         IPagedList<TEntity> GetPagedData(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string includeProperties = "",
         int pageSize = 10,
         int pageNumber = 1);
        /// <summary>
        /// 使用原始Sql语句进行查询
        /// </summary>
        /// <param name="query">Sql语句</param>
        /// <param name="parameters">对应Sql语句中的参数值</param>
        /// <returns></returns>
         IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
        /// <summary>
        /// 通过id查询实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         TEntity GetByID(object id);
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity">当前对应的实体</param>
         /// <param name="checkSave">是否需要SaveChange，默认是true,当涉及到事务的时候需要设置成false,然后用SaveChange()方法同时提交</param>
         void Create(TEntity entity,bool checkSave=true);
        /// <summary>
        /// 通过id删除
        /// </summary>
        /// <param name="id">id</param>
         /// <param name="checkSave">是否需要SaveChange，默认是true,当涉及到事务的时候需要设置成false,然后用SaveChange()方法同时提交</param>
         void Delete(object id, bool checkSave = true);
        /// <summary>
        /// 通过实体直接进行删除
        /// </summary>
        /// <param name="entityToDelete">实体</param>
         /// <param name="checkSave">是否需要SaveChange，默认是true,当涉及到事务的时候需要设置成false,然后用SaveChange()方法同时提交</param>
         void Delete(TEntity entityToDelete, bool checkSave = true);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entityToUpdate">当前实体</param>
         /// <param name="checkSave">是否需要SaveChange，默认是true,当涉及到事务的时候需要设置成false,然后用SaveChange()方法同时提交</param>
         void Update(TEntity entityToUpdate, bool checkSave = true);
        /// <summary>
        /// 提交Change
        /// </summary>
         void SaveChange();
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
         void Dispose(bool disposing);
    }
}
