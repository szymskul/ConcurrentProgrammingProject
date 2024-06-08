using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class LoggerBall
    {
        public LoggerBall(DateTime date, int ID, Vector2 position, Vector2 velocity)
        {
            this.Date = date;
            this.ID = ID;
            this.PositionX = position.X;
            this.PositionY = position.Y;
            this.VelocityX = velocity.X;
            this.VelocityY = velocity.Y;


        }

        public DateTime Date { get; }
        public int ID { get; }
        public float PositionX { get; }
        public float PositionY { get; }
        public float VelocityX { get; }
        public float VelocityY { get; }

    }
}
