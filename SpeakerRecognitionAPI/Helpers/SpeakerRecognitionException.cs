using System;
using SpeakerRecognitionAPI.Models;

namespace SpeakerRecognitionAPI.Helpers
{
    public class SpeakerRecognitionException : Exception
    {
        public SpeakerRecognitionException(string message) : base (message)
        {
        }

        public SpeakerRecognitionException()
        {
        }

        public Error DetailedError { get; set; }
    }
}
