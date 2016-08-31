using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using LoveDemo.Models;

namespace LoveDemo.DomainModels
{
    public class LoveRepository: ILoveRepository
    {
        public List<Person> GetLoversByName(string name)
        {
            throw new NotImplementedException();
            //return new List<Person>
            //{
            //    new Person {Name = "Lucy",Age = 25},
            //    new Person { Name = "Lily",Age = 23}
            //};
        }
    }
}