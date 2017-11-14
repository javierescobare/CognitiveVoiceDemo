using System;
using System.Threading.Tasks;
using SpeakerRecognitionAPI.Models;

namespace SpeakerRecognitionAPI.Interfaces
{
    public interface ISpeakerIdentification
    {
        /// <summary>
        /// Creates an speaker identification profile with the specified locale.
        /// </summary>
        /// <returns>Profile response that contains the id of the created speaker identification profile.</returns>
        /// <param name="locale">Locale.</param>
        Task<ProfileIdentification> CreateProfileAsync(string locale = "en-us");


        /// <summary>
        /// Sends an enrollment request.
        /// </summary>
        /// <returns>The tracking url for the enrollment request.</returns>
        /// <param name="audioFilePath">Audio file path.</param>
        /// <param name="profileId">Speaker identification profile id.</param>
        Task<string> EnrollAsync(string audioFilePath, string profileId);

        /// <summary>
        /// Checks the enrollment status.
        /// </summary>
        /// <returns>The enrollment status.</returns>
        /// <param name="trackingUrl">Tracking URL.</param>
        Task<EnrollmentIdentification> CheckEnrollmentStatusAsync(string trackingUrl);


        /// <summary>
        /// Identifies the given profile ids in the audio.
        /// </summary>
        /// <returns>The tracking url for the identification request.</returns>
        /// <param name="audioFilePath">Audio file path.</param>
        /// <param name="identificationProfileIds">Identification profile identifiers.</param>
        Task<string> IdentifyAsync(string audioFilePath, params string[] identificationProfileIds);


        /// <summary>
        /// Checks the identification status.
        /// </summary>
        /// <returns>The identification status.</returns>
        /// <param name="trackingUrl">Tracking URL.</param>
        Task<EnrollmentIdentification> CheckIdentificationStatusAsync(string trackingUrl);

    }
}
