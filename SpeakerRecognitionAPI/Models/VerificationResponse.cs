using System;
namespace SpeakerRecognitionAPI.Models
{
    public class VerificationResponse
    {
        public Result Result { get; set; }
        public Confidence Confidence { get; set; }
        public string Phrase { get; set; }
    }

    public enum Result
    {
        Accept,
        Reject
    }

}
