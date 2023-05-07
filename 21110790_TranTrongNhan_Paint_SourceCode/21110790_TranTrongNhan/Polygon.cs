using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _21110790_TranTrongNhan
{
    internal abstract class Polygon: GraphicObject
    {
        protected Point[] MyPoint;
        
        public abstract void UpdateCordinate();
        public virtual void UpdatePara(int dx, int dy)
        {
            Start = new Point(Start.X + dx, Start.Y + dy);
           End = new Point(End.X + dx, End.Y + dy);
            for (int i = 0; i < MyPoint.Count(); i++)
            {
                
                MyPoint[i].X = MyPoint[i].X + dx;
                MyPoint[i].Y = MyPoint[i].Y + dy;


            }
        }
    }
}
