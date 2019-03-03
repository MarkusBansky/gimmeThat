using gimmeThat;
using NUnit.Framework;

namespace gimmeTests
{
    [TestFixture]
    public class ThenTests
    {
        [Test]
        public void GetGoogleThenWriteTheLine()
        {
            var _ = new GimmeThat()
                .Get("http://www.google.com")
                .Then((uri, type, result) => Assert.IsFalse(string.IsNullOrWhiteSpace(result)))
                .Response;
        }
    }
}