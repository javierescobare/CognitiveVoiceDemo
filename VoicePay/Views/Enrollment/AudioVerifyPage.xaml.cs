using System;
using System.Collections.Generic;
using VoicePay.ViewModels.Enrollment;
using Xamarin.Forms;

namespace VoicePay.Views.Enrollment
{
    public partial class AudioVerifyPage : ContentPage
    {
        AudioVerifyViewModel ViewModel => BindingContext as AudioVerifyViewModel;
        public AudioVerifyPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            ViewModel.IsPageActive = true;
            await ViewModel.StartRecording();
        }

        protected override async void OnDisappearing()
        {
            await ViewModel.Stop();
            ViewModel.IsPageActive = false;
        }
    }
}
