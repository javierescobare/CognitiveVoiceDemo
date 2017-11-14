// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace VoicePay.Helpers
{
    public static class Settings
    {
        static ISettings settings;
        public static ISettings Instance
        {
            get
            {
                return settings ?? CrossSettings.Current;
            }
            set
            {
                settings = value;
            }
        }

        public static string UserIdentificationId
        {
            get => Instance.GetValueOrDefault(nameof(UserIdentificationId), string.Empty);
            set => Instance.AddOrUpdateValue(nameof(UserIdentificationId), value);
        }

        public static string EnrolledPhrase
        {
            get => Instance.GetValueOrDefault(nameof(EnrolledPhrase), string.Empty);
            set => Instance.AddOrUpdateValue(nameof(EnrolledPhrase), value);
        }
    }

}