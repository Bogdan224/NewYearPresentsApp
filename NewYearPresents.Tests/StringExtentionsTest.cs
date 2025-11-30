using NewYearPresents.Models.Extentions;

namespace NewYearPresents.Tests
{
    [TestClass]
    public sealed class StringExtentionsTest
    {
        [TestMethod]
        [DataRow("бокс 50 шт")]
        public void GetFloatTest1(string source)
        {
            var value = source.GetFloat();
            if(value != 50) Assert.Fail();
            Assert.IsTrue(true);
        }
    }
}
