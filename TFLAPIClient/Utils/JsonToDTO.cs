using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TflApiClient.Extensions
{
    public static class JsonToDTO
    {
        static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
        /// <summary>
        /// String extension method to de-serialize json string to generic type object
        /// </summary>
        /// <typeparam name="T">Generic type </typeparam>
        /// <param name="json">json string</param>
        /// <returns></returns>
        public static T GetDTO<T>(this string json) => JsonConvert.DeserializeObject<T>(json, Settings);
    }
}
