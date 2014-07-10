using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Windows.Threading;
using Microsoft.Xna.Framework;

namespace LibraryMusic
{
    public partial class Player : PhoneApplicationPage
    {
        Song _playingSong = null;
        int index;
        DispatcherTimer timer;
        public Player()
        {
            InitializeComponent();
            index = 0;
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(33);
            dt.Tick += delegate { try { FrameworkDispatcher.Update(); } catch { } };
            
            dt.Start();
            StopPlayingSong();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            Loaded += PhoneApplicationPage_Loaded;
        }




        #region EventHandlers


        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                slider.Maximum = _playingSong.Duration.TotalMilliseconds;
                slider.Value = MediaPlayer.PlayPosition.TotalMilliseconds;
                TimeSpan _duration = MediaPlayer.PlayPosition.Duration();
                Durration.Text = string.Format("{0:00}:{1:00}", _duration.Minutes, _duration.Seconds);
                TimeSpan _fixed = _playingSong.Duration.Duration();
                Current.Text = string.Format("{0:00}:{1:00}", _fixed.Minutes, _fixed.Seconds);
            }
            catch (Exception ex)
            {

            }
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            MediaLibrary library = new MediaLibrary();

            if (NavigationContext.QueryString.ContainsKey("selectedItem"))
            {
                String songToPlay = NavigationContext.QueryString["selectedItem"];

                foreach (Song song in library.Songs)
                {
                    if (0 == String.Compare(songToPlay, song.Name))
                    {
                        _playingSong = song;
                        index++;
                        break;
                    }
                    index++;
                }


            }
            base.OnNavigatedTo(e);

        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

            if (_playingSong != null)
            {
                PlaySong();
            }

        }


        private void btPlay_Click(object sender, RoutedEventArgs e)
        {

            if (MediaPlayer.State == MediaState.Stopped)
            {
                PlaySong();
                
            }
            else if (MediaPlayer.State == MediaState.Playing)
            {
                PausePlayingSong();
                
            }
            else
            {
                ResumingSong();
                
            }
        }


        private void btPrevious_click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
            MediaLibrary library = new MediaLibrary();
            if (index == 0) index = library.Songs.Count - 1;
            else index--;
            _playingSong = library.Songs[index];
            PlaySong();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            nextSong();
        }


        #endregion EventHandlers



        #region MediaPlayer
        public void nextSong()
        {
            index++;
            MediaPlayer.Stop();
            MediaLibrary library = new MediaLibrary();
            if (index == library.Songs.Count) index = 0;
            _playingSong = library.Songs[index];
            PlaySong();
        }

        private void PlaySong()
        {
            if (_playingSong != null)
            {
                MediaPlayer.Play(_playingSong);
                try
                {
                    title.Text = _playingSong.Name;
                }
                catch (Exception exc)
                {
                    title.Text = "@@!";
                }
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/pause.png", UriKind.Relative)).Stream);
                btPlay.Source = tn;
                PopulateSongMetadata();
            }

        }

        private void StopPlayingSong()
        {
            if (MediaPlayer.State == MediaState.Playing || _playingSong != null)
            {
                MediaPlayer.Stop();
            }
        }

        private void PausePlayingSong()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/play.png", UriKind.Relative)).Stream);
                btPlay.Source = tn;
            }
        }

        private void ResumingSong()
        {
            if (MediaState.Paused == MediaPlayer.State)
            {
                MediaPlayer.Resume();
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/pause.png", UriKind.Relative)).Stream);
                btPlay.Source = tn;
            }
        }


        private void PopulateSongMetadata()
        {
            if (_playingSong != null)
            {
                if (_playingSong.Album.HasArt == true)
                {
                    Stream albumArtStream = _playingSong.Album.GetAlbumArt();
                    BitmapImage albumArtImage = new BitmapImage();
                    albumArtImage.SetSource(albumArtStream);
                    imAlbum.Source = albumArtImage;

                }
                else
                {
                    BitmapImage tn = new BitmapImage();
                    tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/abum.png", UriKind.Relative)).Stream);
                    imAlbum.Source = tn;
                }
            }
        }

        #endregion MediaPlayer

        private void btRepeat_Click(object sender, RoutedEventArgs e)
        {
            if (MediaPlayer.IsRepeating == false)
                MediaPlayer.IsRepeating = true;
            else MediaPlayer.IsRepeating = false;
        }




    }


}