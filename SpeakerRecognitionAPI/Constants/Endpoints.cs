using System;
namespace SpeakerRecognitionAPI.Constants
{
    internal static class Endpoints
    {
        #region Identification

        internal readonly static WestUsSpeakerRecognitionEndpoint IdentificationCreateProfile
                = new WestUsSpeakerRecognitionEndpoint("/identificationProfiles");

        internal readonly static WestUsSpeakerRecognitionEndpoint IdentificationEnroll 
                = new WestUsSpeakerRecognitionEndpoint("/identificationProfiles/{0}/enroll");

        internal readonly static WestUsSpeakerRecognitionEndpoint Identify
                = new WestUsSpeakerRecognitionEndpoint("/identify?identificationProfileIds={0}");

        #endregion


        #region Verification

        internal readonly static WestUsSpeakerRecognitionEndpoint VerificationCreateProfile
                = new WestUsSpeakerRecognitionEndpoint("/verificationProfiles");

        internal readonly static WestUsSpeakerRecognitionEndpoint VerificationEnroll
                = new WestUsSpeakerRecognitionEndpoint("/verificationProfiles/{0}/enroll");

        internal readonly static WestUsSpeakerRecognitionEndpoint Verify
                = new WestUsSpeakerRecognitionEndpoint("/verify?verificationProfileId={0}");

        internal readonly static WestUsSpeakerRecognitionEndpoint VerificationPhrases
                = new WestUsSpeakerRecognitionEndpoint("/verificationPhrases?locale={0}");

        #endregion


        internal class WestUsSpeakerRecognitionEndpoint
        {
            private const string BaseServiceUri = "https://westus.api.cognitive.microsoft.com/spid/v1.0";
            private readonly string _relativePath;

            public WestUsSpeakerRecognitionEndpoint(string relativePath)
            {
                _relativePath = relativePath;
            }

            public override string ToString()
            {
                return $"{BaseServiceUri}{_relativePath}";
            }
        }
    }

}
