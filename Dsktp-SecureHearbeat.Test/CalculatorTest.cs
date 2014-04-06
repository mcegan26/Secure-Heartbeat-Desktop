using Dsktp_SecureHeartbeat;
using NUnit.Framework;



namespace Dsktp_SecureHearbeat.Test
{
    [TestFixture]
    public class CalculatorTest
    {
        private Calculator testCal;

        [SetUp]
        public void SetUp()
        {
            testCal = new Calculator();
        }

        [Test]
        public void testAddingZero()
        {
            int answer = testCal.addVal(0, 0);
            Assert.AreEqual(0, answer);
        }

        [Test]
        public void testAddingOne()
        {
            int answer = testCal.addVal(1, 0);
            Assert.AreEqual(1, answer);
        }

        [Test]
        public void testAddingZeroAndOne()
        {
            int answer = testCal.addVal(0, 1);
            Assert.AreEqual(1, answer);
        }

        [Test]
        public void testAddingOneAndOne()
        {
            int answer = testCal.addVal(1, 1);
            Assert.AreEqual(2, answer);
        }
    }
}
