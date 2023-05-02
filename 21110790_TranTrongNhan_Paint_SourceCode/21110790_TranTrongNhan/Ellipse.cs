using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace _21110790_TranTrongNhan
{
    internal class Ellipse : GraphicObject
    {
    
        public override void Draw(Graphics g)
        {

            g.DrawEllipse(myPen, Start.X, Start.Y, End.X - Start.X, End.Y - Start.Y);
            base.Draw(g);           
        }
    }
}
