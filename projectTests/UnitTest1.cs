using System.Threading.Tasks.Sources;
using static System.Formats.Asn1.AsnWriter;

namespace calculatorNamespace
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void scoreAddTest()
        {
            Calculator calc = new Calculator();
            int scoreAdd = calc.add(4, 2);
            Assert.AreEqual(scoreAdd, 6);
        }

        [TestMethod]
        public void scoreRemoveTest()
        {
            Calculator calc = new Calculator();
            int scoreRemove = calc.remove(4, 2);
            Assert.AreEqual(scoreRemove, 2);
        }
    }
}