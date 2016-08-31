using System.Collections.Generic;

namespace Observer
{
    /// <summary>
    /// 抽象主题
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// 发送通知
        /// </summary>
        void Notify();
    }
}
