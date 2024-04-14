using Data;
namespace DataApiTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void CreateDataAPITest()
        {
            DataAbstractAPI dataAPI = DataAbstractAPI.CreateAPI();
            Assert.IsNotNull(dataAPI);
        }
    }
}