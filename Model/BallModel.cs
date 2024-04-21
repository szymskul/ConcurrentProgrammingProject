using Logic;

namespace Model
{
    internal class BallModel : IModelBall
    {
        private IBall ball;

        public BallModel(IBall ball)
        {
            this.ball = ball;
        }

        public override double Xposition
        {
            get { return ball.X; }
        }

        public override double Yposition
        {
            get { return ball.Y; }
        }
    }
}
