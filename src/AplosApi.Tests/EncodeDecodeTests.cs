using System;
using FluentAssertions;
using Xunit;

namespace AplosApi.Tests
{
    public class EncodeDecodeTests
    {
        private const string PrivateKey = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCU4vOB5xNwfZrA5Z8nocBkaKrYLe9Tp4JdnpZNVqSwBD2I0wQwyy8kK5yFUYwNe9A4yayha3IzYZJFkbp7zWD0f6D7Qml/4obOgndJNX1ssQQKGJjqBxUWPZ5LfRoh8kTwQ30iiQzUAw0bbGyNXopjro5SP87UqRhEN2jIH6KY2sif2gKa0LolqWQOT6yk5t6vzuS9cDuQ2Gp+bVYHmvgGEYDccozKWbXdXk2hjF8LVf74kgbz8NsFj0sUL0VonUTE4GUn4mMRVkunp00mz/uy5ujBBeZ+AarpA+ziQdVsELbvNCAAiBSWdBFZFOsGR0QZmRE1NBJFVTTfWYnJnYL9AgMBAAECggEAU+wdUDK9PWI/cc28yW0ecjqhWluCFIhOLpEurYdSKzkoqlAvg4V0HBJNFsThidQpiWj8Wryi0Z2FApvjHtekeAzc4+QKbaB1VkAqFuUEvtiEq2A0CW5Wj4PKD0kECvBxtXCFP4s65OKXZ8bU5VbN8OQ6qtf2sN5jpEgLUwqp7zg7GsD+xQ2rBXSZGWO+GZT/TBo/4Up5DAhI4NX2g2DAgM1RXamcVWWNF9pD7qMspdJIjgZhy9YH/1t6w/+OTN6f+XR5j+od/rJSwXN8blfjfSNSpDr1xVunbtPnP5+CRSoo6t9tBoJ6cRAswjOZBY/ZUJzvEIpu1XpRB5hqXQDhbQKBgQDtf04WWvpYAf1MR41UmChaKze32j7OkdlTXmawopWS84W5vRDpnl4IlKQSoYoyujOkuEVgNt7j6z+BpA/VJNSs5C8hlwMxFmjQE65Pm5dJKxpoXGYCO/BiiRPshWWvoYVlGRbm3/i7h6B1Ets7FFTGQ+5LnNAA0Pflte0OcnachwKBgQCgfF/57cULRzuIT/BiHeUsgbJqy8uINqfZfTbgUVobiqhQdDRShVFyGzV9B4pmhZLg97chrLk8LZLcduCWrLXz5nR+slbCNT+Cd8z1TGDYB0GSL3Xqs2PuVJAnCQXaWMy11erwo+EfMiMwQt5neGrRRK0xUYAzhdyTGM89t1vpWwKBgQCBp5g3uB8nYJ6pv/42CoMtBp76bdkP6KueSdEB6SCDAxMkHUVYZK6tPIh961aI7wI2kKq/JfV0s/8NhUVndR3t7PyV9900NSmvPq9Qon5q4W8fPiIqYhKPmIoZ+5FR2nn6gUHxdKBTsG02vL6WeDj3rCoxeUBM0cgjzbfGxVJfjQKBgBN8xtK8co7aCXqV6mSfqLJs1VNuh7p0mJEqST2X408w2Rtb4PacRbWLZEVYYw/r7Ffw/IXUSXHrPsgSj1b8heOl+kNgOHTAroOTNIocyi2xQ98ScEkdm2bXUeHLkLBg1ArIfQzXeYCmP+ueUw9RafcbVcSFVwHqoUwjWRbvavO9AoGAdVCT/jYkpTm7chDjEpfU5I/m/r2boMkr1IQjCTm6Zny8u5BOZ8Id7hu/NXWx4518y2JNV5HDGkge6B8s6oZMomAv0n24A9NajIF5GKnUpPq3gbQrRK11rRQ/WjQCk083EDxntr33Y0PmFaAiOOH2A6UOedsOqF37uzpygCQAc9k=";

        [Fact]
        public void can_encode_and_decode()
        {
            var encDec = new EncoderDecoder(PrivateKey);
            var phrase = Guid.NewGuid().ToString();
            var encrypted = encDec.EncryptData(phrase);
            var decrypted = encDec.DecryptEncryptedData(encrypted);
            decrypted.Should().Be(phrase);
        }
    }
}
