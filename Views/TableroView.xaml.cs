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
    public partial class TableroView : Page, ConnectService.IGameServiceCallback
    {
        public MatchServer match { get; set; }
        private readonly ConnectService.GameServiceClient gameServiceClient;
        QuestionServer questionRaund;
        AnswerServer[] answersRaund;
        string username;
        string turn;
        int strike;
        public TableroView(MatchServer match)
        {

            InitializeComponent();
            gameServiceClient = new GameServiceClient(new InstanceContext(this));
            username = (App.Current as App).DeptName;
            this.match = match;

            var list = match.players;
            labelPlayerOne.Content = list[0].userName;
            labelPlayerTwo.Content = list[1].userName;

            gameServiceClient.SetCallbackGame(username);

            textAnswer.IsEnabled = false;
            btnAnswer.IsEnabled = false;
            SolidColorBrush brush = new SolidColorBrush(Colors.Red);
            fgrPlayerOne.Fill = brush;
            fgrPlayerTwo.Fill = brush;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            gameServiceClient.StartRaund(username, match.inviteCode);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            int resultado = (int)MessageBox.Show("¿Estás seguro(a) de salir de la ronda?", "¡Regresar a modo de juego?", MessageBoxButton.YesNo);
            if (resultado == 6)
            {
                NavigationService.Navigate(new Uri("Views/InicioView.xaml", UriKind.Relative));
            }
        }

        public void ExitMatch()
        {
            throw new NotImplementedException();
        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            var roomchat = (MainWindow)App.Current.MainWindow;
            roomchat.ContenedorChat.Navigate(new ChatView());
        }

        public void SetRound(QuestionServer question, AnswerServer[] answers)
        {
            questionRaund = question;
            answersRaund = answers;

            lblQuestion.Content = question.question;
            Console.WriteLine(answers[1].answer);
            btnPlay.Visibility = Visibility.Hidden;
            btnPlay.IsEnabled = false;
            var list = match.players;
            turn = list[0].userName;
            SolidColorBrush brush = new SolidColorBrush(Colors.Green);
            fgrPlayerOne.Fill = brush;
            gameServiceClient.YouTurn(turn, match.inviteCode);


        }

        public void UpdateMatch(MatchServer match)
        {
            throw new NotImplementedException();
        }

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {

        }

        public void SetTurn()
        {
            textAnswer.IsEnabled = true;
            btnAnswer.IsEnabled = true;
        }
    }
}
