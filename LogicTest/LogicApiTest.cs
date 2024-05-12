using Data;
using Logic;

namespace LogicTest
{
    [TestClass]
    public class LogicApiTest
    {
        internal class fakeClassDataAbstractApi : DataAbstractAPI
        {
            public override void createBalls(int ballsCount)
            {

            }

            public override int getBallsAmount()
            {
                return 0;
            }

            public override int getHeightSize()
            {
                return 200;
            }


            public override int getWidthSize()
            {
                return 200;
            }

            public override void OnCompleted()
            {

            }

            public override void OnError(Exception error)
            {

            }


            public override void OnNext(IBall ball)
            {

            }


            public override IDisposable Subscribe(IObserver<IBall> observer)
            {
                return null;
            }


        }
        private LogicAbstractAPI logicAbstractAPI;

        [TestMethod]
        public void Create()
        {
            fakeClassDataAbstractApi fake = new fakeClassDataAbstractApi();
            logicAbstractAPI = LogicAbstractAPI.CreateAPI(fake);
            Assert.IsNotNull(logicAbstractAPI);
        }

        [TestMethod]
        public void GetSizes() 
        {
            fakeClassDataAbstractApi fake = new fakeClassDataAbstractApi();
            logicAbstractAPI = LogicAbstractAPI.CreateAPI(fake);
            int height = logicAbstractAPI.getHeight();
            int width = logicAbstractAPI.getWidth();
            Assert.AreEqual(height, 500);
            Assert.AreEqual(width, 700);

        }

    }

}
