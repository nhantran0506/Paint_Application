using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _21110790_TranTrongNhan
{
    internal abstract class GraphicObject
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public  Pen myPen = new Pen(Color.Black);
        public  Brush myBush= new SolidBrush(Color.Black);
        public bool isSelected { get; set; }
        public bool delete { get; set; }


        private GraphicsPath path = new GraphicsPath();
        private List<PointF> spareDots = new List<PointF>();
        private float spareDotSize = 20;

        public virtual void Draw(Graphics g)
        {
            path.Reset();

            Rectangle rect = new Rectangle(Start.X, Start.Y, End.X - Start.X, End.Y - Start.Y);
            path.AddRectangle(rect);
            path.Flatten();
            if (isSelected)
            {
                IsSelecting(g);
            }
        }
        public virtual bool Contain(Point p)
        {
            if (path.IsVisible(p)) return true;
            return false;
        }
        
        public virtual void IsSelecting(Graphics g)
        {
                Pen border = new Pen(Color.Black);
                border.DashStyle = DashStyle.Dot;


                RectangleF bounds = path.GetBounds();
                PointF topLeft = new PointF(bounds.Left, bounds.Top);
                PointF topRight = new PointF(bounds.Right, bounds.Top);
                PointF bottomLeft = new PointF(bounds.Left, bounds.Bottom);
                PointF bottomRight = new PointF(bounds.Right, bounds.Bottom);
                spareDots.Clear();
                spareDots.Add(topLeft);
                spareDots.Add(topRight);
                spareDots.Add(bottomLeft);
                spareDots.Add(bottomRight);

                g.DrawPath(border, path); // Draw the path with a black pen
                g.DrawRectangle(border, topLeft.X - spareDotSize / 2, topLeft.Y - spareDotSize / 2, spareDotSize, spareDotSize);
                g.DrawRectangle(border, topRight.X - spareDotSize / 2, topRight.Y - spareDotSize / 2, spareDotSize, spareDotSize);
                g.DrawRectangle(border, bottomLeft.X - spareDotSize / 2, bottomLeft.Y - spareDotSize / 2, spareDotSize, spareDotSize);
                g.DrawRectangle(border, bottomRight.X - spareDotSize / 2, bottomRight.Y - spareDotSize / 2, spareDotSize, spareDotSize);

            


        }
        public virtual void UpdateDraging(Point p,int index)
        {
            if (End.X - Start.X <= spareDotSize * 2 || End.Y - Start.Y <= spareDotSize * 2)
            {
                if(p.X > Start.X && p.X< End.X)
                {
                    return;
                }
                if(p.Y > Start.Y && p.Y< End.Y) { 
                    return;
                }
            }
            switch (index)
            {
                case 0:
                    Start = p;
                    break;
                case 1:
                    End = new Point(p.X, End.Y);
                    Start = new Point(Start.X, p.Y);
                    break;
                case 2:
                    End = new Point(End.X, p.Y);
                    Start = new Point(p.X, Start.Y);
                    break;
                case 3:
                    End = p;
                    break;
                default:
                    break;

            }

            
            

        }
        public virtual int GrabIndex(Point p)
        {
            
                for (int i = 0; i < spareDots.Count(); i++)
                {


                    float dx = p.X - this.spareDots[i].X;
                    float dy = p.Y - this.spareDots[i].Y;
                    float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                    if (dist <= spareDotSize)
                    {

                        return i;
                    }
                }
            return -1;
            
        }
        public virtual bool isNearSpareDot(Point p)
        {
            
            if(isSelected)
            {
                for (int i = 0; i< spareDots.Count(); i++)
                {

                    
                    float dx = p.X - this.spareDots[i].X;
                    float dy = p.Y - this.spareDots[i].Y;
                    float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                    if (dist <= spareDotSize)
                    {

                        return true;
                    }
                }
            }
            
            return false;
        }


        public virtual void BeGrouping(Point s,Point e)
        {
            if(s.X < Start.X && s.Y < Start.Y)
            {
                if(e.X > End.X && e.Y > End.Y)
                {
                    this.isSelected = true;
                }
            }
        }

    }
}
