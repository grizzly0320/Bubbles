using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bubbles
{
    public class Animator
    {
        private Circle c;
        private Thread? t = null;
        private Thread? f = null;
        public bool IsAlive => t == null || t.IsAlive;
        public bool IsfAlive => f == null || t.IsAlive;
        private Random rnd = new Random();
        public Size ContainerSize { get; set; }
        private static List<Circle> circles = new();

        public Animator(Size containerSize)
        {
            ContainerSize = containerSize;
            c = new Circle(50, rnd.Next(75, ContainerSize.Width - 75), rnd.Next(75, ContainerSize.Height - 75));
            circles.Add(c);
        }

        public void Start()
        {
            float dx = RandomInter();
            float dy = RandomInter();
            t = new Thread(() =>
            {
                while (IsAlive)
                {
                    CheckCollision(circles);
                    CheckWalls(c);
                    Thread.Sleep(30);
                    c.Move();
                    //CheckCollision(circles);
                }
            });
            t.IsBackground = true;
            t.Start();
        }
        private float RandomInter()
        {
            int newRandom = 0;
            while (newRandom == 0)
            {
                newRandom = rnd.Next(-5, 5);
            }
            return (float)newRandom;
        }
        public void PaintCircle(Graphics g)
        {
            c.Paint(g);
        }

        private void CheckCollision(List<Circle> circles)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                for (int j = i + 1; j < circles.Count; j++)
                {
                    var dx = circles[i].X - circles[j].X;
                    var dy = circles[i].Y - circles[j].Y;
                    var dist = Math.Sqrt(dx * dx + dy * dy);
                    if (dist < (circles[i].Diam / 2 + circles[j].Diam / 2))
                    {
                        var angle = Math.Atan2(dy, dx);
                        float sin = (float)Math.Sin(angle);
                        float cos = (float)Math.Cos(angle);
                        Vector2 pos0 = Rotate(dx, dy, sin, cos, false); ;
                        Vector2 pos1 = Rotate(dx, dy, sin, cos, false);
                        Vector2 vel0 = Rotate(circles[i].vx, circles[i].vy, sin, cos, true);
                        Vector2 vel1 = Rotate(circles[j].vx, circles[j].vy, sin, cos, true);
                        float vxTotal = vel0.X - vel1.X;
                        vel1.X = vel0.X;// + vxTotal;
                        var absV = Math.Abs(vel0.X) + Math.Abs(vel1.X);
                        var overlap = (circles[i].Diam / 2 + circles[j].Diam / 2) - Math.Abs(pos0.X - pos1.X);
                        pos0.X += vel0.X / absV * overlap;
                        pos1.X += vel1.X / absV * overlap;
                        var pos0F = Rotate(pos0.X, pos0.Y, sin, cos, false);
                        var pos1F = Rotate(pos1.X, pos1.Y, sin, cos, false);
                        var vel0F = Rotate(vel0.X, vel0.Y, sin, cos, false);
                        var vel1F = Rotate(vel1.X, vel1.Y, sin, cos, false);
                        circles[i].vx = vel0F.X;
                        circles[i].vy = vel0F.Y;
                        circles[j].vx = vel1F.X;
                        circles[j].vy = vel1F.Y;
                    }
                }
            }
        }
        public Vector2 Rotate(float x, float y, float sin, float cos, bool reverse)
        {
            float vx = (reverse) ? (x * cos + y * sin) : (x * cos - y * sin);
            float vy = (reverse) ? (y * cos - x * sin) : (y * cos + x * sin);
            Vector2 vect = new Vector2(vx, vy);
            return vect;
        }
        public void CheckWalls(Circle c) {
            if (c.X + c.Diam >= ContainerSize.Width || c.X <= 0)
            {
                c.vx = -c.vx;
            }
            if (c.Y + c.Diam >= ContainerSize.Height || c.Y <= 0)
            {
                c.vy = -c.vy;
            }
        }
    }
}
