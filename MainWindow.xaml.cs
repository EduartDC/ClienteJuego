using ClienteJuego.Views;
using System.Windows;

namespace ClienteJuego
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Contenedor.NavigationService.Navigate(new LoginView());

        }
        /*private void MainFrame_OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            var ta = new ThicknessAnimation();
            ta.Duration = TimeSpan.FromSeconds(0.3);
            ta.DecelerationRatio = 0.7;
            ta.To = new Thickness(0, 0, 0, 0);
            if (e.NavigationMode == NavigationMode.New && !e.Content.Equals(null))
            {
                ta.From = new Thickness(500, 0, 0, 0);
                (e.Content as Page).BeginAnimation(MarginProperty, ta);
            }
            /*else if (e.NavigationMode == NavigationMode.Back)
            {
                ta.From = new Thickness(0, 0, 500, 0);
            }
            //(e.Content as Page).BeginAnimation(MarginProperty, ta);
        }
         */
    }
}
