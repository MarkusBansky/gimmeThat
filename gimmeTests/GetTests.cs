using gimmeThat;
using NUnit.Framework;

namespace gimmeTests
{
    [TestFixture]
    public class GetTests
    {
        [Test]
        public void GetGoogleSuccessNotNull()
        {
            var gimmeGoogle = new GimmeThat()
                .Get("http://www.google.com");
            
            Assert.IsNotNull(gimmeGoogle.Response);
        }
        
        [Test]
        public void GetGoogleSuccessNotEmpty()
        {
            var gimmeGoogle = new GimmeThat()
                .Get("http://www.google.com");
            
            Assert.IsNotEmpty(gimmeGoogle.Response);
        }

        [Test]
        public void GetGoogleFailIsNull()
        {
            var gimmeGoogle = new GimmeThat()
                .Get("hppt://www.google.com");
            
            Assert.IsNull(gimmeGoogle.Response);
        }
        
        [Test]
        public void GetGoogleFailHasError()
        {
            var gimmeGoogle = new GimmeThat()
                .Get("hppt://www.google.com");
            
            Assert.IsTrue(gimmeGoogle.HasError);
        }
    }
}