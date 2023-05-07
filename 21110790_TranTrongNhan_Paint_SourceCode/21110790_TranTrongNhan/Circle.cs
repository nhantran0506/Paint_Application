using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _21110790_TranTrongNhan
{
    internal class Circle : GraphicObject
    {
        public override void Draw(Graphics g)
        {
            float length = Math.Max(End.X - Start.X, End.Y - Start.Y);
            g.DrawEllipse(myPen, Start.X, Start.Y, length, length);
            base.Draw(g);
        }
        
    }
}
