using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Logic
{
    internal class Table
    {
        public int height { get; set; }
        public int widht { get; set; }
        public List<IBall> balls { get; set; }

        private Task movingTask;
        private int time = 20;

        public Table(int height, int width)
        {
            this.height = height;
            this.widht = width;
            this.balls = new List<IBall>();
        }

        public void AddBalls(int count)
        {
            for (int i = 0; i < count; i++)
            {
                balls.Add(new Ball());
            }
        }

        public void MovingBalls()
        {
            foreach (Ball ball in balls)
            {
                ball.ChangingPosition(height, widht);

            }
        }

        public void Complete()
        {
            movingTask = new Task(ConstanceMove);
            movingTask.Start();
        }

        public void ConstanceMove()
        {
            while (true)
            {
                MovingBalls();
                Thread.Sleep(time);
            }
        }
    }
}