using ClienteJuego.ConnectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;

namespace ClienteJuego.Views
{
    /// <summary>
    /// Interaction logic for TableroView.xaml
    /// </summary>
    public partial class TableroView : Page, ConnectService.IGameServiceCallback
    {
        public MatchServer match { get; set; }
        private readonly ConnectService.GameServiceClient gameServiceClient;
        List<AnswerServer> answersRaund;
        string username;
        string turn;
        int strikePlayerOne;
        int strikePlayerTwo;
        int strikeLimit = 3;
        AnswerServer correctAnswer = new AnswerServer();

        public TableroView(MatchServer match)
        {

            InitializeComponent();
            gameServiceClient = new GameServiceClient(new InstanceContext(this));
            username = (App.Current as App).DeptName;
            this.match = match;

            var list = match.players;
            labelPlayerOne.Content = list[0].userName;
            labelPlayerTwo.Content = list[1].userName;
            textAnswer.Text = null;
            turn = list[0].userName;
            try
            {
                gameServiceClient.SetCallbackGame(username);
            }
            catch (CommunicationObjectFaultedException)
            {
                throw new EndpointNotFoundException();

            }
            catch (EndpointNotFoundException)
            {
                throw new EndpointNotFoundException();

            }


            textAnswer.IsEnabled = false;
            btnAnswer.IsEnabled = false;
            SolidColorBrush brush = new SolidColorBrush(Colors.Gray);
            fgrPlayerOne.Fill = brush;
            fgrPlayerTwo.Fill = brush;

            playerOneStrikeOne.Visibility = Visibility.Hidden;
            playerOneStrikeTwo.Visibility = Visibility.Hidden;
            playerOneStrikethree.Visibility = Visibility.Hidden;
            playerTwoStrikeOne.Visibility = Visibility.Hidden;
            playerTwoStrikeTwo.Visibility = Visibility.Hidden;
            playerTwoStrikethree.Visibility = Visibility.Hidden;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            MatchServer newMatch = new MatchServer();
            newMatch.scorePlayerOne = match.scorePlayerOne;
            newMatch.scorePlayerTwo = match.scorePlayerTwo;
            newMatch.inviteCode = match.inviteCode;
            try
            {
                gameServiceClient.StartRaund(newMatch);
                gameServiceClient.YouTurn(turn, match.inviteCode);
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            int resultYes = 6;
            int resultado = (int)MessageBox.Show(Properties.Resources.messageBoxExitGame, Properties.Resources.messageBoxCare, MessageBoxButton.YesNo);
            if (resultado == resultYes)
            {

                var list = match.players;
                MatchServer newMatch = new MatchServer();
                newMatch.scorePlayerOne = match.scorePlayerOne;
                newMatch.scorePlayerTwo = match.scorePlayerTwo;
                newMatch.inviteCode = match.inviteCode;

                foreach (var player in list)
                {
                    if (!player.userName.Equals(username))
                    {
                        newMatch.playerWinner = player.idPlayer;
                        try
                        {
                            gameServiceClient.EndMatch(newMatch);
                        }
                        catch (CommunicationObjectFaultedException)
                        {
                            MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                            var window = (MainWindow)Application.Current.MainWindow;
                            window.Contenedor.Navigate(new LoginView());
                        }

                    }
                }
            }
        }

        public void ExitMatch(MatchServer match)
        {
            var list = match.players;
            foreach (var player in list)
            {
                if (player.idPlayer == match.playerWinner)
                {
                    MessageBox.Show(Properties.Resources.messageBoxEndGame + " " + player.userName);
                }
            }
            try
            {
                if (username.Equals("Guest"))
                {
                    ConnectService.UserManagerClient client = new ConnectService.UserManagerClient();
                    try
                    {
                        client.UserDisconect((App.Current as App).DeptName);
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }
                    catch (EndpointNotFoundException)
                    {
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }
                }
                else
                {
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new InicioView());
                }

            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }

        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            var roomchat = (MainWindow)App.Current.MainWindow;
            roomchat.ContenedorChat.Navigate(new ChatView());
        }

