using ClienteJuego.ConnectService;
using System.Windows;

namespace ClienteJuego
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public string nameDeep { get; set; }
        public MatchServer matchDeep { get; set; }
        public string codeDeep { get; set; }


    }
}
