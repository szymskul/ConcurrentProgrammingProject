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

        public double getX
        {
            get { return ball.x; }
        }

        public double getY
        {
            get { return ball.y; }
        }
    }
}
