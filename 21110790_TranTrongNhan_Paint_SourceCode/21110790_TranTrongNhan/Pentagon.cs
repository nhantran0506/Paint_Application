using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _21110790_TranTrongNhan
{
    internal class Pentagon : Polygon
    {
        public Pentagon()
        {
            MyPoint = new Point[5];
            for (int i = 0; i < 5; i++)
            {
                MyPoint[i] = new Point();
            }

        }

        public override void Draw(Graphics g)
        {
            
            g.DrawPolygon(myPen, MyPoint);
            base.Draw(g);
        }

        


        public override void UpdateCordinate()
        {
             
            MyPoint[2].Y = Start.Y + (End.Y - Start.Y) / 3; //  2 and 4 is right and left middle
            MyPoint[2].X = End.X;
            MyPoint[4].Y = MyPoint[2].Y;
            MyPoint[4].X = Start.X;
            MyPoint[3].X = (MyPoint[2].X + MyPoint[4].X) / 2;
            MyPoint[3].Y = Start.Y;
            MyPoint[0].X = Start.X + (End.X - Start.X) / 6;
            MyPoint[0].Y = End.Y; // 1 is botttom left
            MyPoint[1].X = Start.X + (End.X - Start.X) * 5 / 6;
            MyPoint[1].Y = MyPoint[0].Y;

           
        }
       
    }
}
