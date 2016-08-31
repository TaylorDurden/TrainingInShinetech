using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoveDemo.Models;

namespace LoveDemo.DomainModels
{
    public interface ILoveRepository
    {
        List<Person> GetLoversByName(string name);
    }
}