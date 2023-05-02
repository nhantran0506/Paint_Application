using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21110790_TranTrongNhan
{
    internal class FilledRectangle : Rectangle_2
    {
        public override void Draw(System.Drawing.Graphics g)
        {
            
            g.FillRectangle(myBush, new System.Drawing.Rectangle(Start.X, Start.Y, End.X - Start.X, End.Y - Start.Y));
            base.Draw(g);
        }
    }
}
