using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace DataApiTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void CreateDataAPITest()
        {
            DataAbstractAPI dataAPI = DataAbstractAPI.CreateApi();
            Assert.IsNotNull(dataAPI);
        }


        [TestMethod]
        public void CreateBalssTest() 
        {
            int count = 3;
            DataAbstractAPI abstractAPI = DataAbstractAPI.CreateApi();
            abstractAPI.createBalls(count);
            Assert.AreEqual(abstractAPI.getBallsAmount(), count);
        }


        [TestMethod]
        public void GetBoardSizeTest()
        {
            DataAbstractAPI abstractAPI = DataAbstractAPI.CreateApi();
            Assert.AreEqual(abstractAPI.getHeightSize(), 500);
            Assert.AreEqual(abstractAPI.getWidthSize(), 700);
        }

        [TestMethod]
        public void GetBall() 
        {
            DataAbstractAPI abstractAPI = DataAbstractAPI.CreateApi();
            abstractAPI.createBalls(2);
            Assert.AreEqual(abstractAPI.getBallsAmount(), 2);
        }
    }
}
