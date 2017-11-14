using System;
namespace SpeakerRecognitionAPI.Models
{
    public class EnrollmentIdentification
    {
        public Status Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastActionDateTime { get; set; }
        public ProcessingResult ProcessingResult { get; set; }
        public string Message { get; set; }
    }

    public class ProcessingResult
    {
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public double RemainingEnrollmentSpeechTime { get; set; }
        public double SpeechTime { get; set; }
        public double EnrollmentSpeechTime { get; set; }
        public string IdentifiedProfileId { get; set; }
    }

    public enum Status
    {
        NotStarted,
        Running,
        Failed,
        Succeeded
    }

    public enum Confidence
    {
        Low,
        Normal,
        High
    }
}
