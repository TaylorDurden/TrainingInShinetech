using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugManagement.Installer
{
    public interface ICacheManger
    {
        void Add(string key, object value);
        void Remove(string key);
        T Get<T>(string key);
        bool KeyExist(string key);
    }
}