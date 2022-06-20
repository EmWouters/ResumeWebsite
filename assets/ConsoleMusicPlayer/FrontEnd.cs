namespace FrontEndMusicPlayer
{
    internal class FrontEnd
    {
        public void ShowASCII()
        {
            //Draws title
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            string mediaArt = @"
███╗░░░███╗███████╗██████╗░██╗░█████╗░  ██████╗░██╗░░░░░░█████╗░██╗░░░██╗███████╗██████╗░
████╗░████║██╔════╝██╔══██╗██║██╔══██╗  ██╔══██╗██║░░░░░██╔══██╗╚██╗░██╔╝██╔════╝██╔══██╗
██╔████╔██║█████╗░░██║░░██║██║███████║  ██████╔╝██║░░░░░███████║░╚████╔╝░█████╗░░██████╔╝
██║╚██╔╝██║██╔══╝░░██║░░██║██║██╔══██║  ██╔═══╝░██║░░░░░██╔══██║░░╚██╔╝░░██╔══╝░░██╔══██╗
██║░╚═╝░██║███████╗██████╔╝██║██║░░██║  ██║░░░░░███████╗██║░░██║░░░██║░░░███████╗██║░░██║
╚═╝░░░░░╚═╝╚══════╝╚═════╝░╚═╝╚═╝░░╚═╝  ╚═╝░░░░░╚══════╝╚═╝░░╚═╝░░░╚═╝░░░╚══════╝╚═╝░░╚═╝";
            Console.Write(mediaArt);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*****************************************************************************************");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteSingleLineColor(ConsoleColor color, string text)
        {
            //Draws single line of text in different color
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void DrawMenuLine() 
        {
            //Draws one of the menu lines
            Console.WriteLine("|---------------- - - - - -  -  -   -   -    -     -");
        }

        public void DrawPlaylist(string[] determinedPlaylist)
        {
            //Draws mp3 playlist
            DrawMenuLine();
            Console.WriteLine("|\tENTRY\tMP3 FILE NAME");
            DrawMenuLine();
            for (int i = 0; i < determinedPlaylist.Length; i++)
            {
                Console.WriteLine($"|\t{i}\t{determinedPlaylist[i]}");
            }
            DrawMenuLine();
        }

        public void DrawPlayerControls()
        {
            //Draws media player controls
            Console.WriteLine();
            Console.WriteLine("MEDIA PLAYER CONTROLS:");
            Console.WriteLine("[0 - ...]: \tPlay new/restart mp3");
            Console.WriteLine("P \t\tPause/unpause");
            Console.WriteLine("V \t\tChange volume");
            Console.WriteLine("M \t\tMute/unmute volume");
            Console.WriteLine("S \t\tStop playing current mp3");
            Console.WriteLine("C \t\tChange folder path");
            Console.WriteLine("Q \t\tEnd program");
            Console.WriteLine();
        }

        public void DrawCurrentTrack(string whatSong)
        {
            //Draws currently playing song
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Now playing: {whatSong}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}