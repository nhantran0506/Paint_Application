using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21110790_TranTrongNhan
{
    internal class Line : GraphicObject
    {
        public override void Draw(Graphics g)
        {

            g.DrawLine(myPen, Start, End);
            base.Draw(g);
        }  
    }
}