        public void SetRound(QuestionServer question, AnswerServer[] answers, MatchServer match)
        {
            strikePlayerOne = 0;
            strikePlayerTwo = 0;

            btnPlay.Visibility = Visibility.Hidden;
            btnPlay.IsEnabled = false;

            answersRaund = answers.ToList();

            labelScorePointsOne.Content = match.scorePlayerOne;
            labelScorePointsTwo.Content = match.scorePlayerTwo;

            labelQuestion.Content = question.question;

            labelAnswer1.Content = ". . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .";
            labelAnswer2.Content = ". . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .";
            labelAnswer3.Content = ". . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .";
            labelAnswer4.Content = ". . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .";

            labelScoreAnswer1.Content = 0;
            labelScoreAnswer2.Content = 0;
            labelScoreAnswer3.Content = 0;
            labelScoreAnswer4.Content = 0;
        }

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {

            if (ValidateAnswer())
            {
                addPoints();

            }
            else if (strikePlayerOne == strikeLimit && strikePlayerTwo == strikeLimit)
            {
                MatchServer newMatch = new MatchServer();

                newMatch.scorePlayerOne = match.scorePlayerOne;
                newMatch.scorePlayerTwo = match.scorePlayerTwo;
                newMatch.inviteCode = match.inviteCode;
                try
                {
                    gameServiceClient.StartRaund(newMatch);
                    strikePlayerOne = 0;
                    strikePlayerTwo = 0;

                    gameServiceClient.AddStrikes(strikePlayerOne, strikePlayerTwo, match.inviteCode);
                }
                catch (CommunicationObjectFaultedException)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }


                var list = match.players;
                if (turn.Equals(list[0].userName))
                {
                    turn = list[1].userName;
                    try
                    {
                        gameServiceClient.YouTurn(turn, match.inviteCode);
                    }
                    catch (CommunicationObjectFaultedException)
                    {
                        MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }

                }
                else if (turn.Equals(list[1].userName))
                {
                    turn = list[0].userName;
                    try
                    {
                        gameServiceClient.YouTurn(turn, match.inviteCode);
                    }
                    catch (CommunicationObjectFaultedException)
                    {
                        MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }

                }
            }
            else
            {
                ChangeTurn();
            }
        }

