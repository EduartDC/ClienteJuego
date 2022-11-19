﻿using System;
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
using System.Media;
using System.Configuration;

namespace ClienteJuego.Properties
{
    internal class Accessories
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
                    streamWriter.WriteLine("/Users/Eduar/source/repos/ClienteJuego" + profilePicture);
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
    }
}
