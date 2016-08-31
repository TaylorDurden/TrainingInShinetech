using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LinqToOjectsDemo
{
    public class LinqToFileInfo
    {
        public IEnumerable<FileInfo> LinqToFileInfoTest()
        {
            const string path = @"D:\projectDemo\qujiangbo\traineeCurrent\trainee\qujiangbo\stage-5\v1\Planpoker-FluentNHibernate\PlanPoker\PlanPoker.WebAPI\bin";
            var files = GetFiles(path);

            var queryResult = from file in files
                where file.Extension == ".dll"
                orderby file.CreationTime descending
                select file;

            return queryResult;
        }

        public IEnumerable<FileInfo> GetFiles(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("The directory doesn't exist!");
            }

            string[] fileNames = null;

            fileNames = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            return fileNames.Select(filename => new FileInfo(filename)).ToList();
            //foreach (var fileName in fileNames)
            //{
            //    fileList.Add(new FileInfo(fileName));
            //}
            //return fileList;

        }
    }
}
