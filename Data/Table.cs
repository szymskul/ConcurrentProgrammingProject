using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Table
    {
        public List<Ball> balls { get; set; }
        public int height { get; private set; } = 500;
        public int width { get; private set; } = 700;

        public Table()
        {
            balls = new List<Ball>();
        }

        public void createBalls(int ballsCount)
        {
            for(int i = 0; i < ballsCount; i++)
            {
                Ball newBall = new Ball(i + 1);
                balls.Add(newBall);
            }
        }

        public Ball getBall(int ballId)
        {
            return balls[ballId - 1];
        }
    }
}
