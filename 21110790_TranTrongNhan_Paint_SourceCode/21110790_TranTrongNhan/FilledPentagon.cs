using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21110790_TranTrongNhan
{
    internal class FilledPentagon : Pentagon
    {
        public override void Draw(Graphics g)
        {
            
            g.FillPolygon(myBush, MyPoint);
            base.Draw(g);
        }
    }
}
