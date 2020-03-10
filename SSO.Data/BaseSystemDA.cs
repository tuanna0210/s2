//using Authentication; //temp
using MvcAuthenication;
using SSO.Base;
using SSO.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public interface IBaseSystemDA<TEntity>
    {
        ///// <summary>
        ///// Add object
        ///// </summary>
        ///// <returns>Return object Mesage</returns>
        //Message Add();

        ///// <summary>
        ///// Edit object
        ///// </summary>
        ///// <returns>Return object Mesage</returns>
        //Message Edit();

        ///// <summary>
        ///// Delete object
        ///// </summary>
        ///// <returns>Return object Mesage</returns>
        //Message Delete();

        ///// <summary>
        ///// check exists item
        ///// </summary>
        ///// <param name="itemid"></param>
        ///// <returns>bool</returns>
        //bool CheckExits(int itemid);

        /// <summary>
        /// Return simple object by ID
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns>object</returns>
        //TEntity GetByID(int itemid);

        //List<TEntity> GetListDanhMucPagingWithSearch();
    }
    public sealed class SingletonDBCTX
    {
        private static volatile System.Data.Entity.DbContext instance;
        private static object syncRoot = new Object();

        private SingletonDBCTX() { }

        public static System.Data.Entity.DbContext Instance
        {
            get
            {
                //if (instance == null)//temp
                //{
                //    lock (syncRoot)
                //    {
                //        if (instance == null)
                //            instance = new Base.CMSEntities();
                //    }
                //}
                return instance;
            }
        }
    }
    public class BaseSystemDA<TEntity, TId> : BaseDA<TEntity, TId> where TEntity : class
    {
        //public BaseSystemDA()
        //{
        //    DBContext = SingletonDBCTX.Instance;

        //    //var database = SystemAuthenticate.GetCustomerDatabase();
        //    //if (database!=null)
        //    //    DBContext = BaseDatabase.GetDBContext<iDasSystemDataEntities>(database);
        //}
        public BaseSystemDA(bool sync = false)
        {
            if (sync)
            {
                //DBContext = SingletonDBCTX.Instance;//temp
                DBContext = null;// new SSODbContext();
            }
            else
            {
                DBContext = null;// new SSODbContext();
                //DBContext = new SSO.Base.CMSEntities(); //temp
            }

            //var database = SystemAuthenticate.GetCustomerDatabase();
            //if (database!=null)
            //    DBContext = BaseDatabase.GetDBContext<iDasSystemDataEntities>(database);
        }
        //temp
        //public Base.CMSEntities CmsContext
        //{
        //    get
        //    {
        //        return (Base.CMSEntities)DBContext;
        //    }
        //}

        public List<ComboboxItem> Combobox(IQueryable<TEntity> l)
        {
            List<ComboboxItem> lst = new List<Base.ComboboxItem>();
            Base.ComboboxItem item;
            foreach (TEntity e in l)
            {
                item = new Base.ComboboxItem();
                foreach (var p in e.GetType().GetProperties())
                {
                    if (p.Name.ToUpper() == "ID")
                    {
                        item.ID = Convert.ToInt32(p.GetValue(e, null));
                    }
                    if (p.Name.ToUpper() == "NAME")
                    {
                        item.Name = Convert.ToString(p.GetValue(e, null));
                    }
                }
                lst.Add(item);
            }
            return lst;
        }

        public Message objMsg = new Message() { Error = false };
    }
}
