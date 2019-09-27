using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TflApiClient.DTO
{
    /// <summary>
    /// Road Corridor Data Transfer Object (DTO)
    /// </summary>
    public class RoadCorridor
    {
        [JsonProperty("$type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("statusSeverity")]
        public string StatusSeverity { get; set; }

        [JsonProperty("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }

        [JsonProperty("bounds")]
        public string Bounds { get; set; }

        [JsonProperty("envelope")]
        public string Envelope { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonIgnore]
        public int ExitCode { get; set; }

        public override string ToString()
        {
            ExitCode = 1;
            if (string.IsNullOrEmpty(DisplayName))
            {
                return "Error:Display name is null or empty!";
            }
            if (string.IsNullOrEmpty(StatusSeverity))
            {
                return "Error:Status severity is null or empty!";
            }

            if (string.IsNullOrEmpty(StatusSeverity))
            {
                return "Error:Status severity is null or empty!";
            }
            ExitCode = 0;
            return $"The status of the {DisplayName} as follows" + Environment.NewLine +
                    "\t" + $"Road Status is {StatusSeverity}" + Environment.NewLine +
                    "\t" + $"Road Status Description is {StatusSeverityDescription}";
        }
    }
}
