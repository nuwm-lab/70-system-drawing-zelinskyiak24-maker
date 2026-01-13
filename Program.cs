using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SystemDrawingGraph
{
    public class Form1 : Form
    {
        private const double XMin = -1;
        private const double XMax = 2.3;
        private const double Step = 0.7;

        public Form1()
        {
            Text = "System.Drawing Graph";
            Width = 800;
            Height = 600;
            Paint += Form1_Paint;
            Resize += (s, e) => Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGraph(e.Graphics);
        }

        private double CalculateY(double x)
        {
            return (Math.Exp(2 * x) - 8) / (x + 3);
        }

        private void DrawGraph(Graphics g)
        {
            g.Clear(Color.White);
            int width = ClientSize.Width;
            int height = ClientSize.Height;

            List<PointF> points = new();
            double yMin = double.MaxValue, yMax = double.MinValue;

            for (double x = XMin; x <= XMax; x += Step)
            {
                double y = CalculateY(x);
                yMin = Math.Min(yMin, y);
                yMax = Math.Max(yMax, y);
            }

            double scaleX = width / (XMax - XMin);
            double scaleY = height / (yMax - yMin);

            for (double x = XMin; x <= XMax; x += Step)
            {
                double y = CalculateY(x);
                float sx = (float)((x - XMin) * scaleX);
                float sy = (float)(height - (y - yMin) * scaleY);
                points.Add(new PointF(sx, sy));
            }

            using Pen axis = new(Color.Black, 2);
            using Pen graph = new(Color.Blue, 2);

            float zeroY = (float)(height - (0 - yMin) * scaleY);
            float zeroX = (float)((0 - XMin) * scaleX);

            g.DrawLine(axis, 0, zeroY, width, zeroY);
            g.DrawLine(axis, zeroX, 0, zeroX, height);

            for (int i = 0; i < points.Count - 1; i++)
                g.DrawLine(graph, points[i], points[i + 1]);
        }
    }
}