        bool ValidateAnswer()
        {
            bool result = false;

            var playerAnswer = textAnswer.Text;
            foreach (var answers in answersRaund.Where(answer => answer.answer.ToLower().Equals(playerAnswer.ToLower())).Select(answer => new { }))
            {
                result = true;

            }

            var list = match.players;
            if (!result && turn.Equals(list[0].userName))
            {
                strikePlayerOne++;
                try
                {
                    gameServiceClient.AddStrikes(strikePlayerOne, strikePlayerTwo, match.inviteCode);
                }
                catch (CommunicationObjectFaultedException)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }

            }
            else if (!result && turn.Equals(list[1].userName))
            {
                strikePlayerTwo++;
                try
                {
                    gameServiceClient.AddStrikes(strikePlayerOne, strikePlayerTwo, match.inviteCode);
                }
                catch (CommunicationObjectFaultedException)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }

            }
            return result;
        }

        private void addPoints()
        {
            var playerAnswer = textAnswer.Text;
            foreach (var answer in answersRaund.Where(answer => answer.answer.ToLower().Equals(playerAnswer.ToLower())))
            {
                correctAnswer = answer;

            }

            var list = match.players;
            if (turn.Equals(list[0].userName))
            {
                match.scorePlayerOne += correctAnswer.score;
            }
            else if (turn.Equals(list[1].userName))
            {
                match.scorePlayerTwo += correctAnswer.score;
            }

            textAnswer.Text = null;

            MatchServer newMatch = new MatchServer();
            AnswerServer newanswer = new AnswerServer();

            newMatch.scorePlayerOne = match.scorePlayerOne;
            newMatch.scorePlayerTwo = match.scorePlayerTwo;
            newMatch.inviteCode = match.inviteCode;

            newanswer.answer = correctAnswer.answer;
            newanswer.score = correctAnswer.score;
            newanswer.place = correctAnswer.place;
            try
            {
                gameServiceClient.SetBoard(newMatch, newanswer);
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                var window = (MainWindow)Application.Current.MainWindow;
                window.Contenedor.Navigate(new LoginView());
            }

            int pointsForWinning = 400;
            if (match.scorePlayerOne >= pointsForWinning)
            {
                foreach (var player in list.Where(player => player.userName.Equals(turn)))
                {
                    try
                    {
                        newMatch.playerWinner = player.idPlayer;
                        try
                        {
                            gameServiceClient.EndMatch(newMatch);
                        }
                        catch (CommunicationObjectFaultedException)
                        {
                            MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                            var window = (MainWindow)Application.Current.MainWindow;
                            window.Contenedor.Navigate(new LoginView());
                        }

                    }
                    catch (CommunicationObjectFaultedException)
                    {
                        MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    }
                }
            }
            else if (match.scorePlayerTwo >= pointsForWinning)
            {
                foreach (var player in list.Where(player => player.userName.Equals(turn)))
                {
                    newMatch.playerWinner = player.idPlayer;
                    newMatch.idMatch = match.idMatch;
                    try
                    {
                        gameServiceClient.EndMatch(newMatch);
                    }
                    catch (CommunicationObjectFaultedException)
                    {
                        MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }
                }
            }

            if (answersRaund.Count == 1)
            {
                gameServiceClient.StartRaund(newMatch);
                strikePlayerOne = 0;
                strikePlayerTwo = 0;
                gameServiceClient.AddStrikes(strikePlayerOne, strikePlayerTwo, match.inviteCode);

                if (turn.Equals(list[0].userName))
                {
                    turn = list[1].userName;
                    try
                    {
                        gameServiceClient.YouTurn(turn, match.inviteCode);
                    }
                    catch (CommunicationObjectFaultedException)
                    {
                        MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }

                }
                else if (turn.Equals(list[1].userName))
                {
                    turn = list[0].userName;
                    try
                    {
                        gameServiceClient.YouTurn(turn, match.inviteCode);
                    }
                    catch (CommunicationObjectFaultedException)
                    {
                        MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.Contenedor.Navigate(new LoginView());
                    }

                }
            }
        }

        void ChangeTurn()
        {

            var list = match.players;
            if (turn.Equals(list[0].userName) && strikePlayerOne == strikeLimit)
            {

                turn = list[1].userName;
                try
                {
                    gameServiceClient.YouTurn(turn, match.inviteCode);
                }
                catch (CommunicationObjectFaultedException)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }

            }
            else if (turn.Equals(list[1].userName) && strikePlayerTwo == strikeLimit)
            {

                turn = list[0].userName;
                try
                {
                    gameServiceClient.YouTurn(turn, match.inviteCode);
                }
                catch (CommunicationObjectFaultedException)
                {
                    MessageBox.Show(Properties.Resources.messageBoxConnectionError);
                    var window = (MainWindow)Application.Current.MainWindow;
                    window.Contenedor.Navigate(new LoginView());
                }

            }

        }

        public void UpdateMatch(MatchServer matchServer, AnswerServer answerServer)
        {
            foreach (var answer in answersRaund)
            {
                if (answer.answer.ToLower().Equals(answerServer.answer.ToLower()))
                {
                    answersRaund.Remove(answer);
                    break;
                }
            }


            match.scorePlayerOne = matchServer.scorePlayerOne;
            match.scorePlayerTwo = matchServer.scorePlayerTwo;
            labelScorePointsOne.Content = matchServer.scorePlayerOne;
            labelScorePointsTwo.Content = matchServer.scorePlayerTwo;

            if (answerServer.place == 1)
            {
                labelAnswer1.Content = answerServer.answer;
                labelScoreAnswer1.Content = answerServer.score;
            }
            else if (answerServer.place == 2)
            {
                labelAnswer2.Content = answerServer.answer;
                labelScoreAnswer2.Content = answerServer.score;
            }
            else if (answerServer.place == 3)
            {
                labelAnswer3.Content = answerServer.answer;
                labelScoreAnswer3.Content = answerServer.score;
            }
            else if (answerServer.place == 4)
            {
                labelAnswer4.Content = answerServer.answer;
                labelScoreAnswer4.Content = answerServer.score;
            }
        }

        public void SetTurn(string username)
        {
            textAnswer.IsEnabled = true;
            btnAnswer.IsEnabled = true;
            turn = username;

            var list = match.players;

            if (list[0].userName.Equals(username))
            {
                SolidColorBrush brush = new SolidColorBrush(Colors.Green);
                fgrPlayerOne.Fill = brush;

                SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
                fgrPlayerTwo.Fill = brushRed;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush(Colors.Green);
                fgrPlayerTwo.Fill = brush;

                SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
                fgrPlayerOne.Fill = brushRed;
            }
        }

        public void EndTurn(string username)
        {
            textAnswer.IsEnabled = false;
            btnAnswer.IsEnabled = false;

            var list = match.players;
            if (list[0].userName.Equals(username))
            {
                SolidColorBrush brush = new SolidColorBrush(Colors.Red);
                fgrPlayerOne.Fill = brush;

                SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
                fgrPlayerTwo.Fill = brushGreen;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush(Colors.Red);
                fgrPlayerTwo.Fill = brush;

                SolidColorBrush brushGreen = new SolidColorBrush(Colors.Green);
                fgrPlayerOne.Fill = brushGreen;

            }
        }

        public void SetStrikes(int stikesOne, int strikesTwo)
        {
            strikePlayerOne = stikesOne;
            strikePlayerTwo = strikesTwo;

            if (strikePlayerOne == 0)
            {
                playerOneStrikeOne.Visibility = Visibility.Hidden;
                playerOneStrikeTwo.Visibility = Visibility.Hidden;
                playerOneStrikethree.Visibility = Visibility.Hidden;
            }
            else if (strikePlayerOne == 1)
            {
                playerOneStrikeOne.Visibility = Visibility.Visible;
            }
            else if (strikePlayerOne == 2)
            {
                playerOneStrikeTwo.Visibility = Visibility.Visible;
            }
            else if (strikePlayerOne == 3)
            {
                playerOneStrikethree.Visibility = Visibility.Visible;
            }

            if (strikePlayerTwo == 0)
            {
                playerTwoStrikeOne.Visibility = Visibility.Hidden;
                playerTwoStrikeTwo.Visibility = Visibility.Hidden;
                playerTwoStrikethree.Visibility = Visibility.Hidden;
            }
            else if (strikePlayerTwo == 1)
            {
                playerTwoStrikeOne.Visibility = Visibility.Visible;
            }
            else if (strikePlayerTwo == 2)
            {
                playerTwoStrikeTwo.Visibility = Visibility.Visible;
            }
            else if (strikePlayerTwo == 3)
            {
                playerTwoStrikethree.Visibility = Visibility.Visible;
            }

        }


    }
}
