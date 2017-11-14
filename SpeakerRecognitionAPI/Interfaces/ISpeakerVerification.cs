using System.Collections.Generic;
using System.Threading.Tasks;
using SpeakerRecognitionAPI.Models;

namespace SpeakerRecognitionAPI.Interfaces
{
    public interface ISpeakerVerification
    {
        /// <summary>
        /// Creates an speaker verification profile with the specified locale.
        /// </summary>
        /// <returns>Profile response that contains the id of the created speaker verification profile.</returns>
        /// <param name="locale">Locale.</param>
        Task<ProfileVerification> CreateProfileAsync(string locale = "en-us");

        /// <summary>
        /// Sends an enrollment request.
        /// </summary>
        /// <returns>The enrollment status info.</returns>
        /// <param name="audioFilePath">Audio file path.</param>
        /// <param name="profileId">Speaker identification profile id.</param>
        Task<EnrollmentVerification> EnrollAsync(string audioFilePath, string profileId);

        /// <summary>
        /// Verifies if the speaker is the same registered person.
        /// </summary>
        /// <returns>The verification result info.</returns>
        /// <param name="audioFilePath">Audio file path.</param>
        /// <param name="profileId">Profile identifier.</param>
        Task<VerificationResponse> VerifyAsync(string audioFilePath, string profileId);

        /// <summary>
        /// Gets the supported phrases.
        /// </summary>
        /// <returns>Supported phrases.</returns>
        /// <param name="locale">Locale.</param>
        Task<IEnumerable<Phrase>> GetPhrasesAsync(string locale = "en-us");
    }
}
