using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class IModelBall
    {
        public static IModelBall CreateBall(IBall ball)
        {
            return new BallModel(ball);
        }

        public abstract double Yposition { get; }

        public abstract double Xposition { get; }
    }
}
