using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpeakerRecognitionAPI.Models;
using SpeakerRecognitionAPI.Constants;
using SpeakerRecognitionAPI.Interfaces;

namespace SpeakerRecognitionAPI
{
    public class SpeakerIdentificationClient : SpeakerServiceBase, ISpeakerIdentification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SpeakerRecognitionAPI.API.SpeakerIdentificationClient"/> class.
        /// </summary>
        /// <param name="subscriptionKey">Subscription key for the Speaker Recognition API.</param>
        public SpeakerIdentificationClient(string subscriptionKey) : base(subscriptionKey)
        {
        }

        /// <summary>
        /// Creates an speaker identification profile with the specified locale.
        /// </summary>
        /// <returns>Profile response that contains the id of the created speaker identification profile.</returns>
        /// <param name="locale">Locale.</param>
        public async Task<ProfileIdentification> CreateProfileAsync(string locale = "en-us")
        {
            var jsonResponse = await CreateProfileAsync(false, locale);
            var result = JsonConvert.DeserializeObject<ProfileIdentification>(jsonResponse);
            return result;
        }

        /// <summary>
        /// Sends an enrollment request.
        /// </summary>
        /// <returns>The tracking url for the enrollment request.</returns>
        /// <param name="audioFilePath">Audio file path.</param>
        /// <param name="profileId">Speaker identification profile id.</param>
        public async Task<string> EnrollAsync(string audioFilePath, string profileId)
        {
            try
            {
                var requestUri = string.Format(Endpoints.IdentificationEnroll.ToString(), profileId);
                var request = PrepareMediaRequest(audioFilePath, requestUri);

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    throw BuildErrorFromServiceResult(result);
                }

                var operationTrackingHeaders = response.Headers.GetValues("Operation-Location");
                var trackingUrl = operationTrackingHeaders.FirstOrDefault();
                return trackingUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Checks the enrollment status.
        /// </summary>
        /// <returns>The enrollment status.</returns>
        /// <param name="trackingUrl">Tracking URL.</param>
        /// <exception cref="T:System.ArgumentNullException">Throws exception if null or empty url.</exception>
        public async Task<EnrollmentIdentification> CheckEnrollmentStatusAsync(string trackingUrl)
        {
            if (string.IsNullOrEmpty(trackingUrl))
                throw new ArgumentNullException(nameof(trackingUrl), "Tracking url is null or empty");

            try
            {
                var response = await _httpClient.GetAsync(trackingUrl);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw BuildErrorFromServiceResult(jsonResponse);

                var result = JsonConvert.DeserializeObject<EnrollmentIdentification>(jsonResponse);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Identifies the given profile ids in the audio.
        /// </summary>
        /// <returns>The tracking url for the identification request.</returns>
        /// <param name="audioFilePath">Audio file path.</param>
        /// <param name="identificationProfileIds">Identification profile identifiers.</param>
        /// <exception cref="T:System.ArgumentException">Throws exception if no ids are provided.</exception>
        public async Task<string> IdentifyAsync(string audioFilePath, params string[] identificationProfileIds)
        {
            try
            {
                if (identificationProfileIds == null || !identificationProfileIds.Any())
                    throw new ArgumentException("No ids provided", nameof(identificationProfileIds));

                var profileIdsParam = string.Join(",", identificationProfileIds);
                var requestUri = string.Format(Endpoints.Identify.ToString(), profileIdsParam);
                var request = PrepareMediaRequest(audioFilePath, requestUri);

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    throw BuildErrorFromServiceResult(result);
                }

                var operationTrackingHeaders = response.Headers.GetValues("Operation-Location");
                var trackingUrl = operationTrackingHeaders.FirstOrDefault();
                return trackingUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Checks the identification status.
        /// </summary>
        /// <returns>The identification status.</returns>
        /// <param name="trackingUrl">Tracking URL.</param>
        /// <exception cref="T:System.ArgumentNullException">Throws exception if null or empty url.</exception>
        public async Task<EnrollmentIdentification> CheckIdentificationStatusAsync(string trackingUrl)
        {
            if (string.IsNullOrEmpty(trackingUrl))
                throw new ArgumentNullException(nameof(trackingUrl), "Tracking url is null or empty");

            try
            {
                var response = await _httpClient.GetAsync(trackingUrl);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw BuildErrorFromServiceResult(jsonResponse);

                var result = JsonConvert.DeserializeObject<EnrollmentIdentification>(jsonResponse);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex.Message);
                throw;
            }
        }

    }
}
