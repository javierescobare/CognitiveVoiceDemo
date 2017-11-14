using System;
using SpeakerRecognitionAPI;

namespace VoicePay.Services
{
    public class VerificationService
    {
        static readonly SpeakerVerificationClient _instance = new SpeakerVerificationClient(Constants.Keys.SpeakerRecognitionApiKey);
        public static SpeakerVerificationClient Instance => _instance ;
    }
}
