using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Table
    {
        public int height { get; set; }
        public int widht { get; set; }
        public List<Ball> balls { get; set; }

        public Table(int height, int width) 
        {
            this.height = height;
            this.widht = width;
            this.balls = new List<Ball>();
        }

        public void AddBalls(int count)
        {
            for(int i = 0; i < count; i++) 
            {
                balls.Add(new Ball());
            }
        }

      
    }
}
