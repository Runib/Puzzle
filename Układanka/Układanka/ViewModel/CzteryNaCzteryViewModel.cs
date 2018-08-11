using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using Układanka.Helper;
using Układanka.Models;
using Układanka.Services;

namespace Układanka.ViewModel
{
    public class CzteryNaCzteryViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private MediaPlayer mediaPlayer;
        private IMyNavigationService navigationService;
        public static ObservableCollection<ImageModel> Original { get; set; } = new ObservableCollection<ImageModel>();
        public static ObservableCollection<ImageModel> GameList { get; set; } = new ObservableCollection<ImageModel>();


        public static bool IsMixed = false;
        public static DateTime StartTime;
        DispatcherTimer dispathcerTimer;
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<ImageModel> MouseClicked { get; set; }
        public RelayCommand OnLoad { get; set; }

        private string currentTime;
        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
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


        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(() => Image); }
        }

        private int myCounter;
        public int MyCounter
        {
            get { return myCounter; }
            set
            {
                if (myCounter != value)
                    myCounter = value;

                OnPropertyChanged("MyCounter");
            }
        }


        public CzteryNaCzteryViewModel(IMyNavigationService navService)
        {
            DispatcherTimerSetup();

            this.navigationService = navService;
            InitCommand();
            Image = ViewModelLocator.DisplayImage;
            GameList = GameHelper.SplitImage(Image, 4);
            MyCounter = 0;
        }

        public void InitCommand()
        {
            MouseClicked = new RelayCommand<ImageModel>(pImg =>
            {
                var EmptyCell = GameList.Where(b => b.Image == null).Single();

                if (EmptyCell.Image != GameHelper.ChangeImage(pImg, EmptyCell, 3))
                {
                    EmptyCell.Image = GameHelper.ChangeImage(pImg, EmptyCell, 3);
                    pImg.Image = null;
                    mediaPlayer = new MediaPlayer();
                    mediaPlayer.Open(ViewModelLocator.mediaUri);
                    mediaPlayer.Volume = 200;
                    mediaPlayer.Play();
                    MyCounter = MyCounter + 1;
                }
                
            });
            

            OnLoad = new RelayCommand(() =>
            {
                Image = ViewModelLocator.DisplayImage;
                GameList = GameHelper.SplitImage(Image, 4);
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
            if (IsMixed)
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
