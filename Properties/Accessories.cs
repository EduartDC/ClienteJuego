using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;

namespace ClienteJuego.Properties
{
    internal class Accessories
    {
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
                return;
            }
        }

        public static void SaveProfileAvatar(string userName, string profilePicture)
        {
            string fileName = userName + "Config.txt";
            try
            {
                if (File.Exists(fileName))
                {
                    StreamWriter streamWriter = File.CreateText(fileName);
                    streamWriter.WriteLine(userName);
                    streamWriter.WriteLine("/Users/Eduar/source/repos/ClienteJuego"+profilePicture);
                    streamWriter.Close();
                }
                else
                {
                    StreamWriter streamWriter = File.CreateText(fileName);
                    streamWriter.WriteLine(userName);
                    streamWriter.WriteLine(profilePicture);
                    streamWriter.Close();
                }
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
            catch (IOException e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            
            return line;
        }
    }
}
