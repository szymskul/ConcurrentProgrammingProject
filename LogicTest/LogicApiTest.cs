using Data;
using Logic;

namespace LogicTest
{
    internal class fakeClassDataAbstractApi : DataAbstractAPI
    {

    }
    [TestClass]
    public class LogicApiTest
    {
        [TestMethod]
        public void TestLogicApi()
        {
            fakeClassDataAbstractApi fake = new fakeClassDataAbstractApi();
            LogicAbstractAPI test = LogicAbstractAPI.CreateAPI(fake);
            test.createBalls(2);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.getAllBalls().Count, 2);
        }
    }
}
