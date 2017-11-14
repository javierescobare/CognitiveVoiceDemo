using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpeakerRecognitionAPI.Constants;
using SpeakerRecognitionAPI.Interfaces;
using SpeakerRecognitionAPI.Models;

namespace SpeakerRecognitionAPI
{
    public class SpeakerVerificationClient : SpeakerServiceBase, ISpeakerVerification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SpeakerRecognitionAPI.SpeakerVerificationClient"/> class.
        /// </summary>
        /// <param name="subscriptionKey">Subscription key for the Speaker Recognition API.</param>
        public SpeakerVerificationClient(string subscriptionKey) : base(subscriptionKey)
        {
        }

        /// <summary>
        /// Creates an speaker verification profile with the specified locale.
        /// </summary>
        /// <returns>Profile response that contains the id of the created speaker verification profile.</returns>
        /// <param name="locale">Locale.</param>
        public async Task<ProfileVerification> CreateProfileAsync(string locale = "en-us")
        {
            var jsonResponse = await CreateProfileAsync(true, locale);
            var result = JsonConvert.DeserializeObject<ProfileVerification>(jsonResponse);
            return result;
        }

        /// <summary>
        /// Sends an enrollment request.
        /// </summary>
        /// <returns>The enrollment status info.</returns>
        /// <param name="audioFilePath">Audio file path.</param>
        /// <param name="profileId">Speaker identification profile id.</param>
        public async Task<EnrollmentVerification> EnrollAsync(string audioFilePath, string profileId)
        {
            try
            {
                var requestUri = string.Format(Endpoints.VerificationEnroll.ToString(), profileId);
                var request = PrepareMediaRequest(audioFilePath, requestUri);

                var response = await _httpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw BuildErrorFromServiceResult(jsonResponse);
                

                var result = JsonConvert.DeserializeObject<EnrollmentVerification>(jsonResponse);
                return result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex.Message);
                throw;
            }

        }

        /// <summary>
        /// Verifies if the speaker is the same registered person.
        /// </summary>
        /// <returns>The verification result info.</returns>
        /// <param name="audioFilePath">Audio file path.</param>
        /// <param name="profileId">Profile identifier.</param>
        public async Task<VerificationResponse> VerifyAsync(string audioFilePath, string profileId)
        {
            try
            {
                var requestUri = string.Format(Endpoints.Verify.ToString(), profileId);
                var request = PrepareMediaRequest(audioFilePath, requestUri);

                var response = await _httpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw BuildErrorFromServiceResult(jsonResponse);

                var result = JsonConvert.DeserializeObject<VerificationResponse>(jsonResponse);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex);
                throw;
            }
        }


        /// <summary>
        /// Gets the supported phrases.
        /// </summary>
        /// <returns>Supported phrases.</returns>
        /// <param name="locale">Locale.</param>
        public async Task<IEnumerable<Phrase>> GetPhrasesAsync(string locale = "en-us")
        {
            try
            {
                var requestUri = string.Format(Endpoints.VerificationPhrases.ToString(), locale);
                var response = await _httpClient.GetAsync(requestUri);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<Phrase>>(jsonResponse);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex);
                throw;
            }
        }
    }
}
