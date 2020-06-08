using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4._4
{
    public partial class Form1 : Form
    {
        Point center;
        PointF centerRotation;
        Rectangle board = new Rectangle(0, 0, 200, 20);
        Point lineStart;
        Point lineEnd;
        PointF[] newPoints = new PointF[8];
        PointF[] startCurvePositions = new PointF[8];
        PointF newP1, newP2, newP3, newP4, newP5, newP6, newP7, newP8;

        float radius = 170f;
        
        int offsetY = 0;
        int offsetX = 0;

        int counter = 0;
        int hummerCount = 0;
        



        public Form1()
        {
            InitializeComponent();
            Paint += Form1_Paint;
            center = new Point(ClientRectangle.Width / 2, ClientRectangle.Height / 2);
            centerRotation = new Point(center.X - 150, center.Y - 90);
            board.X = center.X - 150;
            board.Y = center.Y;

            lineStart.X = center.X - 150;
            lineStart.Y = center.Y - 90;
            lineEnd.X = center.X;
            lineEnd.Y = center.Y - 90;

            PointF p1 = new PointF(lineStart.X, lineEnd.Y + 10);
            PointF p2 = new PointF(lineStart.X, lineEnd.Y - 10);
            PointF p3 = new PointF(lineStart.X + 130, lineEnd.Y - 10);
            PointF p4 = new PointF(lineStart.X + 130, lineEnd.Y - 35);
            PointF p5 = new PointF(lineStart.X + 170, lineEnd.Y - 35);
            PointF p6 = new PointF(lineStart.X + 170, lineEnd.Y + 35);
            PointF p7 = new PointF(lineStart.X + 130, lineEnd.Y + 35);
            PointF p8 = new PointF(lineStart.X + 130, lineEnd.Y + 10);

            PointF[] curvePoints = { p1, p2, p3, p4, p5, p6, p7, p8 };
            newPoints = curvePoints;
            startCurvePositions = curvePoints;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           

            if (counter <= 2)
            {
                offsetY += 13;
                counter += 1;
                Refresh();
            }
            if (hummerCount == 1 && counter == 3)
            {
                counter = 0;
                offsetY = 0;
                timer1.Stop();
                

            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawPin(e, offsetY);
            e.Graphics.DrawRectangle(Pens.Brown, board);
            e.Graphics.FillRectangle(Brushes.Brown, board);
            drawHummer(e, newPoints);

          
        }

        public void drawHummer(PaintEventArgs e, PointF[] curvePoints)
        {
            Pen greenPen = new Pen(Color.Green);


           
            e.Graphics.DrawPolygon(greenPen, curvePoints);
            e.Graphics.FillPolygon(Brushes.Green, curvePoints);
            // e.Graphics.DrawLine(greenPen, lineStart , lineEnd);

        }

        public void drawPin(PaintEventArgs e, int y)
        {
            int yOffset = y;
            Pen blackPen = new Pen(Color.Black);

            Point p1 = new Point(center.X,  center.Y + yOffset);
            Point p2 = new Point(center.X + 5, center.Y - 50 + yOffset);
            Point p3 = new Point(center.X + 15, center.Y - 50 + yOffset);
            Point p4 = new Point(center.X + 15, center.Y - 55 + yOffset);
            Point p5 = new Point(center.X -15, center.Y - 55 + yOffset);
            Point p6 = new Point(center.X - 15, center.Y - 50 + yOffset);
            Point p7 = new Point(center.X - 5, center.Y - 50 + yOffset);
            
            Point[] curvePoints = {p1, p2, p3, p4, p5, p6, p7};

            e.Graphics.DrawPolygon(blackPen, curvePoints);
            e.Graphics.FillPolygon(Brushes.Black, curvePoints);

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
            double angle = 0;

            if (hummerCount >= 7)
            {
                hummerCount = 0;
                newPoints = startCurvePositions;
                timer2.Stop();
                Refresh();
                

            }

            if (hummerCount == 0 || hummerCount == 2 || hummerCount == 4|| hummerCount == 6)
            {
                angle = -25;
                Refresh();
            }

            if (hummerCount == 1 || hummerCount == 3 || hummerCount == 5)
            {
                angle = 30;
                Refresh();
            }
            
            



            newP1 = RotatePoint(newPoints[0], centerRotation, angle);
            newP2 = RotatePoint(newPoints[1], centerRotation, angle);
            newP3 = RotatePoint(newPoints[2], centerRotation, angle);
            newP4 = RotatePoint(newPoints[3], centerRotation, angle);
            newP5 = RotatePoint(newPoints[4], centerRotation, angle);
            newP6 = RotatePoint(newPoints[5], centerRotation, angle);
            newP7 = RotatePoint(newPoints[6], centerRotation, angle);
            newP8 = RotatePoint(newPoints[7], centerRotation, angle);

            PointF[] array = { newP1, newP2, newP3, newP4, newP5, newP6, newP7, newP8 };
            newPoints = array;
            hummerCount +=  1;
           
        }

        static PointF RotatePoint(PointF pointToRotate, PointF centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }
    }


}
