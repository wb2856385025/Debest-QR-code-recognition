using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Repository
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDatabase.NHDataBase
 * File      Name：       Repository
 * Creating  Time：       10/9/2019 2:30:40 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDatabase.NHDataBase
{
    public class Repository : IDisposable
    {
        private static NHibernate.ISessionFactory _sessionfactory;
        [System.ThreadStatic]
        private static LocalDataStoreSlot _localDataStoreSlot;

        private NHibernate.ISession _session;
        private NHibernate.ITransaction _transaction;
        private NHibernate.IInterceptor _interceptor;
        private bool _isSessionCreator;

        /// <summary>
        /// 属性：ISession
        /// </summary>
        public NHibernate.ISession Session
        {
            get { return this._session; }
        }

        /// <summary>
        /// 属性:IsSessionCreator
        /// </summary>
        public bool IsSessionCreator
        {
            get { return this._isSessionCreator; }
        }

        /// <summary>
        /// 属性：mSession的状态IsOpen
        /// </summary>
        public bool IsOpen
        {
            get { return Repository.GetIsOpen(this._session); }
        }

        /// <summary>
        /// 方法:判断ISession状态
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private static bool GetIsOpen(NHibernate.ISession session)
        {
            if (session == null || !session.IsOpen)
            {
                return false;
            }
            // Session is open, connection could be either open or closed.
            // If connection is closed, signal that session is closed.
            if (!session.IsConnected)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Static constructor creates a new session factory used by all instances
        /// of this class.
        /// 构造函数(静态)：创建新的Session
        /// </summary>
        static Repository()
        {
            try
            {
                //加载数据库相关配置信息
                NHibernate.Cfg.Configuration config = new NHibernate.Cfg.Configuration().Configure(@"HibernateMapping.cfg.xml");
                //创建SessionFactory
                _sessionfactory = config.BuildSessionFactory();
                //创建存储本地数据的内存槽
                _localDataStoreSlot = System.Threading.Thread.AllocateDataSlot();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

        /// <summary>
        /// Initialized a new instance of the Repository class.
        /// 构造函数:创建Repository类实例
        /// </summary>
        public Repository()
        {
            this._isSessionCreator = true;
        }

        /// <summary>
        /// Initialized a new instance of the Repository class.
        /// 构造函数：创建Repository类实例，并传递IInterceptor实例
        /// </summary>
        /// <param name="interceptor">An instance of IInterceptor used to
        /// provide persistence pipeline processing on all persistent
        /// objects pass through it.</param>
        public Repository(NHibernate.IInterceptor interceptor)
            : this()
        {
            this._interceptor = interceptor;
        }

        /// <summary>
        /// Initialized a new instance of the Repository class.
        /// 构造函数：创建Repository类实例，并传递ISession实例
        /// </summary>
        /// <param name="session">An instance of NHibernate.ISession that
        /// will be used as the default session by the gateway.</param>
        /// <remarks>       
        public Repository(NHibernate.ISession session)
        {
            if (!Repository.GetIsOpen(session))
            {
                throw new Exception(String.Format("A {0} can only be constructed with an open session.", this.GetType().ToString()));
            }
            this._session = session;
        }

        /// <summary>
        /// Initialized a new instance of the Repository class.
        /// 构造函数：创建Repository类实例，并传递ISession实例和IInterceptor实例
        /// </summary>
        /// <param name="session">An instance of NHibernate.ISession that
        /// will be used as the default session by the gateway.</param>
        /// <remarks>
        /// <param name="interceptor">An instance of IInterceptor used to
        /// provide persistence pipeline processing on all persistent
        /// objects pass through it.</param>
        public Repository(NHibernate.ISession session, NHibernate.IInterceptor interceptor)
        {
            if (!Repository.GetIsOpen(session))
            {
                throw new Exception(String.Format("A {0} can only be constructed with an open session.", this.GetType().ToString()));
            }

            this._session = session;
            this._interceptor = interceptor;
        }


        /// <summary>
        /// 方法：在当前正在运行的线程上为此线程的当前域在指定槽中设置数据
        /// </summary>
        public static void TEnd()
        {
            System.Threading.Thread.SetData(_localDataStoreSlot, null);
        }

        /// <summary>
        /// 方法：获取Repository
        /// </summary>
        /// <returns></returns>
        static public Repository TRepository()
        {
            if (System.Threading.Thread.GetData(_localDataStoreSlot) == null)
                System.Threading.Thread.SetData(_localDataStoreSlot, new Repository());
            return System.Threading.Thread.GetData(_localDataStoreSlot) as Repository;
        }

        /// <summary>
        /// 方法：获取Repository
        /// </summary>
        /// <param name="MustNewRepository">是否新建的Repository</param>
        /// <returns></returns>
        static public Repository TRepository(bool MustNewRepository)
        {
            if (!MustNewRepository)
            {
                if (System.Threading.Thread.GetData(_localDataStoreSlot) == null)
                    System.Threading.Thread.SetData(_localDataStoreSlot, new Repository());
                return System.Threading.Thread.GetData(_localDataStoreSlot) as Repository;
            }
            else
            {
                return new Repository();
            }
        }

        /// <summary>
        /// 方法:获取Repository的ISession
        /// </summary>
        /// <returns></returns>
        static public NHibernate.ISession TSession()
        {
            Repository r = TRepository();
            r.Open();
            return r.Session;
        }

        /// <summary>
        /// 方法：获取Repository的ISession
        /// </summary>
        /// <param name="MustNewSession">是否新建的ISession</param>
        /// <returns></returns>
        static public NHibernate.ISession TSession(bool MustNewSession)
        {
            Repository r = TRepository(true);
            r.Open();
            return r.Session;
        }

        /// <summary>
        /// Opens the gateway.
        /// 方法：打开连接通道
        /// </summary>
        /// <returns>Gateway is open when session is open and connection is open.
        /// </returns>
        public bool Open()
        {
            if (!this._isSessionCreator)
            {
                throw new Exception("A gateway that is sharing the session of another gateway cannot be openned directly.");
            }

            bool wasOpen = true;

            if (this._session == null || !this._session.IsOpen)
            {
                wasOpen = false;

                if (this._interceptor != null)
                {
                    this._session = _sessionfactory.OpenSession(this._interceptor);
                }
                else
                {
                    this._session = _sessionfactory.OpenSession();
                }
            }

            // Session is open, connection could be either open or closed.
            // If connection is closed, open it. Indicate that session was
            // open.
            if (!this._session.IsConnected)
            {
                wasOpen = false;
                this._session.Reconnect();
            }

            return wasOpen;
        }

        public void Close()
        {
            if (!this._isSessionCreator)
            {
                throw new Exception("A gateway that is sharing the session of another gateway cannot be closed directly.");
            }

            if (this._session == null)
            {
                return;
            }

            if (this._session.IsConnected)
            {
                this._session.Disconnect();
            }

            if (this._session.IsOpen)
            {
                this._session.Close();
            }
        }

        public void BeginTransaction()
        {
            if (!this._isSessionCreator)
            {
                throw new Exception("A gateway that is sharing the session of another gateway cannot start a transaction on the shared session.");
            }

            if (!this.IsOpen)
            {
                throw new InvalidOperationException("Repository must be open before a transaction can be started on the gateway.");
            }

            if (this._transaction == null)
            {
                this._transaction = this._session.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            if (!this._isSessionCreator)
            {
                throw new Exception("A gateway that is sharing the session of another gateway cannot commit a transaction on the shared session.");
            }

            if (!this.IsOpen)
            {
                throw new InvalidOperationException("Repository must be open before a transaction can be committed.");
            }

            if (this._transaction == null)
            {
                throw new InvalidOperationException("Repository must have an open transaction in order to commit.");
            }

            if (this._transaction.IsActive)
                this._transaction.Commit();
            this._transaction = null;
        }

        public void Rollback()
        {
            if (!this._isSessionCreator)
            {
                throw new Exception("A gateway that is sharing the session of another gateway cannot roll back a transaction on the shared session.");
            }
            if (!this.IsOpen)
            {
                throw new InvalidOperationException("Repository must be open before a transaction can be rolled back.");
            }

            if (this._transaction != null)
            {
                if (this._transaction.IsActive)
                    this._transaction.Rollback();
                this._transaction = null;
            }
        }

        /// <summary>
        /// 待定(判断实体是否存在)
        /// </summary>
        /// <param name="obj">表</param>
        /// <param name="id">关键字</param>
        /// <returns></returns>
        public static bool IsPersisted(object obj, object id)
        {
            using (NHibernate.ISession session = Repository._sessionfactory.OpenSession())
            {
                try
                {
                    object ot = typeof(Type);
                    Type t = (obj.GetType() == ot.GetType()) ? (Type)obj : obj.GetType();
                    object o = session.Load(t, id);
                    string s = o.ToString();
                    return true;
                }
                catch { }
                return false;
            }
        }

        public void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                this.Close();

                if (this._transaction != null)
                {
                    this._transaction.Dispose();
                    this._transaction = null;
                }

                if (this._session != null)
                {
                    this._session.Dispose();
                    this._session = null;
                }

                this._interceptor = null;

                if (System.Threading.Thread.GetData(_localDataStoreSlot) == this)
                    System.Threading.Thread.SetData(_localDataStoreSlot, null);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            Dispose(false);
        }
    }
}
