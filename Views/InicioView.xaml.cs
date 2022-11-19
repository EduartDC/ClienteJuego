using ClienteJuego.ConnectService;
using ClienteJuego.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for InicioView.xaml
    /// </summary>
    public partial class InicioView : Page
    {

        public Player playerInfo;
        private string userName;

        public InicioView()
        {
            InitializeComponent();
            Accessories.PlayMusic();
            string selected_dept = (App.Current as App).DeptName;
            userName = selected_dept;
            TextUserName.Text = "Hola de nuevo "+selected_dept;
            ImageSource imageSource = new ImageSourceConverter().ConvertFromString(Accessories.LoadConfigPlayer(selected_dept)) as ImageSource;
            imgAvatar.Source = imageSource;

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/GameModeView.xaml", UriKind.Relative));
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/MenuOptionsView.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/GameModeView.xaml", UriKind.Relative));
        }

        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            Accessories.PlaySoundsEffects();
            NavigationService.Navigate(new Uri("Views/ScoreView.xaml", UriKind.Relative));
        }

    }
}
