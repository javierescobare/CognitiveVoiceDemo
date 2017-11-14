using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace VoicePay.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //  TODO: Change it
        public void DisplayAlert(string title, string message, string cancel)
        {
            Device.BeginInvokeOnMainThread(async () => 
            {
                await Application.Current.MainPage.DisplayAlert(title, message, cancel);
            });
        }
    }
}
