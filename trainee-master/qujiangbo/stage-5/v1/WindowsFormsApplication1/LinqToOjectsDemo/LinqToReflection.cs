using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LinqToOjectsDemo
{
    public class LinqToReflection
    {

        public IEnumerable<IGrouping<Type,MethodInfo>> LinqToReflectionTest()
        {
            const string file = @"D:\projectDemo\qujiangbo\traineeCurrent\trainee\qujiangbo\stage-5\v1\Planpoker-FluentNHibernate\PlanPoker\PlanPoker.WebAPI\bin\PlanPoker.Logic.dll";

            var assembly = Assembly.LoadFrom(file);

            var queryResult = from type in assembly.GetTypes()
                where type.IsPublic
                from method in type.GetMethods()
                where method.IsPublic
                group method by type;

            return queryResult;
        }
    }
}
