
using System.Collections.Generic;
using System.Linq;
using PlanPoker.IRepository;
using NHibernate.Linq;

namespace PlanPoker.Repository
{
    /// <summary>
    /// 这个类是用现在比较流行的Repository企业模式，这样代码的内聚就比较高了。
    /// 因为NHibernate的增、删、改的写法都一样。所以没有必要学petshop里那样一次写n个类。
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public class RepositoryBase<T> :IRepository<T> where T : class
    {
        //public string ConfigPath { get; set; }
        private const string ConfigPath =
            "D:/Jimbo/trainee/qujiangbo/stage-4/v2/Planpoker-NHibernate/PlanPoker/PlanPoker.Data/Nhibernate.cfg.xml";
            //"D:/projectDemo/qujiangbo/traineeCurrent/trainee/qujiangbo/stage-4/v2/Planpoker-NHibernate/PlanPoker/PlanPoker.Data/Nhibernate.cfg.xml";
        public void Create(T model)
        {
            var helper = NHibernateHelper.Instance(ConfigPath);
            helper.Session.Save(model);
        }

        public void Delete(object id)
        {
            var helper = NHibernateHelper.Instance(ConfigPath);
            helper.Session.Delete(id);
        }

        public void Edit(T model)
        {
            var helper = NHibernateHelper.Instance(ConfigPath);
            helper.Session.Update(model);
        }

        public T Get(object id)
        {
            var helper = NHibernateHelper.Instance(ConfigPath);
            return helper.Session.Get<T>(id);
        }

        //public IList<T> GetList(int firstResult, int maxResult)
        //{
        //    var helper = NHibernateHelper.Instance(ConfigPath);
        //    ICriteria crit = helper.Session.CreateCriteria(typeof(T));
        //    crit.Skip(firstResult).Take(maxResult);
        //    return crit.List<T>();
        //}

        public IList<T> Query()
        {
            var helper = NHibernateHelper.Instance(ConfigPath);
            return helper.Session.Query<T>().ToList();
        }

        //private readonly IDbFactory _dbFactory;

        //public RepositoryBase(IDbFactory dbFactory)
        //{
        //    _dbFactory = dbFactory;
        //}

        //public void Create(T model)
        //{
        //    DbSet.Add(model);
        //}
        //public void Delete(int id)
        //{
        //    DbSet.Remove(DbSet.Find(id));
        //}
        //public IQueryable<T> Query()
        //{
        //    return DbSet.AsQueryable();
        //}
        //public T Get(int id)
        //{
        //    return DbSet.Find(id);
        //}

        //public void Edit(T model)
        //{
        //    DataContext.Entry(model).State = EntityState.Modified;
        //}

        //private DbContext DataContext => _dbFactory.GetContext();
        //private IDbSet<T> DbSet => DataContext.Set<T>();
    }
}
