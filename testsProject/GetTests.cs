using System;
using gimmeThat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace testsProject
{
    [TestClass]
    public class GetTests
    {
        [TestMethod]
        public void TestBasicBehaviourGettingGoogleContent()
        {
            var obj = new GimmeThat().Get("http://google.com");
            Assert.IsFalse(string.IsNullOrWhiteSpace(obj.Response));
        }

        [TestMethod]
        public void TestThenMethodToDisplay()
        {
            var obj = new GimmeThat()
                .Get("http://google.com")
                .Then((uri, type, result) =>
                {
                    Assert.IsNotNull(result);
                    Assert.AreEqual("http://google.com", uri);
                });

            Assert.IsFalse(string.IsNullOrWhiteSpace(obj.Response));
        }

        [Serializable]
        private class WeatherForCity : IGimmeResponse<WeatherForCity>
        {
            [Serializable]
            public class Rates
            {
                public double MXN { get; set; }
                public double AUD { get; set; }
                public double HKD { get; set; }
                public double RON { get; set; }
                public double HRK { get; set; }
            }

            public string date { get; set; }
            public Rates rates { get; set; }

            public WeatherForCity()
            {}

            public WeatherForCity ConvertTo(string response)
            {
                return JsonConvert.DeserializeObject<WeatherForCity>(response);
            }
        }
        [TestMethod]
        public void TestThenAsWithAJson()
        {
            var obj = new GimmeThat()
                .Get("https://api.exchangeratesapi.io/latest")
                .ThenAs<WeatherForCity>((uri, type, result) =>
                {
                    Assert.IsNotNull(result);
                    Assert.AreEqual("https://api.exchangeratesapi.io/latest", uri);
                });

            Assert.IsFalse(string.IsNullOrWhiteSpace(obj.Response));
        }
    }
}
