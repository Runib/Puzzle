using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Układanka.Services;
using System.Windows;
using System.Windows.Controls;

namespace Układanka.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IMyNavigationService navigationService;

        public RelayCommand ChooseImageCommand { get; set; }

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
                    navigationService.NavigateTo(ViewModelLocator.TrzyNaTrzyKey, Image);
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
                    navigationService.NavigateTo(ViewModelLocator.CzteryNaCzteryKey, Image);
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
                    navigationService.NavigateTo(ViewModelLocator.PiecNaPiecKey, Image);
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
                    ViewModelLocator.DisplayImage = Image; //no dobra jedyne co mi teraz przychodzi do glowy

                    navigationService.NavigateTo(ViewModelLocator.DisplayImageKey,Image, true);
                }
            });
        }
    }
}