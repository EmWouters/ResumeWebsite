using FrontEndMusicPlayer;
using WMPLib;


namespace BackEndControls
{
    public class Controls
    {
        private FrontEnd drawElement;
        public WindowsMediaPlayer mediaPlayer; //This is OK (Michiel)
        public Controls()
        {
            drawElement = new FrontEnd();
            mediaPlayer = new WindowsMediaPlayer();
        }

        public int CurrentVolume { get; set; } = 50;
        public bool PlayerIsOn { get; set; } = true;
        public bool ShowMenu { get; set; }
        public string CurrentTrack { get; set; } = "";
        public string MusicPath { get; set; } = "";
        public string ControlButton { get; set; } = "";
        public List<string> Playlist { get; set; } = new List<string>();

        public bool CheckViablePlaylist(string inputPath)
        {
            //Checks if the input directory exists
            if (Directory.Exists(inputPath) && (Path.IsPathFullyQualified(inputPath)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int SetVolume(int currentVolume)
        {
            //Returns an audio volume
            int returnVolume = currentVolume;

            bool correctVolume = false;
            while (correctVolume == false)
            {
                Console.WriteLine("Input volume (0-100):");
                var volume = Console.ReadLine();
                if (int.TryParse(volume, out int newVolume))
                {
                    if (newVolume >= 0 && newVolume <= 100)
                    {
                        returnVolume = newVolume;
                        correctVolume = true;
                    }
                    else
                    {
                        drawElement.WriteSingleLineColor(ConsoleColor.Red, $"{volume} is not a correct volume.");
                    }
                }
                else
                {
                    drawElement.WriteSingleLineColor(ConsoleColor.Red, $"{volume} is not a correct volume.");
                }
            }
            return returnVolume;
        }

        public List<string> AddFilesToPlayList(string inputPath, string[] files)
        {
            List<string> mp3result = new List<string>();

            //Populates the playlist with mp3 files, if they exist in the current directory
            string[] mp3Files = files.Where(f => f.EndsWith(".mp3")).ToArray();

            for (int i = 0; i < mp3Files.Length; i++)
            {
                int pathNameLength = inputPath.Length;
                string fileName = mp3Files[i].Substring(pathNameLength);
                mp3result.Add(fileName);
            }
            return mp3result;
        }

        public void WritePause(bool setToPaused)
        {
            //Displays pause state
            if (!setToPaused)
            {
                drawElement.WriteSingleLineColor(ConsoleColor.Red, "MP3 PLAYBACK IS PAUSED");
            }
            else
            {
                drawElement.WriteSingleLineColor(ConsoleColor.Green, "MP3 PLAYBACK IS UNPAUSED");
            }
        }

        public void SetPause(bool setToPaused)
        {
            //Set the mediaplayer to pause/unpause depending on the pause state boolean
            if (!setToPaused)
            {
                mediaPlayer.controls.pause();
            }
            else
            {
                mediaPlayer.controls.play();
            }
        }

        public void WriteMute()
        {
            //try
            //{
                //Displays mute state
                if (mediaPlayer.settings.mute == true)
                {
                    drawElement.WriteSingleLineColor(ConsoleColor.Red, "MP3 PLAYBACK IS MUTED");
                }
                else
                {
                    drawElement.WriteSingleLineColor(ConsoleColor.Green, "MP3 PLAYBACK IS UNMUTED");
                }
            //}
            //// NEVER EVER DO THIS! 
            //catch (Exception ex)
            //{
            //   
            //}
            //finally
            //{
            //    drawElement.WriteSingleLineColor(ConsoleColor.Red, "ERROR OCCURRED! IGNORING FOR NOW.");
            //}
        }

        public void SetMute()
        {
            //Set the mediaplayer to mute/unmute depending on the mute state boolean
            if (mediaPlayer.settings.mute)
            {
                mediaPlayer.settings.mute = false;
            }
            else
            {
                mediaPlayer.settings.mute = true;
            }
        }

        public void ResetCurrentTrackDisplay()
        {
            //Resets the current track string to "no track"
            CurrentTrack = "";
        }

        public void SongStop(WindowsMediaPlayer player)
        {
            //Stops the current song
            player.controls.stop();
            ResetCurrentTrackDisplay();
        }

        public void ProgramQuit(WindowsMediaPlayer player)
        {
            //Ends the mediaplayer loop (has to set multiple things though)
            player.controls.stop(); 
            PlayerIsOn = false; 
            ShowMenu = false; 
        }

        public void SongPathChange(WindowsMediaPlayer player)
        {
            //Allow the path to be input again (Has to reset multiple things though)
            ShowMenu = false;               //Media player menu stops, goes back to path input menu
            ResetCurrentTrackDisplay();     //Track display goes back to ""
            player.controls.stop();         //Stop currently playing song.
            player.currentPlaylist.clear(); //The INTERNAL playlist of WindowsMediaPlayer player must be cleared!
            Playlist.Clear();               //And the stored LIST must be cleared as well!
        }

        public void MultiMethodDrawPlayer(bool isPaused)
        {
            //A collection of methods used above
            drawElement.ShowASCII();
            drawElement.DrawPlaylist(Playlist.ToArray());
            drawElement.DrawPlayerControls();
            WriteMute();
            WritePause(isPaused);
            drawElement.DrawCurrentTrack(CurrentTrack);
            drawElement.WriteSingleLineColor(ConsoleColor.DarkYellow, $"VOLUME: {CurrentVolume}");
            Console.WriteLine("Please input control button:");
        }


    }
}