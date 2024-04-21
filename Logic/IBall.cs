using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class IBall
    {
        public static IBall CreateBall()
        {

            return new Ball();
        }

        public abstract double getRandomNumber(double min, double max);

        public abstract void ChangingPosition(double height, double width);

        public abstract double X { get; set; }
        public abstract double Y { get; set; }

    }
}
