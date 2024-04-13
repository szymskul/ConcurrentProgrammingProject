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

        public double X
        {
            get { return ball.x; }
        }

        public double Y
        {
            get { return ball.y; }
        }
    }
}
