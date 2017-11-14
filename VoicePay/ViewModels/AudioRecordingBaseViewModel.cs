using System;
using System.Threading.Tasks;
using Plugin.AudioRecorder;

namespace VoicePay.ViewModels
{
    public abstract class AudioRecordingBaseViewModel : BaseViewModel
    {
        protected readonly AudioRecorderService Recorder;

        private string _stateMessage;
        public string StateMessage
        {
            get { return _stateMessage; }
            set { _stateMessage = value; RaisePropertyChanged(); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged(); }
        }

        protected AudioRecordingBaseViewModel()
        {
            Recorder = new AudioRecorderService
            {
                AudioSilenceTimeout = TimeSpan.FromSeconds(4),
                StopRecordingOnSilence = true,
                StopRecordingAfterTimeout = false,
                PreferredSampleRate = 16000,
                SilenceThreshold = 0.1f
            };
        }

        protected async Task WaitAndStartRecording()
        {
            await Task.Delay(3000);
            await StartRecording();
        }

        public abstract Task StartRecording();

        public async Task Stop()
        {
            await Recorder.StopRecording(false);
        }

    }
}
