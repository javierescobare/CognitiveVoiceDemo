using System;
using Newtonsoft.Json;

namespace SpeakerRecognitionAPI.Models
{
    public class Phrase
    {
        [JsonProperty("phrase")]
        public string Text { get; set; }
    }
}
