using Data;
using NUnit.Framework;

namespace AbstractDataApiTest
{
    [TestClass]
    public class AbstractDataApiTestClass
    {
        [TestMethod]
        public void AbstractDataApiTestMethod()
        {
            DataAbstractApi dataAbstractApiTest = new DataAbstractApi();

            var expectedDataApiInstanceType = typeof(DataApi);

            var DataApiInstanceTypeTest = dataAbstractApiTest.CreateApi();

            Assert.IsInstanceOf(expectedDataApiInstanceType, DataApiInstanceTypeTest);
        }
    }
}