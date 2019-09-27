using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using TflApiClient;
using TflApiClient.DTO;
using TflApiClient.Interface;
using Xbehave;
using Xunit;

namespace Test.TFLApiClient.Greens
{
    public class ConfigurationFeature
    {
        TflClient _program;
        public ConfigurationFeature()
        {
            _program = new TflClient();
            _program.Setup();

        }
        [Scenario(DisplayName = "Configuration keys should all be defined")]
        public void ConfigurationKeys(string apiUrl, string appId, string appIdText, string appKey, string appKeyText)
        {
            "Api Url in Configuration file"
                .x(() => apiUrl = _program.Configuration.GetValue<string>("api-url", ""));

            "Api Url should be TFL API URL"
                .x(() => apiUrl.Should().BeEquivalentTo("https://api.tfl.gov.uk"));

            "App Id in Configuration file"
                .x(() => appId = _program.Configuration.GetValue<string>("app-id", ""));

            "App Id should have length at least 8 chars"
                .x(() => appId.Length.Should().BeGreaterOrEqualTo(8));

            "App Id Text Configuration value"
                .x(() => appIdText = _program.Configuration.GetValue<string>("app-id-text", ""));

            "App Id Text should be 'app_id'"
                .x(() => appIdText.Should().BeEquivalentTo("app_id"));

            "App Key Configuration value"
                .x(() => appKey = _program.Configuration.GetValue<string>("app-key", ""));

            "App key should have length at least 30 chars"
                .x(() => appKey.Length.Should().BeGreaterOrEqualTo(30));


            "App Key Text Configuration value"
                .x(() => appKeyText = _program.Configuration.GetValue<string>("app-key-text", ""));
            "App key text should be 'app_key'"
                .x(() => appKeyText.Should().BeEquivalentTo("app_key"));
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
        [Scenario(DisplayName = "Successfull Road status query")]
        [Example("A2")]
        [Example("A3")]
        [Example("A4")]
        public void RoadQueryFeatures(string roadName, RoadCorridor roadCorridor, ApiError apiError)
        {
            $"Road service should not be null"
                .x(() => _roadService.Should().NotBeNull());

            $"And it should be query the road {roadName}"
                .x(async () => { (roadCorridor, apiError) = await _roadService.Get(roadName); });

            $"Than the road {roadName} query should not return an api error DTO"
                .x(() => apiError.Should().BeNull());

            $"And road query result should be in type RoadCorridor DTO"
                .x(() => roadCorridor.Should().BeOfType(typeof(RoadCorridor)));

            $"And road query result should be equal queried road name as well"
                .x(() => roadCorridor.DisplayName.ToLower().Should().BeEquivalentTo(roadName.ToLower()));

            $"Also road query result string should be contains 'Road Status' and 'Road Status Description' texts"
                .x(() => roadCorridor.ToString().Should().ContainAll(new string[] { "Road Status", "Road Status Description" }));
        }

    }
}

