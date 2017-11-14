using Newtonsoft.Json;
namespace SpeakerRecognitionAPI.Models
{
    public class ProfileIdentification
    {
        [JsonProperty("identificationProfileId")]
        public string IdentificationProfileId { get; set; }
    }
}
