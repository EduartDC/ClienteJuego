using ClienteJuego.Properties;
using System;
using System.Configuration;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Page
    {
        private readonly Configuration audioConfiguration;
        private readonly KeyValueConfigurationElement musicEffect;
        private readonly KeyValueConfigurationElement soundsEffect;
        private string language;
        public SettingsView()
        {
            audioConfiguration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            musicEffect = audioConfiguration.AppSettings.Settings["MUSIC_EFFECT"];
            soundsEffect = audioConfiguration.AppSettings.Settings["SOUND_EFFECT"];
            InitializeComponent();
            language = "";
            MusicEffects.IsChecked = musicEffect.Value.Equals("true");
            SoundsEffects.IsChecked = soundsEffect.Value.Equals("true");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/MenuOptionsView.xaml", UriKind.Relative));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            audioConfiguration.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void MusicChecked(object sender, RoutedEventArgs e)
        {
            musicEffect.Value = "true";

        }

        private void MusicUnchecked(object sender, RoutedEventArgs e)
        {
            musicEffect.Value = "false";
        }

        private void SoundsUnchecked(object sender, RoutedEventArgs e)
        {
            soundsEffect.Value = "false";
        }

        private void SoundsChecked(object sender, RoutedEventArgs e)
        {
            soundsEffect.Value = "true";

        }

        private void btnSpanishLanguage_Click(object sender, RoutedEventArgs e)
        {
            language = "es-MX";
        }

        private void btnEnglishLanguage_Click(object sender, RoutedEventArgs e)
        {
            language = "";
        }
    }
}
