using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SpeakerRecognitionAPI.Models
{
    public class EnrollmentVerification
    {
        /// <summary>
        /// The enrollment status of the profile
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EnrollmentStatus EnrollmentStatus { get; set; }

        /// <summary>
        /// The number of remaining enrollments for a profile
        /// </summary>
        public int RemainingEnrollments { get; set; }

        /// <summary>
        /// The current speaker verification profile enrollments count.
        /// </summary>
        public int EnrollmentsCount { get; set; }

        /// <summary>
        /// The verification phrase used by the speaker
        /// </summary>
        public string Phrase { get; set; }
    }
}
