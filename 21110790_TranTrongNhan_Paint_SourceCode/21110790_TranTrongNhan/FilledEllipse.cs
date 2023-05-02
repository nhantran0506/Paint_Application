using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21110790_TranTrongNhan
{
    internal class FilledEllipse : Ellipse
    {
        public override void Draw(Graphics g)
        {  
            g.FillEllipse(myBush, new System.Drawing.Rectangle(Start.X, Start.Y, End.X - Start.X, End.Y - Start.Y));
            base.Draw(g);

        }
    }
}
