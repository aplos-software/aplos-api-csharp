using System;

namespace AplosApi
{
    public class ApiGatewayFactory
    {
        private IClientCredentialSource _credentialSource;

        public ApiGatewayFactory(Action<IConfigurer> configure)
        {
            var configurer = new Configurer(this);
            configure(configurer);
        }

        public IApiGateway BuildGateway()
        {
            var contextBuilder = new ClientContextBuilder(_credentialSource);

            var factory = new RestClientFactory(contextBuilder);

            var gateway = new ApiGateway(factory);

            return gateway;
        }

        public interface IConfigurer
        {
            void LoadPrivateKeyFromFile(string keyFile);
        }

        private class Configurer : IConfigurer
        {
            private readonly ApiGatewayFactory _configuration;

            public Configurer(ApiGatewayFactory configuration)
            {
                _configuration = configuration;
            }

            public void LoadPrivateKeyFromFile(string keyFile)
            {
                var credentialSource = new FileClientCredentialSource(keyFile);
                _configuration._credentialSource = credentialSource;
            }
        }
    }
}