using ClienteJuego.ConnectService;
using System.Windows;

namespace ClienteJuego
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public string DeptName { get; set; }
        public MatchServer MatchDepp { get; set; }
        public string codeDepp { get; set; }


    }
}
