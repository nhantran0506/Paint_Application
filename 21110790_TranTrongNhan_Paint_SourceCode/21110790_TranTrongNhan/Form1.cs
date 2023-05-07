using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _21110790_TranTrongNhan
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pictureBox1.Image = bm;
            InnitValue();



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdatePara();
            this.DoubleBuffered = true;

        }


        private ColorDialog cd = new ColorDialog();
        private Bitmap bm;
        private Graphics g;
        private bool isPaiting = false;
        private Pen MyPen = new Pen(Color.Black);
        private bool isSelect = false;
        private int penSize = 1;
        private DashStyle dashStyle;
        private List<GraphicObject> objList = new List<GraphicObject>();

        private GraphicObject grouppingShape;
        private GraphicObject selectedObject;
        private GraphicObject currentObject;
        private bool mouseDown = false;
        private bool controlDown;


        private int dx = 0;
        private int dy = 0;
        private Point dragStart;


        private void InnitValue()
        {
            //combobox 
            //size
            size_combox.Items.Add("1");
            size_combox.Items.Add("3");
            size_combox.Items.Add("5");
            size_combox.Items.Add("8");
            size_combox.SelectedIndex = 0;
            //dash style
            foreach (DashStyle style in Enum.GetValues(typeof(DashStyle)))
            {
                dash_style_combox.Items.Add(style);
            }
            dash_style_combox.SelectedIndex = 0;
            //brush style
            brush_style_combox.Items.Add("Solid");
            brush_style_combox.Items.Add("HatchBrush");

            brush_style_combox.SelectedIndex = 0;


        }


        private void ResetSelected(ref List<GraphicObject> objList)
        {
            foreach(GraphicObject obj in objList)
            {
                obj.isSelected = false;
            }
        }

        private void UpdatePara()
        {
            MyPen.Width = Convert.ToInt32(size_combox.SelectedValue);
        }



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentObject == null)
            {
                bool hitShape = false;
                foreach (GraphicObject obj in objList)
                {
                    if (obj.Contain(e.Location) || obj.isNearSpareDot(e.Location))
                    {
                        hitShape = true;
                        break;
                    }
                }

                if (!hitShape)
                {
                    foreach (GraphicObject obj in objList)
                    {
                        obj.isSelected = false;
                    }
                }
            }

            mouseDown = true;
            if (grouppingShape != null)
            {
                grouppingShape.End = e.Location;
                grouppingShape.myPen.DashStyle = DashStyle.Dot;
                grouppingShape.Start = grouppingShape.End;


            }

            if (currentObject != null && !isSelect)
            {
                currentObject.myPen = new Pen(cd.Color, penSize);

                currentObject.myBush = new SolidBrush(cd.Color);


                currentObject.myPen.DashStyle = (DashStyle)dashStyle;

                switch (brush_style_combox.SelectedItem.ToString())
                {
                    case "Solid":
                        currentObject.myBush = new SolidBrush(cd.Color);
                        break;
                    case "HatchBrush":
                        currentObject.myBush = new HatchBrush(HatchStyle.DiagonalBrick, cd.Color, Color.White);
                        break;

                }
                currentObject.Start = e.Location;
                currentObject.End = e.Location;
                objList.Add(currentObject);
            }
            if (!isSelect)
            {
                isPaiting = true;
            }




            if (objList.Count > 0 && isSelect)
            {

                foreach (GraphicObject obj in objList)
                {
                    if(obj.Contain(e.Location) && !obj.isNearSpareDot(e.Location))
                    {
                        if (!controlDown)
                        {
                            ResetSelected(ref objList);                         
                        }
                        obj.isSelected = true;
                        dragStart = e.Location;
                        
                    }                               
                }

            }
        }




        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

           
            if (grouppingShape != null && mouseDown)
            {

                grouppingShape.End = e.Location;
            }

            foreach (GraphicObject obj in objList)
            {

                if (obj.isNearSpareDot(e.Location))
                {
                    int grabIdex = obj.GrabIndex(e.Location);
                    if (grabIdex == 0)
                    {
                        Cursor.Current = Cursors.SizeNWSE;
                    }
                    if (grabIdex == 1)
                    {
                        Cursor.Current = Cursors.SizeNESW;
                    }
                    if (grabIdex == 2)
                    {
                        Cursor.Current = Cursors.SizeNESW;
                    }
                    if (grabIdex == 3)
                    {
                        Cursor.Current = Cursors.SizeNWSE;
                    }

                    if (mouseDown)
                    {

                       
                        obj.UpdateDraging(e.Location, grabIdex);
                        if(obj is Polygon)
                        {
                            Polygon tmp = (Polygon)obj;
                            tmp.UpdateCordinate();
                        }
                    }

                }
            }


            if (isSelect && mouseDown)
            {

                dx = e.Location.X - dragStart.X;
                dy = e.Location.Y - dragStart.Y;
                foreach (GraphicObject obj in objList)
                {
                    if (!obj.isNearSpareDot(e.Location))
                    {
                        if (obj.isSelected && !(obj is Polygon))
                        {
                            
                            obj.Start = new Point(obj.Start.X + dx, obj.Start.Y + dy);
                            obj.End = new Point(obj.End.X + dx, obj.End.Y + dy);
                        }
                        else if (obj.isSelected && obj is Polygon)
                        {
                           
                            Polygon tmp = (Polygon)obj;
                            tmp.UpdatePara(dx, dy);
                        }
                    }


                }

                dragStart = e.Location;


            }
            else if (isPaiting)
            {
                if (currentObject != null)
                {

                    if (currentObject is Circle || currentObject is FilledCircle)
                    {
                        currentObject.End = new Point(e.Location.X, currentObject.End.Y + e.Location.X - currentObject.End.X);

                    }
                    else
                    {
                        currentObject.End = e.Location;
                    }
                    if (currentObject is Polygon polygon)
                    {
                        polygon.UpdateCordinate();
                    }
                }
                

            }



            pictureBox1.Invalidate();
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;

            if (isSelect && selectedObject != null)
            {



                selectedObject = null;


            }
            else if (isPaiting)
            {

                isPaiting = false;


            }
            if (grouppingShape != null)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    objList[i].BeGrouping(grouppingShape.Start, grouppingShape.End);
                }
                grouppingShape = null;
            }

            currentObject = null;



        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g.Clear(this.BackColor);

            if (grouppingShape != null && grouppingShape.Start != null)
            {


                grouppingShape.Draw(e.Graphics);
            }
            for(int i = 0; i < objList.Count; i++)
            {
                if (objList[i].delete)
                {
                    objList.RemoveAt(i);    
                }
                else if (objList[i] !=null && !objList[i].delete)
                {
                    objList[i].Draw(e.Graphics);
                }
            }
            

        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new Line();
        }

        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new Ellipse();
        }

        private void btn_rec_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new Rectangle_2();
        }



        private void btn_arc_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new Arc();
        }

        private void btn_filledEl_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new FilledEllipse();
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            isSelect = false;
            cd.ShowDialog();
            color_dis_box.BackColor = cd.Color;
        }

        private void btn_polygon_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new Triangle();
        }

        private void btn_pentagon_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new Pentagon();
        }

        private void btn_filled_triangle_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new FilledTriangle();
        }

        private void btn_filled_pentagon_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new FilledPentagon();
        }




        private void btn_select_Click(object sender, EventArgs e)
        {

            isSelect = true;
            selectedObject = null;
            currentObject = null;
        }

        private void btn_Filledrec_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new FilledRectangle();
        }



        private void size_combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            isSelect = false;
            penSize = int.Parse(size_combox.SelectedItem.ToString());

        }

        private void dash_style_combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            isSelect = false;
            dashStyle = (DashStyle)dash_style_combox.SelectedItem;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < objList.Count; i++)
            {
                if (objList[i].isSelected)
                {
                    objList[i].delete =true;
                    
                }
            }
        }

        private void btn_group_Click(object sender, EventArgs e)
        {

            isSelect = true;
            grouppingShape = new Rectangle_2();
            foreach (GraphicObject obj in objList)
            {
                obj.isSelected = false;
            }
        }

       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.ControlKey)
            {
                controlDown = true;

            }
            else
            {
                controlDown = false;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                controlDown = false;

            }
           
        }

        private void btn_circle_Click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new Circle();
        }

        private void btn_filled_circle_click(object sender, EventArgs e)
        {
            isSelect = false;
            currentObject = new FilledCircle();
        }
    }
}