using ClienteJuego.ConnectService;
using ClienteJuego.Views;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ClienteJuego
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ConnectService.IGameServiceCallback, ConnectService.IMatchServiceCallback
    {
        string userName = (App.Current as App).DeptName;
        private readonly ConnectService.GameServiceClient gameServiceClient;
        private readonly ConnectService.MatchServiceClient matchServiceClient;
        public MainWindow()
        {
            InitializeComponent();
            Contenedor.NavigationService.Navigate(new LoginView());
            gameServiceClient = new GameServiceClient(new InstanceContext(this));
            matchServiceClient = new MatchServiceClient(new InstanceContext(this));
        }

        public void EndTurn(string username)
        {
            throw new System.NotImplementedException();
        }

        public void ExitMatch(MatchServer match)
        {
            throw new System.NotImplementedException();
        }

        public void SetRound(QuestionServer question, AnswerServer[] answers, MatchServer match)
        {
            throw new System.NotImplementedException();
        }

        public void SetStrikes(int stikesOne, int strikesTwo)
        {
            throw new System.NotImplementedException();
        }

        public void SetTurn(string username)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateMatch(MatchServer match, AnswerServer answerServer)
        {
            throw new System.NotImplementedException();
        }

        private void mainWindows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
            var match = (App.Current as App).MatchDepp;
            var code = (App.Current as App).codeDepp;
            client.UserDisconect(userName);
            if (match != null)
            {
                EndWindowsMatch(match);
            }
            else if (code != null)
            {
                EndWindowsLobby(code);
            }

        }

        void EndWindowsMatch(MatchServer match)
        {

            var list = match.players;


            if (match != null)
            {

                MatchServer newMatch = new MatchServer();
                newMatch.scorePlayerOne = match.scorePlayerOne;
                newMatch.scorePlayerTwo = match.scorePlayerTwo;
                newMatch.inviteCode = match.inviteCode;

                foreach (var player in list)
                {
                    if (!player.userName.Equals(userName))
                    {
                        newMatch.playerWinner = player.idPlayer;

                        gameServiceClient.EndMatch(newMatch);

                    }
                }
            }
        }

        void EndWindowsLobby(string code)
        {
            matchServiceClient.DisconnectFromLobby(userName, code);
        }

        public void UpdateLobby(PlayerServer[] plyers)
        {
            throw new System.NotImplementedException();
        }

        public void LoadMatch(MatchServer match)
        {
            throw new System.NotImplementedException();
        }

        public void Kicked()
        {
            throw new System.NotImplementedException();
        }
    }
}
