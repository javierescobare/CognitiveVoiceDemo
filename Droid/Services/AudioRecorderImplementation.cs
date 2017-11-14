using System;
using System.IO;
using Android;
using Android.Media;
using Android.Support.V4.App;
using Plugin.CurrentActivity;
using VoicePay.Droid.Services;
using VoicePay.Services.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AudioRecorderImplementation))]
namespace VoicePay.Droid.Services
{
    public class AudioRecorderImplementation : IAudioRecorder
    {
        private MediaRecorder _recorder;
        private string _fullFilePath;
        private string _path;

        private string _fileName = "audio.mpeg4";
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private bool _isRecording = false;
        public bool IsRecording => _isRecording;

        private bool _isSetUp;
        public bool IsSetUp => _isSetUp;

        //  TODO: Remove it
        MediaPlayer _player;

        public AudioRecorderImplementation()
        {
            _recorder = new MediaRecorder();
            _path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            //  TODO: Remove it
            _player = new MediaPlayer();
            _player.Completion += (sender, e) => { _player.Reset(); };
        }

        public void SetUp()
        {
            _recorder.SetAudioSource(AudioSource.Mic);
            _recorder.SetOutputFormat(OutputFormat.Default);
            _recorder.SetAudioEncoder(AudioEncoder.Default);
            _fullFilePath = $"{_path}/{_fileName}";
            _recorder.SetOutputFile(_fullFilePath);
            _recorder.Prepare();
            _isSetUp = true;
        }

        public void StartRecording()
        {
            if (!_isSetUp) SetUp();

            _recorder.Start();
            _isRecording = true;
        }

        public void StopRecording()
        {
            if (!_isRecording) return;

            _recorder.Stop();
            _recorder.Reset();

            _isRecording = false;
            _isSetUp = false;

            //  TODO: Remove it
            _player.SetDataSource(_fullFilePath);
            _player.Prepare();
            _player.Start();
        }

        public void Release()
        {
            if (_recorder == null) return;

            _recorder.Release();
            _recorder.Dispose();
            _recorder = null;
        }

        public string GetLastRecordedFilePath()
        {
            return _fullFilePath;
        }
    }
}
