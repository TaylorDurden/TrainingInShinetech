using NHibernate.Cfg;
using NHibernate;

namespace PlanPoker.Repository
{
    public sealed class NHibernateHelper
    {
        private NHibernateHelper(string path)
        {
            _path = path;
        }
        private static readonly object LockHelper = new object();

        private static NHibernateHelper _instance;

        public static NHibernateHelper Instance(string dbPath)
        {
            if (_instance != null) return _instance;
            lock (LockHelper)
            {
                _instance = new NHibernateHelper(dbPath);
            }
            return _instance;
        }

        private readonly string _path;

        private ISession _session;

        public ISession Session
        {
            get
            {
                if (_session != null && _session.IsOpen) return _session;
                var sf = (new Configuration()).Configure(_path).BuildSessionFactory();
                _session = sf.OpenSession();
                return _session;
            }
        }

        public void Clear()
        {
            _session.Clear();
        }
        
        public bool Contains(object args)
        {
            return _session.Contains(args);
        }
        public void Evict(object args)
        {
            _session.Evict(args);
        }
    }
}
