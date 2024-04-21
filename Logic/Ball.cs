using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    internal class Ball : IBall
    {
        private double x;
        private double y;
        public double velocityX { get; set; }
        public double velocityY { get; set; }
        public double r { get; set; }

        public Ball()
        {
            x = getRandomNumber(21.0, 679.0);
            y = getRandomNumber(21.0, 479.0);
            velocityX = getRandomNumber(1, 3);
            velocityY = getRandomNumber(1, 3);

            r = 10;
        }

        Random random = new Random();
        public override double getRandomNumber(double min, double max)
        {
            if(min > 0 && max > 0)
                return random.NextDouble() * (max - min) + min;
            else
                return 1;
        }

        public override void ChangingPosition(double height, double width)
        {
            double next_x = x + velocityX;
            double next_y = y + velocityY;

            if (next_x + r > width || next_x < 0)
            {
                velocityX *= -1.0;
            }
            if (next_y + r > height || next_y < 0)
            {
                velocityY *= -1.0;
            }
            x = next_x;
            y = next_y;
        }

        public override double X
        {
            get => x;
            set { x = value; }
        }

        public override double Y
        {
            get => y;
            set { y = value; }
        }


    }
}