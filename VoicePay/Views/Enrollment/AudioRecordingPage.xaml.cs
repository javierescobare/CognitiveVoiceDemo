using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoicePay.ViewModels.Enrollment;
using Xamarin.Forms;

namespace VoicePay.Views.Enrollment
{
    public partial class AudioRecordingPage : ContentPage
    {
        AudioTrainingViewModel ViewModel => BindingContext as AudioTrainingViewModel;

        public AudioRecordingPage()
        {
            InitializeComponent();
            CompletedAnimated.PropertyChanged += CompletedAnimated_PropertyChanged;
        }

        protected override async void OnAppearing()
        {
            await ViewModel.StartRecording();
        }

        async void CompletedAnimated_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsVisible"))
            {
                CompletedAnimated.Play();
                await Task.Delay(3000);
                await Navigation.PopToRootAsync(false);
            }
        }

        //  Prevent from going back
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
