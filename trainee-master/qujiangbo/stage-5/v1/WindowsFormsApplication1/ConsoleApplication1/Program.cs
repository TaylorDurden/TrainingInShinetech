using System;
using System.Collections.Generic;
using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;

namespace ConsoleApplication1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transcation = session.BeginTransaction())
                {
                    //var saveMakeDemo = false;
                    var makeDemo = new Make
                    {
                        Name = "makeDemo"
                    };
                    //var saveModelDemo = false;
                    var modelDemo = new Model
                    {
                        Name = "modelDemo",
                        Make = makeDemo
                    };
                    //modelDemo.Make = makeDemo;
                    var carDemo = new Car
                    {
                        Title = "carDemo",
                        Make = makeDemo,
                        Model = modelDemo
                    };


                    #region user and userDetail demo

                    var createTime = DateTime.ParseExact("2019-07-08　11:00", "yyyy-MM-dd　hh:ss", null);
                    var user = new User()
                    {
                        CreateTime = createTime,
                        Password = "123456",
                        UserName = "Jimbo"
                    };


                    //var detail = new UserDetail
                    //{
                    //    FirstName = "Bill",
                    //    LastName = "Gates",
                    //    LastUpdated = createTime,
                    //    User = user
                    //};
                    //user.UserDetail = detail;
                    var order = new Order
                    {
                        Address = "China BeiJing ",
                        CreateTime = DateTime.Now,
                        User = user
                    };
                    var order2 = new Order
                    {
                        Address = "China xi'an",
                        CreateTime = DateTime.Now,
                        User = user
                    };
                    var order3 = new Order
                    {
                        Address = "China xi'an",
                        CreateTime = DateTime.Now,
                        User = user
                    };
                    var orders = new List<Order> { order, order2, order3 };
                    user.Orders = orders;

                    var product=new Product
                    {
                        Name = "egg",
                        Price = 100,
                        CreateTime=createTime
                    };
                    var product2 = new Product
                    {
                        Name = "egg2",
                        Price = 102,
                        CreateTime = createTime
                    };
                    var product3 = new Product
                    {
                        Name = "egg3",
                        Price = 103,
                        CreateTime = createTime
                    };
                    var product4 = new Product
                    {
                        Name = "egg4",
                        Price = 104,
                        CreateTime = createTime
                    };
                    var products = new List<Product>{product,product2,product3,product4};
                    //var user4 = session.Load<User>(1);
                    var user4 = new User
                    {
                        CreateTime = createTime,
                        Password = "123456",
                        UserName = "Jimbo User4"
                    };

                    var order4 = new Order
                    {
                        Address = "China xi'an4",
                        CreateTime = DateTime.Now,
                        User = user4,
                        Products = products
                    };

                    var order5 = new Order
                    {
                        Address = "China xi'an 5",
                        CreateTime = DateTime.Now,
                        User = user4,
                        Products = products
                    };

                    #endregion

                    try
                    {
                        ////session.Save(user);
                        session.Save(order4);
                        session.Save(order5);
                        //session.Save(carDemo);
                        session.Flush();
                        transcation.Commit();
                        //Console.WriteLine("-----"+ user);
                        Console.ReadKey();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        transcation.Rollback();
                        Console.ReadKey();
                    }

                }
            }
        }
    }

    #region simple demo

    public class MakeMap : ClassMap<Make>
    {
        public MakeMap()
        {
            Id(x => x.MakeId).GeneratedBy.Identity();
            Map(x => x.Name).Nullable();
            //HasMany<Model>(x => x.Models).AsSet().KeyColumn("MakeId").Cascade.All();
        }
    }

    public class ModelMap : ClassMap<Model>
    {
        public ModelMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Nullable();
            References(x => x.Make).Cascade.All();
            //References<Make>(x => x.Make).Not.LazyLoad().Column("MakeId");
            //HasOne<Make>(x => x.Makes).Cascade.All();
            //References(x => x.Make).Cascade.All();

        }
    }
    
    public class CarMap : ClassMap<Car>
    {
        public CarMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Title).Nullable();
            References(x => x.Make).Cascade.All();
            References(x => x.Model).Cascade.All();
            //Map(x => x.MakeId);
            //Map(x => x.ModelId);
            //HasMany(x => x.Makes).KeyColumn("MakeId").Cascade.All().Inverse();
            //HasMany(x => x.Models).KeyColumn("ModelId").Cascade.All().Inverse();
        }
    }

    public class Make
    {
        public virtual int MakeId { get; set; }
        public virtual string Name { get; set; }

    }

    public class Model
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Make Make { get; set; }
    }

    public class Car
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual Make Make { get; set; }
        public virtual Model Model { get; set; }
        //public virtual int MakeId { get; set; }
        //public virtual int ModelId { get; set; }
        //public virtual ICollection<Make>  Makes { get; set; }
        //public virtual ICollection<Model> Models { get; set; }
    }

    #endregion

    #region user and userdetail demo

    public class User
    {
        public virtual int UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

    public class UserDetail
    {
        public virtual User User { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime LastUpdated { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        //public virtual PersonName PersonName { get; set; }
    }

    public class PersonName
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }

    public class Order
    {
        public virtual User User { get; set; }
        public virtual int OrderId { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual string Address { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        public virtual int ProductId { get; set; }
        public virtual string Name { get; set; }
        public virtual float Price { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Id(x => x.UserId).GeneratedBy.Identity();
            Map(x => x.UserName).Length(20).Nullable();
            Map(x => x.Password).Length(20).Nullable();
            Map(x => x.CreateTime).Nullable();
            //HasOne(x => x.UserDetail).Cascade.All().PropertyRef("User");
            HasOne(x => x.UserDetail).Cascade.All().Fetch.Select();
            HasMany(x => x.Orders).AsSet().KeyColumn("UserId").Cascade.All();

        }
    }

    public class UserDetailMapping : ClassMap<UserDetail>
    {
        public UserDetailMapping()
        {
            Id(x => x.UserId).Column("UserId").GeneratedBy.Foreign("User");
            HasOne(x => x.User).Cascade.All().Constrained();
            Map(x => x.FirstName).Column("[First　Name]").Length(20).Nullable();
            Map(x => x.LastName).Column("[Last　Name]").Length(20).Nullable();
            Map(x => x.LastUpdated).Nullable();

            //Component<PersonName>(u => u.Name, p =>
            //{
            //    p.Map(o => o.FirstName).Column("[First　Name]");
            //    p.Map(o => o.LastName).Column("[Last　Name]");
            //    });
        }
    }

    public class OrderMapping : ClassMap<Order>
    {
        public OrderMapping()
        {
            Id(x => x.OrderId).GeneratedBy.Identity();
            Map(x => x.Address).Nullable();
            Map(x => x.CreateTime).Nullable();
            //References(x => x.User).Not.LazyLoad().Column("UserId");
            HasManyToMany(x => x.Products)
                .AsSet()
                .Not.LazyLoad()
                .Cascade.All()
                .ParentKeyColumn("OrderId")
                .ChildKeyColumn("ProductId").Table("OrderProduct");
            References(x => x.User).Column("UserId").ForeignKey("UserId").Cascade.All();
        }
    }

    public class ProductMapping : ClassMap<Product>
    {
        public ProductMapping()
        {
            Id(x => x.ProductId).GeneratedBy.Identity();
            Map(x => x.Name).Nullable();
            Map(x => x.CreateTime).Nullable();
            HasManyToMany(x => x.Orders)
                .AsSet()
                .LazyLoad()
                .ParentKeyColumn("ProductId")
                .ChildKeyColumn("OrderId")
                .Table("OrderProduct");

        }
    }

    #endregion
}