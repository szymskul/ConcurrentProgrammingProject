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
        public int height { get; set; }
        public int widht { get; set; }

        public Table()
        {
            balls = new List<Ball>();
        }

        public void createBalls(int ballsCount)
        {
            for(int i = 0; i < ballsCount; i++)
            {
                balls.Add(new Ball(i + 1));
            }
        }

        public Ball getBall(int ballId)
        {
            return balls[ballId - 1];
        }
    }
}
