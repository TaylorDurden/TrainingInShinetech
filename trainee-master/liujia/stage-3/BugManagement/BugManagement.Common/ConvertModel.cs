using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.Common
{
    public static class ConvertModel
    {
        /// <summary>
        /// ConvertMoudle
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourle"></param>
        /// <param name="to"></param>
        public static void ConvertMoudle<K, T>(K sourle, T to) where T : class
        {
            foreach (System.Reflection.PropertyInfo info in typeof(K).GetProperties())
            {
                foreach (System.Reflection.PropertyInfo infotemp in typeof(T).GetProperties())
                {
                    if (info.Name.Equals(infotemp.Name))
                    {
                        infotemp.SetValue(to, info.GetValue(sourle, null), null);
                    }
                }
            }
        }
    }
}
