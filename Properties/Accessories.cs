using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ClienteJuego.Properties
{
    internal static class Accessories
    {

        public static readonly SoundPlayer SOUNDSEFFECTS = new SoundPlayer("/users/eduar/source/repos/clientejuego/effectsounds/buttonclic.wav");
        public static readonly SoundPlayer ERROREFFECT = new SoundPlayer("/users/eduar/source/repos/clientejuego/effectsounds/Error.wav");
        public static readonly SoundPlayer WINNEREFFECT = new SoundPlayer("/users/eduar/source/repos/clientejuego/effectsounds/Ganador.wav");
        public static readonly SoundPlayer ANSWEREFFECT = new SoundPlayer("/users/eduar/source/repos/clientejuego/effectsounds/Correcto.wav");

        public static readonly SoundPlayer STARTMUSIC = new SoundPlayer("/users/eduar/source/repos/clientejuego/effectsounds/Inicio.wav");
        public static readonly SoundPlayer ORIGINALMUSIC = new SoundPlayer("/users/eduar/source/repos/clientejuego/effectsounds/musicOriginal.wav");

        public static string Hash(string password)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hashedPassword = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (byte theByte in crypto)
            {
                hashedPassword.Append(theByte.ToString("x2"));
            }
            return hashedPassword.ToString();
        }

        public static void RegexSpecial(KeyEventArgs e)
        {
            bool resultado = Regex.IsMatch(e.Key.ToString(), @"^[^ ][a-zA-Z ]+[^ ]$");


            if (resultado)
            {
                e.Handled = true;

            }
        }

        public static void SaveProfileAvatar(string userName, string profilePicture)
        {
            string fileName = userName + "Config.txt";
            try
            {

                StreamWriter streamWriter = File.CreateText(fileName);
                streamWriter.WriteLine(userName);
                streamWriter.WriteLine("/Users/Eduar/source/repos/ClienteJuego" + profilePicture);
                streamWriter.Close();

            }
            catch (IOException e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }

        public static string LoadConfigPlayer(string userName)
        {
            var line = "";
            try
            {
                string fileName = userName + "Config.txt";
                var lines = File.ReadAllLines(fileName, Encoding.Default).ToList();
                line = lines.FirstOrDefault(p => p.StartsWith("/"));
            }
            catch (IOException)
            {
                return "/Users/Eduar/source/repos/ClienteJuego/Avatars/avatarDef.png";

            }

            return line;
        }

        public static void PlaySoundsEffects()
        {
            SOUNDSEFFECTS.Stop();
            if (ConfigurationManager.AppSettings["SOUND_EFFECT"].Equals("true"))
            {
                SOUNDSEFFECTS.Play();

            }


        }

        public static void PlayMusic()
        {
            ORIGINALMUSIC.Stop();
            if (ConfigurationManager.AppSettings["MUSIC_EFFECT"].Equals("true"))
            {
                ORIGINALMUSIC.Play();

            }


        }

        public static int GenerateRandomCode()
        {
            var random = new Random();
            return random.Next(100, 999);
        }
    }
}
