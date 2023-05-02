using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21110790_TranTrongNhan
{
    internal class Triangle : Polygon
    {
        public Triangle()
        {
            MyPoint = new Point[3];
            for (int i=0;i< 3; i++)
            {
                MyPoint[i] = End;
            }

        }
        public override void Draw(Graphics g)
        {
          
            g.DrawPolygon(myPen, MyPoint);
            base.Draw(g);
        }
        public override void UpdateCordinate()
        {
            MyPoint[0] = End;
            MyPoint[1].Y = MyPoint[0].Y;
            MyPoint[1].X = Start.X;
            MyPoint[2].X = (MyPoint[1].X + MyPoint[0].X) / 2;
            MyPoint[2].Y = Start.Y;
        }

      

    }
}
