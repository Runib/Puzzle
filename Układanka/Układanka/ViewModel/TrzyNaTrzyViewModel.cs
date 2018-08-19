using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Układanka.Services;
using System.Drawing;
using System.Windows.Interop;
using System.IO;
using System.Drawing.Imaging;
using Układanka.Models;
using System.Collections.ObjectModel;
using Układanka.Helper;
using System.Windows.Threading;
using System.ComponentModel;
using System.Media;

namespace Układanka.ViewModel
{
    public class TrzyNaTrzyViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private MediaPlayer mediaPlayer;
        private IMyNavigationService navigationService;
        public static ObservableCollection<ImageModel> Original { get; set; } = new ObservableCollection<ImageModel>();
        public static ObservableCollection<ImageModel> GameList { get; set; } = new ObservableCollection<ImageModel>();

        public static bool IsMixed = false;
        public static DateTime StartTime;
        public event PropertyChangedEventHandler PropertyChanged;


        public RelayCommand<ImageModel> MouseClicked { get; set; }
        public RelayCommand OnLoad { get; set; }


        private string currentTime;
        public string CurrentTime
        {
            get { return currentTime; }
            set {
                if (currentTime != value)
                    currentTime = value;

                OnPropertyChanged("CurrentTime");
            }
        }

        private ImageSource img;
        public ImageSource Img
        {
            get { return img; }
            set { img = value; RaisePropertyChanged(() => Img); }
        }

        private int myCounter;
        public int MyCounter
        {
            get { return myCounter; }
            set {
                if (myCounter != value)
                    myCounter = value;

                OnPropertyChanged("MyCounter");
            }
        }

        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(() => Image); }
        }

        public TrzyNaTrzyViewModel(IMyNavigationService navService)
        {
            
            DispatcherTimerSetup();

            this.navigationService = navService;
            InitCommand();
            Image = ViewModelLocator.DisplayImage;
            Original = GameHelper.OriginalImage(Image, 3);
            GameList = GameHelper.SplitImage(Image,3);
            MyCounter = 0;

        }

        public void InitCommand()
        {
            MouseClicked = new RelayCommand<ImageModel>( pImg =>
            {
                var EmptyCell = GameList.Where(b => b.Image == null).Single();

                if(EmptyCell.Image != GameHelper.ChangeImage(pImg, EmptyCell,2))
                {
                    EmptyCell.Image = GameHelper.ChangeImage(pImg, EmptyCell,2);
                    pImg.Image = null;
                    mediaPlayer = new MediaPlayer();
                    mediaPlayer.Open(ViewModelLocator.mediaUri);
                    mediaPlayer.Volume = 200;
                    mediaPlayer.Play();
                    MyCounter = MyCounter + 1;
                    if (GameHelper.CheckIt(Original,3))
                    {

                    }
                }
                
            });

            OnLoad = new RelayCommand(() =>
            {
                Image = ViewModelLocator.DisplayImage;
                Original = GameHelper.OriginalImage(Image, 3);
                GameList = GameHelper.SplitImage(Image, 3);
                MyCounter = 0;
                DispatcherTimerSetup();
            });

        }

        private void DispatcherTimerSetup()
        {
            DispatcherTimer dispathcerTimer = new DispatcherTimer();
            dispathcerTimer.Interval = TimeSpan.FromSeconds(1);
            dispathcerTimer.Tick += new EventHandler(CurrentTimeText);
            dispathcerTimer.Start();
        }

        private void CurrentTimeText(object sender, EventArgs e)
        {
            if(IsMixed)
            {
                MyCounter = 0;
                IsMixed = false;
            }
            DateTime currTime = DateTime.Now;
            DateTime newTime = currTime.Subtract(StartTime.TimeOfDay);
            CurrentTime = newTime.ToString("HH:mm:ss");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
