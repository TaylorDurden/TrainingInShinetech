using System;

namespace Features
{
    public class Point
    {
        //自动属性的初始化方式
        public int X { get; set; }
        public int Y { get; set; }
        
        public Point():this(5,7)
        {

        }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public double Distance => Math.Sqrt(X*X + Y*Y);//属性表达式
        //用Lambda作为函数体Expression bodies on method-like members
        public Point MovePoint(int dx,int dy) => new Point(X+dx,Y+dy);//函数表达式
    }
}