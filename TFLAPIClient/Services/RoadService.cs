using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TflApiClient.DTO;
using TflApiClient.Extensions;
using TflApiClient.Interface;

namespace TflApiClient.Services
{
    /// <summary>
    /// Road status query service
    /// </summary>
    public class RoadService : ApiConfigBase, IServiceEndpoint<RoadCorridor>
    {
        #region Private Fiels

        private readonly HttpClient _client;
        private ILogger _logger;
        private IConfigurationRoot _configuration;

        private string _path = "Road";

        #endregion

        public RoadService(ILogger logger, IConfigurationRoot configuration) : base(configuration)
        {
            _client = new HttpClient() { Timeout = TimeSpan.FromSeconds(90) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "TFL API Client");

            _logger = logger;
            _configuration = configuration;
        }

        public void SetServicePath(string path = "Road")
        {
            _path = string.IsNullOrEmpty(path) ? "Road" : path;
        }

        public async Task<(RoadCorridor, ApiError)> Get(string queryString = "")
        {
            if (string.IsNullOrEmpty(queryString))
            {
                _logger.Log("Query string parameter is missing!", "parameter required");
                return (null, new ApiError() { ExceptionType = "MissingQueryParameter", Message = "Query string can not be empty" });
            }

            try
            {
                var url = await UrlBuilder(queryString);
                _logger.Log(url, "Query URL");
                var httpResponse = await _client.GetAsync(url);
                if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.Log($"Query from the service with parameter {queryString} was failed!", "Service Query Failed");

                    var errorContent = await httpResponse.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(errorContent))
                    {
                        return
                            (null,
                            new ApiError(queryString)
                            {
                                ExceptionType = "HTTP Error",
                                HttpStatus = httpResponse.StatusCode + "",
                                Message = "HTTP request failed!",
                                RelativeUri = url,
                                TimestampUtc = new DateTimeOffset(DateTime.Now),
                                QueryString=queryString
                            });
                    }
                    else
                    {
                        var apiError = errorContent.GetDTO<ApiError>();
                        apiError.QueryString = queryString;
                        return (null, apiError);
                    }
                }
                else
                {
                    _logger.Log($"Query from the service with parameter {queryString} was successful!", "Service Query Succeeded");

                    var successContent = await httpResponse.Content.ReadAsStringAsync();
                    var roadCorridor = successContent.GetDTO<List<RoadCorridor>>();
                    if (roadCorridor.Any())
                    {
                        return (roadCorridor.FirstOrDefault(), null);
                    }
                    return (null, new ApiError() { ExceptionType = "Serialization Error", Message = "Not serialized record found!" });
                }

            }
            catch (Exception ex)
            {
                _logger.Log("Get Operation Failed", "Service Error", ex);
                return (null, new ApiError()
                {
                    ExceptionType = "Internal Error",
                    Message = ex.Message + Environment.NewLine + ex.StackTrace
                });
            }

        }


        public async Task<string> UrlBuilder(string queryString)
        {
            StringBuilder queryPattern = new StringBuilder();
            _logger.Log(ApiUrl, "ApiUrl");
            queryPattern.AppendFormat("{0}/{1}/{2}", ApiUrl, _path, queryString);
            if (IsApiKeyRequired)
            {
                queryPattern.Append($"?{AppIdText}={AppId}&{AppKeyText}={AppKey}");
            }
            return queryPattern.ToString();
        }
    }
}
