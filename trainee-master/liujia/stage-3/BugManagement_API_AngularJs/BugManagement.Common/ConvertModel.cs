
namespace BugManagement.Common
{
    public static class ConvertModel
    {
        /// <summary>
        /// ConvertMoudle
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourle"></param>
        /// <param name="to"></param>
        public static void ConvertMoudle<TK, T>(TK sourle, T to) where T : class
        {
            foreach (System.Reflection.PropertyInfo info in typeof(TK).GetProperties())
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
