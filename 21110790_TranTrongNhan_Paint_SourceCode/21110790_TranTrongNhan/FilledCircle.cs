using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21110790_TranTrongNhan
{
    internal class FilledCircle : Circle
    {
        public override void Draw(Graphics g)
        {
            float length = Math.Max(End.X - Start.X, End.Y - Start.Y);
            g.FillEllipse(myBush, new System.Drawing.Rectangle(Start.X, Start.Y, (int)length, (int)length));
            base.Draw(g);

        }
    }
}
