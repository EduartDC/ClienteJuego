using ClienteJuego.ConnectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
    /// Interaction logic for TableroView.xaml
    /// </summary>
    public partial class TableroView : Page, ConnectService.IMatchServiceCallback
    {
        public MatchServer match { get; set; }
        private readonly ConnectService.MatchServiceClient matchServiceClient;
        QuestionServer question;
        AnswerServer[] answers;
        public TableroView(MatchServer match)
        {

            InitializeComponent();
            matchServiceClient = new MatchServiceClient(new InstanceContext(this));
            this.match = match;
            question = matchServiceClient.GetQuestions();
            answers = matchServiceClient.GetAnswers(question.idQuestion);

            var list = match.players;
            labelPlayerOne.Content = list[0].userName;
            labelPlayerTwo.Content = list[1].userName;

            lblQuestion.Content = question.question;
            lblStatusRoundOff.Content = Properties.Resources.textStatusRoundOn;
        }

        public void SetValues()
        {






        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            int resultado = (int)MessageBox.Show("¿Estás seguro(a) de salir de la ronda?", "¡Regresar a modo de juego?", MessageBoxButton.YesNo);
            if (resultado == 6)
            {
                NavigationService.Navigate(new Uri("Views/InicioView.xaml", UriKind.Relative));
            }
        }

        public void UpdateLobby(PlayerServer[] plyers)
        {
            throw new NotImplementedException();
        }

        public void LoadMatch(MatchServer match)
        {
            throw new NotImplementedException();
        }

        public void LoadBroad(ManagerService match)
        {
            throw new NotImplementedException();
        }

        public void ExitMatch()
        {
            throw new NotImplementedException();
        }
    }
}
