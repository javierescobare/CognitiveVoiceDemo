using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace VoicePay.Views.Enrollment
{
    public partial class CorrectResultPage : ContentPage
    {
        public CorrectResultPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new WelcomePage());
        }
    }
}
