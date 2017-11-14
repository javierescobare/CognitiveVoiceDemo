using System;
using System.Threading.Tasks;
using SpeakerRecognitionAPI.Interfaces;
using SpeakerRecognitionAPI.Models;
using VoicePay.Helpers;
using VoicePay.Services;
using VoicePay.Services.Interfaces;
using VoicePay.Views.Enrollment;
using Xamarin.Forms;

namespace VoicePay.ViewModels.Enrollment
{
    public class AudioVerifyViewModel : AudioRecordingBaseViewModel
    {
        private readonly IBeepPlayer _beeper;
        private readonly ISpeakerVerification _verificationService;
        private string PhraseMessage => $"\"{Settings.EnrolledPhrase}\"";
        public bool IsPageActive { get; set; } = true;

        public AudioVerifyViewModel() : this(VerificationService.Instance) { }
        public AudioVerifyViewModel(ISpeakerVerification verificationService)
        {
            StateMessage = "Espera...";

            _beeper = DependencyService.Get<IBeepPlayer>();
            _verificationService = verificationService;

            Recorder.AudioInputReceived += async (object sender, string e) => { await Recorder_AudioInputReceived(sender, e); };
        }

        private async Task Recorder_AudioInputReceived(object sender, string audioFilePath)
        {
            if (string.IsNullOrEmpty(audioFilePath))
            {
                StateMessage = "No logramos escucharte :/";
                Message = "Intenta hablando mas fuerte";
                if(IsPageActive)
                    await WaitAndStartRecording();
                return;
            }

            IsBusy = true;

            StateMessage = "Verificando tu voz...";
            Message = string.Empty;

            try
            {
                var verificationResponse = await _verificationService.VerifyAsync(audioFilePath, Settings.UserIdentificationId);
                if (verificationResponse.Result == Result.Accept && verificationResponse.Confidence != Confidence.Low)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new CorrectResultPage());
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new IncorrectResultPage());
                    });
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override async Task StartRecording()
        {
            await Recorder.StartRecording();
            _beeper.Beep();
            StateMessage = "Escuchando...";
            Message = PhraseMessage;
        }
    }
}
