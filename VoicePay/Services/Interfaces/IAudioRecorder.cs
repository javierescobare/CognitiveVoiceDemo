using System;
namespace VoicePay.Services.Interfaces
{
    public interface IAudioRecorder
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:VoicePay.Services.Interfaces.IAudioRecorder"/> is set up.
        /// </summary>
        /// <value><c>true</c> if is set up; otherwise, <c>false</c>.</value>
        bool IsSetUp { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:VoicePay.Services.Interfaces.IAudioRecorder"/> is recording.
        /// </summary>
        /// <value><c>true</c> if is recording; otherwise, <c>false</c>.</value>
        bool IsRecording { get; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        string FileName { get; set; }

        /// <summary>
        /// Sets up the audio recorder. You must call it before each recording.
        /// </summary>
        void SetUp();

        /// <summary>
        /// Starts the recording.
        /// </summary>
        void StartRecording();

        /// <summary>
        /// Stops the recording.
        /// </summary>
        void StopRecording();

        /// <summary>
        /// Gets the last recorded file path.
        /// </summary>
        /// <returns>The last recorded file path.</returns>
        string GetLastRecordedFilePath();

        /// <summary>
        /// Release this instance.
        /// </summary>
        void Release();
    }
}
