using System;
using Newtonsoft.Json;

namespace SpeakerRecognitionAPI.Models
{

    public class ServiceError
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }

    public class Error
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
