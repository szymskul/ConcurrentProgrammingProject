using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        public double r {  get; set; }

        public Ball()
        {
            X = getRandomNumber(10, 100);
            Y = getRandomNumber(10, 100);
            VelocityX = getRandomNumber(10, 100);   
            VelocityY = getRandomNumber(10, 100);

            r = 5;
        }

        Random random = new Random();
        public double getRandomNumber(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;  
        }


        
    }
}