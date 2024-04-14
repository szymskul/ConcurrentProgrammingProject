
using Logic;
namespace LogicTest
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void BallTestMethod()
        {
            Ball ball = new Ball();
            double prev_x = ball.x;
            double prev_y = ball.y;
            ball.ChangingPosition(200, 250);
            Assert.AreEqual(ball.x, prev_x + ball.velocityX);
            Assert.AreEqual(ball.x, prev_y + ball.velocityY);   
        }
    }
}