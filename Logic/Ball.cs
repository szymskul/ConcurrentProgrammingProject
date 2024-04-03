 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class Ball
    {
        private double X { get; set; }
        private double Y { get; set; }
        private double VelocityX { get; set; }
        private double VelocityY { get; set; }
        public bool IsVisible { get; set; }

        public Ball(double x, double y, double velocityX, double velocityY, bool isVisible) 
        {
            this.X = x;
            this.Y = y;
            this.VelocityX = velocityX;
            this.VelocityY = velocityY;
            this.IsVisible = isVisible;
        }

        Random randomNumber = new Random();

        public double generateRandomPosition(int scale)
        {
            return randomNumber.NextDouble() * scale;
        }
    }
}
