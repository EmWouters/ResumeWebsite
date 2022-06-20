using BackEndControls;
using FrontEndMusicPlayer;

//Test path: c:\users\emlyn\music\

//Class instances:
FrontEnd frontEndElement = new FrontEnd();
Controls control = new Controls();

int pauseClicked = 0;

//OUTER LOOP: MUSIC FOLDER INPUT
//******************************
while (control.PlayerIsOn == true)
{
    //Initial/current volume:
    control.mediaPlayer.settings.volume = control.CurrentVolume;

    //Show media player title:
    frontEndElement.ShowASCII();

    //Reset menu appearance:
    control.ShowMenu = false;
    control.ResetCurrentTrackDisplay();

    //Path user input:
    Console.WriteLine("Input music map path (example: c:\\users\\...\\music\\):");
    control.MusicPath = Console.ReadLine();
    if (!control.MusicPath.EndsWith("\\"))
    {
        control.MusicPath = "";

        #region Explanation

        /*
        The playlist is only properly constructed by inputting a correct path FORMAT:
        c:\users\user\music     is wrong
        c:\users\user\music\    is right
        This is because the constructed string needs to be correct:
            c:\users\user\musicsong.mp3
            is not the same as
            c:\users\user\music\song.mp3
            (notice the \ backslash)
        */

        #endregion Explanation
    }

    //Check for viable path:
    if (control.CheckViablePlaylist(control.MusicPath) == true)
    {
        control.Playlist.Clear();
        control.Playlist = control.AddFilesToPlayList(control.MusicPath, Directory.GetFiles(control.MusicPath));
        control.ShowMenu = true;
    }
    else
    {
        control.ShowMenu = false;
        control.Playlist.Clear();
        frontEndElement.WriteSingleLineColor(ConsoleColor.Red, "Path not found! Press any key...");
        Console.ReadKey();
    }

    //INNER LOOP: PLAYLIST CONTROLS
    //*****************************

    //Media player and playlist are now visible.
    while (control.ShowMenu == true)
    {
        //Draw user interface elements:
        control.MultiMethodDrawPlayer(IsPaused());

        //Control button input:
        control.ControlButton = Console.ReadLine();

        HandlePlaylistChoice(control);
        HandleMenuChoice(control);

        Console.Clear();
    }
}

ShowCredits(control);

void HandleMenuChoice(Controls control)
{
    switch (control.ControlButton)
    {
        case ("P"):
            pauseClicked++;
            control.SetPause(IsPaused());
            break;

        case ("M"):
            control.SetMute();
            break;

        case ("V"):
            control.CurrentVolume = control.SetVolume(control.mediaPlayer.settings.volume);
            control.mediaPlayer.settings.volume = control.CurrentVolume;
            break;

        case ("S"):
            control.SongStop(control.mediaPlayer);
            break;

        case ("C"):
            control.SongPathChange(control.mediaPlayer);
            break;

        case ("Q"):
            control.ProgramQuit(control.mediaPlayer);
            break;
    }
}

void HandlePlaylistChoice(Controls control)
{
    for (int i = 0; i < control.Playlist.Count; i++)
    {
        if (control.ControlButton == Convert.ToString(i))
        {
            control.CurrentTrack = control.Playlist[i];
            control.mediaPlayer.controls.stop(); //Stop previous song

            //Start newly selected song:
            control.mediaPlayer.URL = Path.Combine(control.MusicPath, control.Playlist[i]);

            if (pauseClicked % 2 == 1)
            {
                //If the pause button is still on, stop the newly selected song
                control.mediaPlayer.controls.stop();
            }

            //This is where potential metadata can be stored in a string or string array
            //OPTIONAL
        }
    }
}

void ShowCredits(Controls control)
{
    Console.Clear();
    Console.WriteLine("Program ended");
    control.mediaPlayer.controls.stop();
}

bool IsPaused()
{
    return pauseClicked % 2 == 0;
}