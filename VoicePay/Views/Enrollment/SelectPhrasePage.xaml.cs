using System;
using System.Collections.Generic;
using SpeakerRecognitionAPI.Models;
using VoicePay.Helpers;
using VoicePay.ViewModels.Enrollment;
using Xamarin.Forms;

namespace VoicePay.Views.Enrollment
{
    public partial class SelectPhrasePage : ContentPage
    {
        public SelectPhraseViewModel ViewModel => BindingContext as SelectPhraseViewModel;

        public SelectPhrasePage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            //Deselect Item
            ((ListView)sender).SelectedItem = null;

            var phrase = e.Item as Phrase;
            EnrollmentProcess.SelectedPhrase = phrase.Text;
            await Navigation.PushAsync(new AudioRecordingPage());
        }

        protected async override void OnAppearing()
        {
            await ViewModel.GetPhrases();
        }
    }
}
