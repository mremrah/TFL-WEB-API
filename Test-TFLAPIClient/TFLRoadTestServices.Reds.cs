using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using TflApiClient;
using TflApiClient.DTO;
using TflApiClient.Interface;
using Xbehave;
using Xunit;

namespace Test.TFLApiClient.Reds
{
    public class ConfigurationFeature
    {
        TflClient _program;
        public ConfigurationFeature()
        {
            _program = new TflClient();
            _program.Setup();

        }
        [Scenario(DisplayName = "Configuration keys should not be all defined")]
        public void ConfigurationKeys(string apiUrl, string appId, string appIdText, string appKey, string appKeyText)
        {
            "Api Url in Configuration file"
                .x(() => apiUrl = _program.Configuration.GetValue<string>("api-url", ""));

            "Api Url should not be empty or null"
                .x(() => apiUrl.Should().NotBeNullOrEmpty());

            "Api Url should not contain 'google', 'image' or 'cooking'"
                .x(() => apiUrl.Should().NotContainAll (new string[] { "google", "image", "cooking" }));

            "App Id in Configuration file"
                .x(() => appId = _program.Configuration.GetValue<string>("app-id", ""));

            "App Id should not be empty or null"
                .x(() => appId.Should().NotBeNullOrEmpty());

            "App Id Text Configuration value"
                .x(() => appIdText = _program.Configuration.GetValue<string>("app-id-text", ""));

            "App Id Text should not be empty or null"
                .x(() => appIdText.Should().NotBeNullOrEmpty());

            "App Key Configuration value"
                .x(() => appKey = _program.Configuration.GetValue<string>("app-key", ""));

            "App key should not be empty or null"
                .x(() => appKey.Should().NotBeNullOrEmpty());


            "App Key Text Configuration value"
                .x(() => appKeyText = _program.Configuration.GetValue<string>("app-key-text", ""));
            "App key text should not be empty or null"
                .x(() => appKeyText.Should().NotBeNullOrEmpty());


        }

    }
    public class TFLRoadFeatures
    {
        TflClient _program;
        IServiceEndpoint<RoadCorridor> _roadService;
        public TFLRoadFeatures()
        {
            _program = new TflClient();
            _program.Setup();
            _roadService = _program.ServiceProvider.GetService(typeof(IServiceEndpoint<RoadCorridor>)) as IServiceEndpoint<RoadCorridor>;

        }
        [Scenario(DisplayName = "Failing Road status queries")]
        [Example("A2B3")]
        [Example("KAK3")]
        [Example("EY4")]
        public void RoadQueryFeatures(string roadName, RoadCorridor roadCorridor, ApiError apiError)
        {
            $"Road service should not be null"
                .x(() => _roadService.Should().NotBeNull());
            $"And it should be query the road {roadName}"
                .x(async () => { (roadCorridor, apiError) = await _roadService.Get(roadName); });

            $"Than the road {roadName} query should return an api error DTO"
                .x(() => apiError.Should().NotBeNull());

            $"And road query result should be in type ApiError DTO"
                .x(() => apiError.Should().BeOfType(typeof(ApiError)));

            $"And api error result should be equal HTTP status code 404"
                .x(() => apiError.HttpStatusCode.Should().Be(404));

            $"Also api error result string should be contains 'is not a valid road' message and queried road name"
                .x(() => apiError.ToString().Should().ContainAll(new string[] { roadName, "is not a valid road" }));
        }

    }
}
