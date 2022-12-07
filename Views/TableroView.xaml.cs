using ClienteJuego.ConnectService;
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
using System.Windows.Threading;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for TableroView.xaml
    /// </summary>
    public partial class TableroView : Page
    {
        private int count;

        private static int allvalue;

        private static int value1;
        private static int value2;
        private static int value3;
        private static int value4;
        private static int value5;
        private static int round = -1;
        static List<QuestionServer> questions;

        public TableroView()
        {
            InitializeComponent();
            count = 0;
        }

        public void resetValues()
        {
            value1 = 0;
            value2 = 0;
            value3 = 0;
            value4 = 0;
            value5 = 0;
            lblAnswer1.Content = ". . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .";
            lblAnswer2.Content = ". . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .";
            lblAnswer3.Content = ". . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .";
            lblAnswer4.Content = ". . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .";
            lblAnswer5.Content = ". . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .";
            lblScoreAnswer1.Content = "0";
            lblScoreAnswer2.Content = "0";
            lblScoreAnswer3.Content = "0";
            lblScoreAnswer4.Content = "0";
            lblScoreAnswer5.Content = "0";
            lblScorePoints.Content = "0";
            allvalue = 0;
        }

        public void chargeRound(int i)
        {
            resetValues();
            lblStatusRoundOff.Content = Properties.Resources.textStatusRoundOn;
            //lblQuestion.Content = questions[i].Answers;
            //MessageBox.Show("Cargando la \'Ronda " + (i + 1) + "\'");
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            round = 0;
            chargeRound(round);
            startStopwatch(true);
        }

        private void startStopwatch(Boolean status)
        {            
            DispatcherTimer objectTimer = new DispatcherTimer();
            objectTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            objectTimer.Tick += (a, b) =>
            {
                count++;
                lblTimer.Content = count.ToString();
            };

            if (status == true)
            {
                objectTimer.Start();
            }
            else if(status == false)
            {
                objectTimer.Stop();
                count = 0;
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            int result = (int)MessageBox.Show("¿Estás seguro(a) de salir de la ronda?", "¡Regresar a modo de juego?", MessageBoxButton.YesNo);
            if (result == 6)
            {
                NavigationService.Navigate(new Uri("Views/InicioView.xaml", UriKind.Relative));
            }            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            startStopwatch(false);
        }
    }
}
