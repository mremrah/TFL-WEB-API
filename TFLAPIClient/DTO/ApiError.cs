using Newtonsoft.Json;
using System;

namespace TflApiClient.DTO
{
     /// <summary>
    /// Api Error Data Transfer Object (DTO)
    /// </summary>   
    public class ApiError
    {
        public ApiError()
        {
        }
        public ApiError(string queryString)
        {
            QueryString = queryString;
        }

        [JsonProperty("$type")]
        public string Type { get; set; }

        [JsonProperty("timestampUtc")]
        public DateTimeOffset TimestampUtc { get; set; }

        [JsonProperty("exceptionType")]
        public string ExceptionType { get; set; }

        [JsonProperty("httpStatusCode")]
        public long HttpStatusCode { get; set; }

        [JsonProperty("httpStatus")]
        public string HttpStatus { get; set; }

        [JsonProperty("relativeUri")]
        public string RelativeUri { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonIgnore]
        public string QueryString { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(QueryString))
            {
                return $"Error: {ExceptionType} {Message}";
            }
            else
            {
                return $"{QueryString} is not a valid road!";
            }
        }
    }
}
