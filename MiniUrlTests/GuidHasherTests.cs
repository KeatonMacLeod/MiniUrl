namespace MiniUrlTests
{
    using MiniUrl.Logic;

    [TestClass]
    public class GuidHasherTests
    {
        [TestMethod]
        public void TestDefaultHashLength()
        {
            IUrlHasher guidHasher = new GuidHasher();
            string url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

            string hashedUrl = guidHasher.Hash(url);
            Assert.AreEqual(hashedUrl.Length, MiniUrl.Constants.HASH_LENGTH);
        }

        [TestMethod]
        public void TestCustomHashlength()
        {
            int customHashLength = 8;
            IUrlHasher guidHasher = new GuidHasher(customHashLength);
            string url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

            string hashedUrl = guidHasher.Hash(url);
            Assert.AreEqual(hashedUrl.Length, customHashLength);
        }
    }
}