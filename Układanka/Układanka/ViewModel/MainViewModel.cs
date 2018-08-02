using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Układanka.Services;
using System.Windows;
using System.Windows.Controls;
using Układanka.Helper;
using System.ComponentModel;

namespace Układanka.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IMyNavigationService navigationService;


        private string ChoosenSplit = null;

        public RelayCommand ChooseImageCommand { get; set; }
        public RelayCommand MixImageCommand { get; set; }


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

        public MainViewModel(IMyNavigationService navService)
        {
            SelectedInd = -1;
            navigationService = navService;
            InitCommand();
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
                        TrzyNaTrzyViewModel.GameList.Clear();
                        TrzyNaTrzyViewModel.GameList = GameHelper.SplitImage(Image, 3);
                    }
                    else if (ChoosenSplit == "4x4")
                    {
                        CzteryNaCzteryViewModel.GameList.Clear();
                        CzteryNaCzteryViewModel.GameList = GameHelper.SplitImage(Image, 4);
                    }
                    else if (ChoosenSplit == "5x5")
                    {
                        PiecNaPiecViewModel.GameList.Clear();
                        PiecNaPiecViewModel.GameList = GameHelper.SplitImage(Image, 5);
                    }
                }
            });
        }
    }
}