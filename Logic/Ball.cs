using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball
    {
        public double x { get; set; }
        public double y { get; set; }
        public double velocityX { get; set; }
        public double velocityY { get; set; }
        public double r {  get; set; }

        public Ball()
        {
            x = getRandomNumber(10.0, 100.0);
            y = getRandomNumber(10.0, 100.0);
            velocityX = getRandomNumber(10, 100);   
            velocityY = getRandomNumber(10, 100);

            r = 5;
        }

        Random random = new Random();
        public double getRandomNumber(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;  
        }

        public void Moving(double height, double width)
        {
            double next_x = x + velocityX;
            double next_y = y + velocityY;

            if (next_x + r > width || next_x < r)
            {
                velocityX *= -1.0;
            }
            if (next_y + r > height || next_y < r)
            {
                velocityY *= -1.0;
            }
            x = next_x;
            y = next_y;
        }


        
    }
}