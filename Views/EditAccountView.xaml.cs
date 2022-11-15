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
    /// Interaction logic for EditAccountView.xaml
    /// </summary>
    public partial class EditAccountView : Page
    {
        public EditAccountView()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            int resultado = (int)MessageBox.Show("¿Estás seguro(a) de salir? No se guardarán las modificaciones.", "Cuidado!", MessageBoxButton.YesNo);
            if (resultado == 6)
            {
                NavigationService.Navigate(new Uri("Views/AccountView.xaml", UriKind.Relative));
            }
        }
    }
}
