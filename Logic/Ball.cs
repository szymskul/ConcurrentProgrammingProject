using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Ball
    {
        public double x { get; set; }
        public double y { get; set; }
        public double velocityX { get; set; }
        public double velocityY { get; set; }
        public double r { get; set; }

        public Ball()
        {
            x = getRandomNumber(10.0, 490.0);
            y = getRandomNumber(10.0, 490.0);
            velocityX = getRandomNumber(1.0, 2);
            velocityY = getRandomNumber(1.0, 2);

            r = 10;
        }

        Random random = new Random();
        public double getRandomNumber(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }

        public void ChangingPosition(double height, double width)
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

    }
}