using System;

namespace Proxy
{
    public class Friend:Person
    {
        //引用真实主题实例
        RealBuyPerson realBuyPerson;

        public override void BugProduct()
        {
            Console.WriteLine("通过代理类访问真实实体对象的方法");
            if (realBuyPerson == null) {
                realBuyPerson = new RealBuyPerson();
            }
            PreBuyProduct();
            realBuyPerson.BugProduct();
            PostBuyProduct();
        }

        public void PreBuyProduct() {
            Console.WriteLine("我怕弄糊涂了，需要列一张清单，张三：带相机，李四：带手机。。。");
        }

        public void PostBuyProduct() {
            Console.WriteLine("终于买完了，对一下东西，相机是张三的，手机是李四的。。。");
        }
    }
}
