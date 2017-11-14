using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace VoicePay.Views.Enrollment
{
    public partial class IncorrectResultPage : ContentPage
    {
        public IncorrectResultPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new WelcomePage());
        }
    }
}
