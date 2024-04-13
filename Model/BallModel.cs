using Logic;

namespace Model
{
    public class BallModel
    {
        private Ball ball;

        public BallModel(Ball ball)
        {
            this.ball = ball;
        }

        public double Xposition
        {
            get { return ball.x; }
        }

        public double Yposition
        {
            get { return ball.y; }
        }
    }
}
