using System;
using System.Threading.Tasks;
using SpeakerRecognitionAPI.Helpers;
using SpeakerRecognitionAPI.Interfaces;
using VoicePay.Helpers;
using VoicePay.Services;
using VoicePay.Services.Interfaces;
using Xamarin.Forms;

namespace VoicePay.ViewModels.Enrollment
{
    public class AudioTrainingViewModel : AudioRecordingBaseViewModel
    {
        private readonly IBeepPlayer _beeper;
        private readonly ISpeakerVerification _verificationService;
        private string PhraseMessage => $"\"{EnrollmentProcess.SelectedPhrase}\"";

        private bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; RaisePropertyChanged(); }
        }

        public AudioTrainingViewModel() : this(VerificationService.Instance) { }
        public AudioTrainingViewModel(ISpeakerVerification verificationService)
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
                await WaitAndStartRecording();
                return;
            }

            IsBusy = true;

            StateMessage = "Analizando audio...";
            Message = string.Empty;

            try
            {
                var enrollmentResult = await VerificationService.Instance.EnrollAsync(audioFilePath, Settings.UserIdentificationId);
                EnrollmentProcess.SelectedPhrase = enrollmentResult.Phrase;

                if (enrollmentResult.RemainingEnrollments > 0)
                {
                    await StartRecording();
                }
                else
                {
                    IsCompleted = true;
                    StateMessage = "¡Muy bien! Hemos terminado.";
                    Settings.EnrolledPhrase = EnrollmentProcess.SelectedPhrase;
                }
            }
            catch (SpeakerRecognitionException ex)
            {
                if (ex.DetailedError.Message.Equals("InvalidPhrase", StringComparison.OrdinalIgnoreCase))
                {
                    StateMessage = "¡Ups! Parece que dijiste una frase no válida";
                    Message = "Intentémoslo denuevo...";
                    await WaitAndStartRecording();
                }
            }
            catch
            {
                StateMessage = "¡Ups! Algo extraño sucedió";
                Message = "Intentémoslo denuevo...";
                await WaitAndStartRecording();
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
