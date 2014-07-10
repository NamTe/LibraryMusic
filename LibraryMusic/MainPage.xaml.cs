﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LibraryMusic.Resources;
using Microsoft.Xna.Framework.Media;
using System.Windows.Threading;

namespace LibraryMusic
{
    public class Songs
    {
        public string Name { get; set; }
    }

    public class ListSong
    {
        private List<Songs> SongList = new List<Songs>();
        public ListSong(Songs newSong)
        {
            SongList.Add(newSong);
        }

        public ListSong()
        {
        }

        public void add(Songs newSong)
        {
            SongList.Add(newSong);
        }

        public List<Songs> getList() { return SongList; }

    }

    public partial class MainPage : PhoneApplicationPage
    {
        MediaLibrary library;
        ListSong newListSong;
        DispatcherTimer timer;
        int i = 0;
        public MainPage()
        {
            InitializeComponent();

            library = new MediaLibrary();
            newListSong = new ListSong();
            foreach (Song song in library.Songs)
            {
                newListSong.add(new Songs { Name = song.Name });
            }

            MainLongListSelector.ItemsSource = newListSong.getList();
            MainLongListSelector.Visibility = Visibility.Collapsed;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            try
            {

                i++;
                if (i == 5)
                {
                    
                    Image.Visibility = Visibility.Collapsed;
                    TextTitle.Text = "Media Player";
                    TextListSong.Text = "ListSong";
                    MainLongListSelector.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Player.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as Songs).Name, UriKind.RelativeOrAbsolute));
        }

    }
}