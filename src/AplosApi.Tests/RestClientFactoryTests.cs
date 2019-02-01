using System;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace AplosApi.Tests
{
    public class RestClientFactoryTests
    {
        private readonly IClientContext _initialContext;
        private readonly IClientContext _refreshContext;

        public RestClientFactoryTests()
        {
            _initialContext = new ClientContext
            {
                AuthToken = "a",
                Expires = DateTimeOffset.UtcNow.AddMilliseconds(300)
            };

            _refreshContext = new ClientContext
            {
                AuthToken = "b",
                Expires = DateTimeOffset.UtcNow.AddMilliseconds(1000)
            };

            _initialContext.IsExpired.Should().BeFalse();
            _refreshContext.IsExpired.Should().BeFalse();
        }

        [Fact]
        public async Task token_is_refreshed_only_after_current_token_expires()
        {
            var builder = A.Fake<IClientContextBuilder>();

            A.CallTo(() => builder.CreateContext())
                .ReturnsNextFromSequence(_initialContext, _refreshContext);

            var factory = new RestClientFactory(builder, TimeSpan.Zero);

            var initialClient = factory.BuildClient();

            var sameClient = factory.BuildClient();

            await Task.Delay(TimeSpan.FromMilliseconds(300));

            _initialContext.IsExpired.Should().BeTrue();
            _refreshContext.IsExpired.Should().BeFalse();

            var refreshClient = factory.BuildClient();

            A.CallTo(() => builder.CreateContext())
                .MustHaveHappenedTwiceExactly();

            var initialAuth = initialClient.AuthorizationToken();
            initialAuth.Should().Be(_initialContext.AuthToken);
            initialClient.AuthorizationExpiration().Should().Be(_initialContext.Expires);

            var sameAuth = sameClient.AuthorizationToken();
            sameAuth.Should().Be(_initialContext.AuthToken);
            initialClient.AuthorizationExpiration().Should().Be(_initialContext.Expires);

            var refreshAuth = refreshClient.AuthorizationToken();
            refreshAuth.Should().Be(_refreshContext.AuthToken);
            refreshClient.AuthorizationExpiration().Should().Be(_refreshContext.Expires);

            initialClient.RestClientId().Should().Be(sameClient.RestClientId());
            initialClient.RestClientId().Should().NotBe(refreshClient.RestClientId());
        }
    }
}