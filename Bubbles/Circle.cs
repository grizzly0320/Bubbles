using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Bubbles
{
    public class Circle
    {
        private Random r = new();
        private float diam;
        private float x, y;
        //private int mas;
        private float gravityX = 0;
        private float gravityY = 0.1f;
        //private int speed;
        private float centerX, centerY;
        Random rand = new();
        public float vx, vy;

        public float CenterX => centerX;
        public float CenterY => centerY;
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => x = value; }
        public float Diam => diam;
        public Color Color { get; set; }
        public float GravityX => gravityX;
        public float GravityY => gravityY;

        public Circle(float diam, float x, float y, Color color)
        {
            this.diam = diam;
            this.x = x;
            this.y = y;
            this.Color = color;
        }
        public Circle(float diam, float x, float y)
        {
            this.diam = diam;
            this.x = x;
            this.y = y;
            this.Color = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
            vx = RandomInter();
            vy = RandomInter();
            centerX = x + diam/2;
            centerY = y + diam/2;
        }
        public void Move()
        {
            //vy += gravityY;
            //vx += gravityX;
            x += vx;
            y += vy;
        }
        //public void SetBallX(float dx)
        //{
        //    x += dx;
        //}
        //public void SetBallY(float dy)
        //{
        //    y += dy;
        //}
        public void Paint(Graphics g)
        {
            var brush = new SolidBrush(Color);
            g.FillEllipse(brush, X, Y, Diam, Diam);
        }
        private float RandomInter()
        {
            int newRandom = 0;
            while (newRandom == 0)
            {
                newRandom = rand.Next(-5, 5);
            }
            return (float)newRandom;
        }
    }
}
