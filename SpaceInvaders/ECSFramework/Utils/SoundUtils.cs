using System;
using System.IO;
using System.Windows.Media;

namespace SpaceInvaders
{
    /// <summary>Static class for play sound from all the architecture</summary>
    public static class SoundUtils
    {
        #region Fields

        private static MediaPlayer mediaPlayer = new MediaPlayer();
        private static MediaPlayer ambianceMediaPlayer = new MediaPlayer();
        private static string ambianceUrl;

        #endregion Fields

        #region PublicStaticMethods

        /// <summary>Play a sound from a url</summary>
        /// <param name="url">The URL.</param>
        public static void PlaySound(string url)
        {
            mediaPlayer.Open(new Uri(url));
            mediaPlayer.Play();
        }

        /// <summary>Plays the sound from ressources.</summary>
        /// <param name="url">The URL.</param>
        public static void PlaySoundFromRessources(string url)
        {
            string di = Environment.CurrentDirectory;
            PlaySound(Path.Combine(di, url));
        }

        /// <summary>Play the ambiance music.</summary>
        public static void AmbianceMusic()
        {
            string di = Environment.CurrentDirectory;
            ambianceUrl = Path.Combine(di, Properties.Settings.Default.AmbianceSound);
            ambianceMediaPlayer.Open(new Uri(ambianceUrl));
            ambianceMediaPlayer.MediaEnded += Media_Ended;// For replay the music when she ended
            ambianceMediaPlayer.Play();
        }

        #endregion PublicStaticMethods

        #region EventMethods

        /// <summary>Handles the Ended event of the Media player and replay the music.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private static void Media_Ended(object sender, EventArgs e)
        {
            ambianceMediaPlayer.Open(new Uri(ambianceUrl));
            ambianceMediaPlayer.Play();
        }

        #endregion EventMethods
    }
}