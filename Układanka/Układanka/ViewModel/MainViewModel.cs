using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Układanka.Services;
using System.Windows;
using System.Windows.Controls;
using Układanka.Helper;
using System.ComponentModel;
using System.Windows.Media;

namespace Układanka.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IMyNavigationService navigationService;

        private MediaPlayer mediaPlayer;
        private bool play = true;
        private string ChoosenSplit = null;

        public RelayCommand SoundCommand { get; set; }
        public RelayCommand ChooseImageCommand { get; set; }
        public RelayCommand MixImageCommand { get; set; }

        private string imageSound;
        public string ImageSound
        {
            get { return imageSound; }
            set { imageSound = value; RaisePropertyChanged(() => ImageSound); }
        }


        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(() => Image); }
        }

        private string selectedSplit;
        public string SelectedSplit
        {
            get { return selectedSplit; }
            set { selectedSplit = value; RaisePropertyChanged(() => SelectedSplit); if (!string.IsNullOrEmpty(value)) CheckSplit(); SelectedInd = -1; }
        }

        private int selectedInd;

        public int SelectedInd
        {
            get { return selectedInd; }
            set { selectedInd = value; RaisePropertyChanged(() => SelectedInd); }
        }


        private void CheckSplit()
        {
            if (SelectedSplit=="3x3")
            {
                if (Image != null)
                {
                    TrzyNaTrzyViewModel.StartTime = DateTime.Now;
                    TrzyNaTrzyViewModel.GameList.Clear();
                    navigationService.NavigateTo(ViewModelLocator.TrzyNaTrzyKey, Image);
                    ChoosenSplit = "3x3";
                }
                else
                {
                    MessageBox.Show("Wybierz obraz", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                    
                
            }
            else if (SelectedSplit=="4x4")
            {
                if (Image != null)
                {
                    CzteryNaCzteryViewModel.StartTime = DateTime.Now;
                    CzteryNaCzteryViewModel.GameList.Clear();
                    navigationService.NavigateTo(ViewModelLocator.CzteryNaCzteryKey, Image);
                    ChoosenSplit = "4x4";
                }
                else
                {

                    MessageBox.Show("Wybierz obraz", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (SelectedSplit=="5x5")
            {

                if (Image != null)
                {
                    PiecNaPiecViewModel.StartTime = DateTime.Now;
                    PiecNaPiecViewModel.GameList.Clear();
                    navigationService.NavigateTo(ViewModelLocator.PiecNaPiecKey, Image);
                    ChoosenSplit = "5x5";
                }
                else
                {
                    MessageBox.Show("Wybierz obraz", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                   
            }
        }

        public void PlayOrStop()
        {
            if (play)
            {
                mediaPlayer.MediaEnded += new EventHandler(Media_Ended);
                mediaPlayer.Play();
            }
            else
                mediaPlayer.Pause();
        }

        public MainViewModel(IMyNavigationService navService)
        {
            SelectedInd = -1;
            navigationService = navService;
            InitCommand();
            ImageSound = "/Images/speaker.png";
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("D:/c#/Nowy folder/Układanka/Układanka/Układanka/Music/Rick and Morty - Evil Morty.mp3"));
            mediaPlayer.Volume = 150;
            PlayOrStop();
        }

        public void InitCommand()
        {
            ChooseImageCommand = new RelayCommand(() =>
            {
                var dialog = new OpenFileDialog()
                {
                    //TODO: filters
                };
                dialog.DefaultExt = ".jpg";
                dialog.Filter = "Image (*.jpg)|*.jpg|Image (*.png)|*.png";

                if (dialog.ShowDialog() == true)
                {
                    Image = dialog.FileName;
                    ViewModelLocator.DisplayImage = Image; 
                    ChoosenSplit = null;
                    navigationService.NavigateTo(ViewModelLocator.DisplayImageKey,Image, true);
                }
            });

            SoundCommand = new RelayCommand(() =>
            {
                if(play)
                    ImageSound= "/Images/mute.png";
                else
                    ImageSound= "/Images/speaker.png";
                play = !play;
                PlayOrStop();
            });

            MixImageCommand = new RelayCommand(() =>
            {
                if (Image==null)
                {
                    MessageBox.Show("Wybierz obraz", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (ChoosenSplit==null)
                {
                    MessageBox.Show("Wybierz poziom", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (ChoosenSplit == "3x3")
                    {
                        TrzyNaTrzyViewModel.StartTime = DateTime.Now;
                        TrzyNaTrzyViewModel.GameList.Clear();
                        TrzyNaTrzyViewModel.IsMixed = true;
                        TrzyNaTrzyViewModel.GameList = GameHelper.SplitImage(Image, 3);
                    }
                    else if (ChoosenSplit == "4x4")
                    {
                        CzteryNaCzteryViewModel.StartTime = DateTime.Now;
                        CzteryNaCzteryViewModel.GameList.Clear();
                        CzteryNaCzteryViewModel.IsMixed = true;
                        CzteryNaCzteryViewModel.GameList = GameHelper.SplitImage(Image, 4);
                    }
                    else if (ChoosenSplit == "5x5")
                    {
                        PiecNaPiecViewModel.StartTime = DateTime.Now;
                        PiecNaPiecViewModel.GameList.Clear();
                        PiecNaPiecViewModel.IsMixed = true;
                        PiecNaPiecViewModel.GameList = GameHelper.SplitImage(Image, 5);
                    }
                }
            });
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}