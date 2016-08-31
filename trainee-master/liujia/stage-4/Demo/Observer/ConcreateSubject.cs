
namespace Observer
{
    public delegate void MsgEvent();
    /// <summary>
    /// 具体主题
    /// </summary>
    public class ConcreateSubject:ISubject
    {
        /// <summary>
        /// 定义委托事件
        /// </summary>
        public event MsgEvent MsgAction;

        public void Notify() {
            if (MsgAction != null) {
                MsgAction();
            }
        }
    }
}
