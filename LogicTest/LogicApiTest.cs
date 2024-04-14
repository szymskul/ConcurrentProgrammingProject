using Logic;

namespace LogicTest
{
    internal class LogicApiTest
    {
        [TestMethod]
        public void TestLogicApi()
        {
            LogicAbstractAPI test = LogicAbstractAPI.CreateAPI();
            test.createBalls(2);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.getAllBalls().Count, 2);
        }
    }
}
