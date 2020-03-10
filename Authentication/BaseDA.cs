using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Authentication
{
    public class BaseDA<TEntity, TId> : IDisposable where TEntity : class
    {
        public int TotalRecord = 0;
        public int TotalRecordAll = 0;
        private bool IsDisposed = false;
        private DbContext dbContext;
        private DbSet<TEntity> dbSet;

        public DbTransaction Transaction { get; set; }

        public BaseDA()
        {
        }

        public BaseDA(DbContext context)
        {
            this.dbContext = context;
            this.dbSet = (DbSet<TEntity>)context.Set<TEntity>();
        }

        protected DbContext DBContext
        {
            get
            {
                return this.dbContext;
            }
            set
            {
                this.dbContext = value;
                if (this.dbContext == null)
                    return;
                this.dbSet = (DbSet<TEntity>)this.dbContext.Set<TEntity>();
            }
        }

        public IQueryable<TEntity> ExecWithStoreProcedure(
          string query,
          params object[] parameters)
        {
            return ((IEnumerable<TEntity>)this.DBContext.Database.SqlQuery<TEntity>(query, parameters)).AsQueryable<TEntity>();
        }

        public bool CheckConnected()
        {
            return this.dbContext.Database.Exists();
        }

        public virtual int Clear()
        {
            return this.dbSet.RemoveRange((IEnumerable<TEntity>)this.dbSet).Count<TEntity>();
        }

        public virtual int TotalReCord()
        {
            return ((IQueryable<TEntity>)this.dbSet).Count<TEntity>();
        }

        public virtual int TotalReCord(Expression<Func<TEntity, bool>> predicate)
        {
            return ((IQueryable<TEntity>)this.dbSet).Where<TEntity>(predicate).Count<TEntity>();
        }

        public virtual void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public virtual void DeleteAndSubmit(TEntity entity)
        {
            this.Delete(entity);
            this.Save();
        }

        //public virtual void Delete(Expression<Func<TEntity, bool>> predicate)//temp
        //{
        //    foreach (TEntity entity in (IEnumerable<TEntity>)this.Filter(predicate))
        //    {
        //        if (((DbEntityEntry<TEntity>)this.dbContext.Entry<TEntity>((M0)entity)).get_State() == 1)
        //            this.dbSet.Attach(entity);
        //        this.dbSet.Remove(entity);
        //    }
        //    this.Save();
        //}

        public virtual void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual void InsertAndSubmit(TEntity entity)
        {
            this.dbSet.Add(entity);
            this.dbContext.SaveChanges();
        }

        //public virtual void Update(TEntity entity)//temp
        //{
        //    this.dbSet.Attach(entity);
        //    ((DbEntityEntry<TEntity>)this.dbContext.Entry<TEntity>((M0)entity)).set_State((EntityState)16);
        //}

        public void UpdateAndSubmit(TEntity entity)//temp
        {
            //this.dbSet.Attach(entity);//temp
            //((DbEntityEntry<TEntity>)this.dbContext.Entry<TEntity>((M0)entity)).set_State((EntityState)16);//temp
            this.dbContext.SaveChanges();
        }

        public void UpdateAndSubmit(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            this.Update(entity, predicate);
            this.Save();
        }

        public void Update(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            TEntity entity1 = ((IQueryable<TEntity>)this.dbSet).Where<TEntity>(predicate).SingleOrDefault<TEntity>();
            if ((object)entity1 == null)
                throw new NullReferenceException("Error: Entity is not exist.");
            if (entity1.Equals((object)entity))
                return;
            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                if (property.CanWrite && property.CanRead)
                {
                    if (property.PropertyType == typeof(DateTime))
                    {
                        if (property.GetValue((object)entity, (object[])null).ToString() != "1/1/0001 12:00:00 AM")
                            property.SetValue((object)entity1, property.GetValue((object)entity, (object[])null), (object[])null);
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        if (int.Parse(property.GetValue((object)entity, (object[])null).ToString()) > 0)
                            property.SetValue((object)entity1, property.GetValue((object)entity, (object[])null), (object[])null);
                    }
                    else
                        property.SetValue((object)entity1, property.GetValue((object)entity, (object[])null), (object[])null);
                }
            }
        }

        public virtual TEntity GetById(object id)
        {
            return this.dbSet.Find(new object[1] { id });
        }

        public virtual TEntity GetByQuery(Expression<Func<TEntity, bool>> predicate)
        {
            return ((IQueryable<TEntity>)this.dbSet).FirstOrDefault<TEntity>(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return ((IEnumerable<TEntity>)this.dbSet).AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> GetByQuery(string FieldSort, bool FieldOption)
        {
            return BaseDA<TEntity, TId>.Sort<TEntity>((IQueryable<TEntity>)this.dbSet, FieldSort, FieldOption, ref this.TotalRecordAll);
        }

        public IQueryable<TEntity> GetByQuery(
          Expression<Func<TEntity, bool>> predicate,
          string FieldSort,
          bool FieldOption)
        {
            return BaseDA<TEntity, TId>.Sort<TEntity>(((IQueryable<TEntity>)this.dbSet).Where<TEntity>(predicate), FieldSort, FieldOption, ref this.TotalRecord);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return ((IQueryable<TEntity>)this.dbSet).Where<TEntity>(predicate);
        }

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return ((IQueryable<TEntity>)this.dbSet).Where<TEntity>(predicate).AsQueryable<TEntity>();
        }

        //public virtual TEntity GetById(TId id, string propertyName)//temp
        //{
        //    ParameterExpression parameterExpression;
        //    return ((IQueryable<TEntity>)this.dbSet).Where<TEntity>(Expression.Lambda<Func<TEntity, bool>>((Expression)Expression.Equal((Expression)Expression.Property((Expression)parameterExpression, propertyName), (Expression)Expression.Constant((object)id)), parameterExpression)).SingleOrDefault<TEntity>();
        //}

        public IQueryable<TEntity> GetPaging(
          string FieldSort,
          bool FieldOption,
          int RowPerPage,
          int CurrentPage,
          string Keyword,
          List<string> SearchInField)
        {
            return BaseDA<TEntity, TId>.SortPaging<TEntity>((IQueryable<TEntity>)this.dbSet, FieldSort, FieldOption, RowPerPage, CurrentPage, Keyword, SearchInField, ref this.TotalRecord);
        }

        public virtual IQueryable<TEntity> GetPaging(
          Expression<Func<TEntity, bool>> predicate,
          string FieldSort,
          bool FieldOption,
          int RowPerPage,
          int CurrentPage,
          string Keyword,
          List<string> SearchInField)
        {
            return BaseDA<TEntity, TId>.SortPaging<TEntity>(((IQueryable<TEntity>)this.dbSet).Where<TEntity>(predicate), FieldSort, FieldOption, RowPerPage, CurrentPage, Keyword, SearchInField, ref this.TotalRecord);
        }

        public virtual IQueryable<TEntity> GetPaging(
          Expression<Func<TEntity, bool>> predicate,
          Expression<Func<TEntity, bool>> predicate2,
          string FieldSort,
          bool FieldOption,
          int RowPerPage,
          int CurrentPage,
          string Keyword,
          List<string> SearchInField)
        {
            return BaseDA<TEntity, TId>.SortPaging<TEntity>(((IQueryable<TEntity>)this.dbSet).Where<TEntity>(predicate).Where<TEntity>(predicate2), FieldSort, FieldOption, RowPerPage, CurrentPage, Keyword, SearchInField, ref this.TotalRecord);
        }

        public static IQueryable<T> SortPaging<T>(
          IQueryable<T> source,
          string FieldSort,
          bool FieldOption,
          int RowPerPage,
          int CurrentPage,
          string Keyword,
          List<string> SearchInField,
          ref int totalRecord)
        {
            Expression expression1 = source.Expression;
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (!string.IsNullOrEmpty(Keyword) && SearchInField.Count > 0)
            {
                ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
                MethodInfo method1 = typeof(string).GetMethod("Contains", new Type[1]
                {
          typeof (string)
                });
                MethodInfo method2 = typeof(string).GetMethod("ToLower", new Type[0]);
                Expression expression2 = (Expression)null;
                int num = 0;
                foreach (string propertyName in SearchInField)
                {
                    ++num;
                    if (num == 1)
                    {
                        Expression instance = (Expression)Expression.Call((Expression)Expression.Property((Expression)parameterExpression, propertyName), method2);
                        Expression expression3 = (Expression)Expression.Constant((object)Keyword, typeof(string));
                        expression2 = (Expression)Expression.Call(instance, method1, expression3);
                    }
                    else
                    {
                        Expression instance = (Expression)Expression.Call((Expression)Expression.Property((Expression)parameterExpression, propertyName), method2);
                        Expression expression3 = (Expression)Expression.Constant((object)Keyword, typeof(string));
                        expression2 = (Expression)Expression.OrElse((Expression)Expression.Call(instance, method1, expression3), expression2);
                    }
                }
                Expression expression4 = (Expression)Expression.Lambda<Func<T, bool>>(expression2, parameterExpression);
                expression1 = (Expression)Expression.Call(typeof(Queryable), "Where", new Type[1]
                {
          source.ElementType
                }, source.Expression, (Expression)Expression.Quote(expression4));
            }
            if (string.IsNullOrEmpty(FieldSort))
            {
                FieldSort = source.ElementType.GetProperties()[0].Name;
                FieldOption = true;
            }
            string propertyName1 = FieldSort;
            string methodName = FieldOption ? "OrderByDescending" : "OrderBy";
            ParameterExpression parameterExpression1 = Expression.Parameter(source.ElementType, string.Empty);
            MemberExpression memberExpression = Expression.Property((Expression)parameterExpression1, propertyName1);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)memberExpression, parameterExpression1);
            Expression expression5 = (Expression)Expression.Call(typeof(Queryable), methodName, new Type[2]
            {
        source.ElementType,
        memberExpression.Type
            }, expression1, (Expression)Expression.Quote((Expression)lambdaExpression));
            source = source.Provider.CreateQuery<T>(expression5);
            totalRecord = Queryable.Count<T>(source);
            if (CurrentPage > 0 && RowPerPage > 0)
            {
                Expression expression2 = (Expression)Expression.Call(typeof(Queryable), "Skip", new Type[1]
                {
          source.ElementType
                }, expression5, (Expression)Expression.Constant((object)((CurrentPage - 1) * RowPerPage)));
                source = source.Provider.CreateQuery<T>(expression2);
                Expression expression3 = (Expression)Expression.Call(typeof(Queryable), "Take", new Type[1]
                {
          source.ElementType
                }, expression2, (Expression)Expression.Constant((object)RowPerPage));
                source = source.Provider.CreateQuery<T>(expression3);
            }
            return source;
        }

        public static IQueryable<T> Sort<T>(
          IQueryable<T> source,
          string FieldSort,
          bool FieldOption,
          ref int totalRecord)
        {
            Expression expression1 = source.Expression;
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(FieldSort))
            {
                FieldSort = source.ElementType.GetProperties()[0].Name;
                FieldOption = true;
            }
            string propertyName = FieldSort;
            string methodName = FieldOption ? "OrderByDescending" : "OrderBy";
            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, string.Empty);
            MemberExpression memberExpression = Expression.Property((Expression)parameterExpression, propertyName);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)memberExpression, parameterExpression);
            Expression expression2 = (Expression)Expression.Call(typeof(Queryable), methodName, new Type[2]
            {
        source.ElementType,
        memberExpression.Type
            }, expression1, (Expression)Expression.Quote((Expression)lambdaExpression));
            source = source.Provider.CreateQuery<T>(expression2);
            totalRecord = Queryable.Count<T>(source);
            return source;
        }

        public virtual void Save()
        {
            this.dbContext.SaveChanges();
        }

        public void Free()
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException("Object Name");
        }

        public void Dispose()
        {
            if (this.dbContext != null)
            {
                this.Transaction.Dispose();
                this.Transaction = (DbTransaction)null;
                this.dbContext.Dispose();
                this.dbContext = (DbContext)null;
            }
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        ~BaseDA()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposedStatus)
        {
            if (this.IsDisposed)
                return;
            this.IsDisposed = true;
            if (!disposedStatus)
                ;
        }
    }
}
