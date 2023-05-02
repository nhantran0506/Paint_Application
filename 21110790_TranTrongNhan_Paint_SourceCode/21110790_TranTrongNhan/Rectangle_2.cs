using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21110790_TranTrongNhan
{
    internal class Rectangle_2:GraphicObject
    {
        public override void Draw(Graphics g)
        {
            
           
            g.DrawRectangle(myPen, new System.Drawing.Rectangle(Start.X, Start.Y, End.X - Start.X, End.Y - Start.Y));
            base.Draw(g);
        }

        

       
    }
}
