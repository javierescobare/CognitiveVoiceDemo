using System;
using Android.Media;
using VoicePay.Services.Interfaces;
using VoicePay.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(BeepPlayerImpl))]
namespace VoicePay.Droid.Services
{
    public class BeepPlayerImpl : IBeepPlayer
    {
        public void Beep()
        {
            ToneGenerator toneGen = new ToneGenerator(Stream.Music, 100);
            toneGen.StartTone(Tone.CdmaConfirm, 500);
        }
    }
}
