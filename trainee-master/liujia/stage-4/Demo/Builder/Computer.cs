using System;
using System.Collections.Generic;

namespace Builder
{
    //电脑类
    public class Computer
    {
        //电脑组件集合
        private IList<string> parts = new List<string>();

        //把单个组件添加到电脑组件集合中
        public void Add(string part) {
            parts.Add(part);
        }

        public void Show() {
            Console.WriteLine("电脑开始在组装......");
            foreach (var part in parts) {
                Console.WriteLine("组件{0}已装好",part);
            }
            Console.WriteLine("电脑组装好了");
        }
    }
}
