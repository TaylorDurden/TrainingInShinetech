using System;

namespace Features
{
    public class Point
    {
        //�Զ����Եĳ�ʼ����ʽ
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
        
        public double Distance => Math.Sqrt(X*X + Y*Y);//���Ա��ʽ
        //��Lambda��Ϊ������Expression bodies on method-like members
        public Point MovePoint(int dx,int dy) => new Point(X+dx,Y+dy);//�������ʽ
    }
}