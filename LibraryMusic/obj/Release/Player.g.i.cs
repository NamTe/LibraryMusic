﻿#pragma checksum "C:\Users\TinTac_Tk3\documents\visual studio 2013\LibraryMusic\LibraryMusic\Player.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9351DA29DFDCEE88E949A50233C920E3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace LibraryMusic {
    
    
    public partial class Player : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock title;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Image imAlbum;
        
        internal System.Windows.Controls.Image btPrevious;
        
        internal System.Windows.Controls.Image btNext;
        
        internal System.Windows.Controls.Image btRepeat;
        
        internal System.Windows.Controls.Image btPlay;
        
        internal System.Windows.Controls.Slider slider;
        
        internal System.Windows.Controls.TextBlock Current;
        
        internal System.Windows.Controls.TextBlock Durration;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/LibraryMusic;component/Player.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.title = ((System.Windows.Controls.TextBlock)(this.FindName("title")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.imAlbum = ((System.Windows.Controls.Image)(this.FindName("imAlbum")));
            this.btPrevious = ((System.Windows.Controls.Image)(this.FindName("btPrevious")));
            this.btNext = ((System.Windows.Controls.Image)(this.FindName("btNext")));
            this.btRepeat = ((System.Windows.Controls.Image)(this.FindName("btRepeat")));
            this.btPlay = ((System.Windows.Controls.Image)(this.FindName("btPlay")));
            this.slider = ((System.Windows.Controls.Slider)(this.FindName("slider")));
            this.Current = ((System.Windows.Controls.TextBlock)(this.FindName("Current")));
            this.Durration = ((System.Windows.Controls.TextBlock)(this.FindName("Durration")));
        }
    }
}
