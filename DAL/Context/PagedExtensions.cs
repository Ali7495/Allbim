using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Common.Extensions;
using Models.PageAble;

namespace DAL
{
    public static class PagedExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();
            int skip = GetDataSkip(page, pageSize, result);

            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize, CancellationToken cancellationToken) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = await query.CountAsync();
            int skip = GetDataSkip(page, pageSize, result);

            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);

            return result;
        }
        public static PagedResult<T> GetOrderedPaged<T>(this IQueryable<T> query, int page, int pageSize, string orderby) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();
            int skip = GetDataSkip(page, pageSize, result);


            string orderMode;
            Expression<Func<T, object>> orderByFunc;
            GetOrderByDetails(orderby, out orderMode, out orderByFunc);

            if (orderMode.ToLower() == "desc")
                query = query.OrderByDescending(orderByFunc);
            else
                query = query.OrderBy(orderByFunc);

            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
        public static async Task<PagedResult<T>> GetOrderedPagedAsync<T>(this IQueryable<T> query, int page, int pageSize, string orderby, CancellationToken cancellationToken) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = await query.CountAsync();
            int skip = GetDataSkip(page, pageSize, result);


            string orderMode;
            Expression<Func<T, object>> orderByFunc;
            GetOrderByDetails(orderby, out orderMode, out orderByFunc);

            if (orderMode.ToLower() == "desc")
                query = query.OrderByDescending(orderByFunc);
            else
                query = query.OrderBy(orderByFunc);

            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);

            return result;
        }

        public static async Task<PagedResult<T>> GetNewPagedAsync<T>(this IQueryable<T> query,CancellationToken cancellationToken, PageAbleModel pageAbleModel, Expression<Func<T, bool>> condition,params Expression<Func<T,object>>[] includes) where T : class
        {
            var result = new PagedResult<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    var memberExpression = include.Body as MemberExpression;
                    if (memberExpression != null)
                    query = query.Include(memberExpression.Member.Name);
                }
            }
          
            if (condition != null)
            {
                query = query.Where(condition);
            }
            
            result.CurrentPage = pageAbleModel.PageNumber;
            result.PageSize = pageAbleModel.PageSize;
            result.RowCount = await query.CountAsync();
            result.TotalCount = result.RowCount;
            
            if (pageAbleModel.SortField != null)
            {
                string orderMode=pageAbleModel.SortOrder;
                string orderby = pageAbleModel.SortField.ToPascalCase();
                Expression<Func<T, object>> orderByFunc;
                GetNewOrderByDetails(orderby, orderMode, out orderByFunc);
                if (orderMode.ToLower() == "desc")
                    query = query.OrderByDescending(orderByFunc);
                else
                    query = query.OrderBy(orderByFunc);
            }
            if (pageAbleModel.AllowPaginate)
            {
                int skip = GetDataSkip(pageAbleModel.PageNumber, pageAbleModel.PageSize, result);
                result.Results = await query.Skip(skip).Take(pageAbleModel.PageSize).ToListAsync(cancellationToken);
            }
            else
            {
                result.Results = await query.ToListAsync(cancellationToken);
            }
  

            
            return result;
        }

        #region  Helpers

        private static int GetDataSkip<T>(int page, int pageSize, PagedResult<T> result) where T : class
        {
            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            return skip;
        }
        private static void GetOrderByDetails<T>(string orderby, out string orderMode, out Expression<Func<T, object>> orderByFunc) where T : class
        {
            var orderInfo = orderby.Split(",");
            var orderByPropName = orderInfo.FirstOrDefault().ToPascalCase();
            orderMode = orderInfo.LastOrDefault();
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, orderByPropName);
            var propAsObject = Expression.Convert(property, typeof(object));
            orderByFunc = Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }
        private static void GetNewOrderByDetails<T>(string orderby, string orderMode, out Expression<Func<T, object>> orderByFunc) where T : class
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, orderby);
            var propAsObject = Expression.Convert(property, typeof(object));
            orderByFunc = Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }

        /* /// <summary>
         /// Checks that if the given order by string is valid or not
         /// If the given type doesn't contain the requested orderby column => the order by string is invalid
         /// OrderBy mode is neither ASC nor DESC (unknown orderby type) => the order by string is invalid
         /// </summary>
         /// <typeparam name="T">The given table type to be ordered</typeparam>
         /// <param name="orderBy">the string that contains orderby info (column and order type)</param>
         /// <returns></returns>
         public static bool IsValidOrderBy<T>(this string orderBy) where T : class
         {
             if (string.IsNullOrEmpty(orderBy))
                 return false;

             var orderByInfo = orderBy.Split(",");

             if (orderByInfo.Length != 2)
                 return false;

             if (typeof(T).GetProperty(orderByInfo[0].ToPascalCase(), BindingFlags.IgnoreCase) == null)
                 return false;

             if (orderByInfo[1].ToLower() != "asc" && orderByInfo[1].ToLower() != "desc")
                 return false;

             return true;
         }*/
        #endregion
    }
}
