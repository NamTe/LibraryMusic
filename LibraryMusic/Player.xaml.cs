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

namespace LibraryMusic {

    public class XNAFrameworkDispatcherService : IApplicationService {
        private DispatcherTimer frameworkDispatcherTimer;
        public XNAFrameworkDispatcherService() {
            this.frameworkDispatcherTimer = new DispatcherTimer();
            this.frameworkDispatcherTimer.Interval = TimeSpan.FromTicks(333333);
            this.frameworkDispatcherTimer.Tick += frameworkDispatcherTimer_Tick;
            FrameworkDispatcher.Update();
        }
        void frameworkDispatcherTimer_Tick(object sender, EventArgs e) { FrameworkDispatcher.Update(); }
        void IApplicationService.StartService(ApplicationServiceContext context) { this.frameworkDispatcherTimer.Start(); }
        void IApplicationService.StopService() { this.frameworkDispatcherTimer.Stop(); }
    }


    public partial class Player : PhoneApplicationPage {
        Song _playingSong = null;
        int index;
        DispatcherTimer timer;
        public Player() {
            InitializeComponent();
            index = 0;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(timer_Tick);
            MediaElement p = new MediaElement();
            timer.Start();
        }




        #region EventHandlers


        void timer_Tick(object sender, EventArgs e) {
            try {
                slider.Maximum = _playingSong.Duration.TotalMilliseconds;
                slider.Value = MediaPlayer.PlayPosition.TotalMilliseconds;
                TimeSpan _duration = MediaPlayer.PlayPosition.Duration();
                Durration.Text = string.Format("{0:00}:{1:00}", _duration.Minutes, _duration.Seconds);
                TimeSpan _fixed = _playingSong.Duration.Duration();
                Current.Text = string.Format("{0:00}:{1:00}", _fixed.Minutes, _fixed.Seconds);
            }
            catch (Exception ex) {

            }
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {
            if (NavigationContext.QueryString.ContainsKey("selectedItem")) {
                String songToPlay = NavigationContext.QueryString["selectedItem"];

                foreach (Song song in Variable.library.Songs) {
                    if (0 == String.Compare(songToPlay, song.Name)) {
                        _playingSong = song;
                        index++;
                        break;
                    }
                    index++;
                }
                PlaySong();

            }

            base.OnNavigatedTo(e);

        }



        private void btPlay_Click(object sender, RoutedEventArgs e) {

            if (MediaPlayer.State == MediaState.Stopped) {
                PlaySong();

            }
            else if (MediaPlayer.State == MediaState.Playing) {
                PausePlayingSong();

            }
            else {
                ResumingSong();

            }
        }


        private void btPrevious_click(object sender, RoutedEventArgs e) {
            MediaPlayer.Stop();
            if (index == 0) index = Variable.library.Songs.Count - 1;
            else index--;
            _playingSong = Variable.library.Songs[index];
            PlaySong();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e) {
            nextSong();
        }


        #endregion EventHandlers



        #region MediaPlayer
        public void nextSong() {
            index++;
            MediaPlayer.Stop();
            if (index == Variable.library.Songs.Count) index = 0;
            _playingSong = Variable.library.Songs[index];
            PlaySong();
        }

        private void PlaySong() {
            try { StopPlayingSong(); }
            catch (Exception e) { }

            if (_playingSong != null) {

                try {
                    
                    MediaPlayer.Play(_playingSong);
                    title.Text = _playingSong.Name;
                }
                catch (Exception exc) {
                    title.Text = "@@!";
                }
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/pause.png", UriKind.Relative)).Stream);
                btPlay.Source = tn;
                PopulateSongMetadata();
            }

        }

        private void StopPlayingSong() {
            if (MediaPlayer.State == MediaState.Playing || _playingSong != null) {
                MediaPlayer.Stop();
            }
        }

        private void PausePlayingSong() {
            if (MediaPlayer.State == MediaState.Playing) {
                MediaPlayer.Pause();
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/play.png", UriKind.Relative)).Stream);
                btPlay.Source = tn;
            }
        }

        private void ResumingSong() {
            if (MediaState.Paused == MediaPlayer.State) {
                MediaPlayer.Resume();
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/pause.png", UriKind.Relative)).Stream);
                btPlay.Source = tn;
            }
        }


        private void PopulateSongMetadata() {
            if (_playingSong != null) {
                if (_playingSong.Album.HasArt == true) {
                    Stream albumArtStream = _playingSong.Album.GetAlbumArt();
                    BitmapImage albumArtImage = new BitmapImage();
                    albumArtImage.SetSource(albumArtStream);
                    imAlbum.Source = albumArtImage;

                }
                else {
                    BitmapImage tn = new BitmapImage();
                    tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/abum.png", UriKind.Relative)).Stream);
                    imAlbum.Source = tn;
                }
            }
        }

        #endregion MediaPlayer

        private void btRepeat_Click(object sender, RoutedEventArgs e) {
            if (MediaPlayer.IsRepeating == false)
                MediaPlayer.IsRepeating = true;
            else MediaPlayer.IsRepeating = false;
        }




    }


}