using MiniUrl.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniUrlTests
{
    [TestClass]
    public class UrlValidatorTests
    {
        [TestMethod]
        public void TestValidUrl()
        {
            UrlValidator validator = new UrlValidator();

            string url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

            Assert.AreEqual(true, validator.ValidateUrl(url));
        }

        [TestMethod]
        public void TestWhitespaceUrl()
        {
            UrlValidator validator = new UrlValidator();

            string url = " ";

            Assert.AreEqual(false, validator.ValidateUrl(url));
        }

        [TestMethod]
        public void TestNullUrl()
        {
            UrlValidator validator = new UrlValidator();

            string url = null;

            Assert.AreEqual(false, validator.ValidateUrl(url));
        }
    }
}
