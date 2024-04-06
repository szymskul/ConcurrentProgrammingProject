using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BallInterface
    {
        public abstract double X { get; set; }
        public abstract double Y { get; set; }
        public abstract double VelocityX { get; set; }
        public abstract double VelocityY { get; set; }
        public abstract bool IsVisible { get; set; }

        public static BallInterface CreateBall(double X, double Y, double VelocityX, double VelocityY, bool IsVisible)
        {
            return new Ball(X, Y, VelocityX, VelocityY, IsVisible);
        }

        private class Ball : BallInterface
        {
            public override double X { get; set; }
            public override double Y { get; set; }
            public override double VelocityX { get; set; }
            public override double VelocityY { get; set; }
            public override bool IsVisible { get; set; }

            public Ball(double x, double y, double velocityX, double velocityY, bool isVisible)
            {
                this.X = x;
                this.Y = y;
                this.VelocityX = velocityX;
                this.VelocityY = velocityY;
                this.IsVisible = isVisible;
            }

        }
    }
}