using Microsoft.Extensions.Configuration;

namespace TflApiClient.Services
{
    /// <summary>
    /// API Configuration settings, required by service consumers
    /// </summary>
    public class ApiConfigBase
    {
        IConfigurationRoot _configuration;
        public ApiConfigBase(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }
        protected string ApiUrl => _configuration.GetValue<string>("api-url", "https://api.tfl.gov.uk");
        protected string AppId => _configuration.GetValue<string>("app-id", "");
        protected string AppKey => _configuration.GetValue<string>("app-key", "");
        protected string AppIdText => _configuration.GetValue<string>("app-id-text", "");
        protected string AppKeyText => _configuration.GetValue<string>("app-key-text", "");
        protected bool IsApiKeyRequired => _configuration.GetValue<bool>("is-app-key-required", false);
    }
}
