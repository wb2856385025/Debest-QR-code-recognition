using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Orm
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDatabase.NHibernate
 * File      Name：       Orm
 * Creating  Time：       10/9/2019 2:27:59 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDatabase.NHDataBase
{
    public class Orm
    {
        /// <summary>
        /// 方法:获取新建的Session
        /// </summary>
        /// <returns></returns>
        static public NHibernate.ISession Session()
        {
            return ProDatabase.NHDataBase.Repository.TSession(true);
        }

        /// <summary>
        /// 方法：获取新建的Repository
        /// </summary>
        /// <returns></returns>
        static public ProDatabase.NHDataBase.Repository Repository()
        {
            return ProDatabase.NHDataBase.Repository.TRepository(true);
        }

        /// <summary>
        /// 方法:获取新建ISession的数据库连接
        /// </summary>
        /// <returns></returns>
        static public System.Data.SqlClient.SqlConnection SqlConnection()
        {

            return ProDatabase.NHDataBase.Repository.TSession(true).Connection as System.Data.SqlClient.SqlConnection;
        }

        /// <summary>
        /// 方法:获取新建ISession的数据库连接字串
        /// </summary>
        /// <returns></returns>
        static public string DBConnectString()
        {
            return ProDatabase.NHDataBase.Repository.TSession(true).Connection.ConnectionString;
        }

        /// <summary>
        /// 方法：判断主键是否存在
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        static public bool IsPersisted(object obj, object id)
        {
            return ProDatabase.NHDataBase.Repository.IsPersisted(obj, id);
        }

        /// <summary>
        /// 方法：保存对象
        /// </summary>
        /// <param name="obj"></param>
        static public void Save(object obj)
        {
            object o = new object();
            lock (o)
            {
                try
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Open();
                    ProDatabase.NHDataBase.Repository.TRepository().BeginTransaction();
                    ProDatabase.NHDataBase.Repository.TRepository().Session.Save(obj);
                    ProDatabase.NHDataBase.Repository.TRepository().CommitTransaction();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Rollback();
                }
                finally
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Close();
                }
            }
        }

        /// <summary>
        /// 方法：保存或更新对象
        /// </summary>
        /// <param name="obj"></param>
        static public void SaveOrUpdate(object obj)
        {
            object o = new object();
            lock (o)
            {
                try
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Open();
                    ProDatabase.NHDataBase.Repository.TRepository().BeginTransaction();
                    ProDatabase.NHDataBase.Repository.TRepository().Session.SaveOrUpdate(obj);
                    ProDatabase.NHDataBase.Repository.TRepository().CommitTransaction();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Rollback();
                }
                finally
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Close();
                }
            }
        }

        /// <summary>
        /// 方法：更新对象
        /// </summary>
        /// <param name="obj"></param>
        static public void Update(object obj)
        {
            object o = new object();
            lock (o)
            {
                try
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Open();
                    ProDatabase.NHDataBase.Repository.TRepository().BeginTransaction();
                    ProDatabase.NHDataBase.Repository.TRepository().Session.Update(obj);
                    ProDatabase.NHDataBase.Repository.TRepository().CommitTransaction();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Rollback();
                }
                finally
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Close();
                }
            }
        }

        /// <summary>
        /// 方法：新增对象 //与Save一样，需要修改
        /// </summary>
        /// <param name="obj"></param>
        public static void Add(object obj)
        {
            object o = new object();
            lock (o)
            {
                try
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Open();
                    ProDatabase.NHDataBase.Repository.TRepository().BeginTransaction();
                    ProDatabase.NHDataBase.Repository.TRepository().Session.Save(obj);
                    ProDatabase.NHDataBase.Repository.TRepository().CommitTransaction();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Rollback();
                }
                finally
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Close();
                }
            }
        }
        /// <summary>
        /// 方法：删除对象
        /// </summary>
        /// <param name="obj"></param>
        public static void Delete(object obj)
        {
            object o = new object();
            lock (o)
            {
                try
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Open();
                    ProDatabase.NHDataBase.Repository.TRepository().BeginTransaction();
                    ProDatabase.NHDataBase.Repository.TRepository().Session.Delete(obj);
                    ProDatabase.NHDataBase.Repository.TRepository().CommitTransaction();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Rollback();
                }
                finally
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Close();
                }
            }
        }

        /// <summary>
        /// 方法:删除数据库连接
        /// </summary>
        /// <param name="sql"></param>
        public static void Delete(string sql)
        {
            object o = new object();
            lock (o)
            {
                try
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Open();
                    ProDatabase.NHDataBase.Repository.TRepository().BeginTransaction();
                    ProDatabase.NHDataBase.Repository.TRepository().Session.Delete(sql);
                    ProDatabase.NHDataBase.Repository.TRepository().CommitTransaction();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Rollback();
                }
                finally
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Close();
                }
            }
        }
    }


    public class Orm<T>
    {

        #region 单例

        /// <summary>
        /// 静态实例
        /// </summary>
        /// 
        private static Orm<T> _instance = new Orm<T>();

        /// <summary>
        /// 静态属性:实例
        /// </summary>
        public static Orm<T> Instance
        {
            get { return _instance; }
        }
        #endregion

        public Orm()
        {
        }

        /// <summary>
        /// 判断主键是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsPersisted(object id)
        {
            return ProDatabase.NHDataBase.Repository.IsPersisted(typeof(T), id);
        }

        /// <summary>
        /// 方法:通过关键字id获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(object id)
        {
            lock (this)
            {
                T obj = default(T);
                try
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Open();
                    obj = ProDatabase.NHDataBase.Repository.TRepository().Session.Get<T>(id);
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Rollback();
                }
                finally
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Close();
                }
                return obj;
            }
        }

        /// <summary>
        /// 方法：获取对象列表(BindingList)
        /// </summary>
        /// <returns></returns>
        public System.ComponentModel.BindingList<T> GetBList()
        {
            return (new System.ComponentModel.BindingList<T>(this.GetList()));
        }


        /// <summary>
        /// 获取对象列表(List)
        /// </summary>
        /// <returns></returns>
        public System.Collections.Generic.IList<T> GetList()
        {
            lock (this)
            {
                IList<T> objList = null;
                try
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Open();
                    objList = ProDatabase.NHDataBase.Repository.TRepository().Session.CreateCriteria(typeof(T)).List<T>();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {

                }
                finally
                {
                    ProDatabase.NHDataBase.Repository.TRepository().Close();
                }
                return objList;
            }
        }

        /// <summary>
        /// 属性:条件
        /// </summary>
        public NHibernate.ICriteria Criteria
        {
            get
            {
                ProDatabase.NHDataBase.Repository.TRepository().Open();
                return ProDatabase.NHDataBase.Repository.TRepository().Session.CreateCriteria(typeof(T));
            }
        }
    }
}
