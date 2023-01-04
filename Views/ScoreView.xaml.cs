using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for ScoreView.xaml
    /// </summary>
    public partial class ScoreView : Page
    {
        public ScoreView()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/InicioView.xaml", UriKind.Relative));
        }
    }
}
