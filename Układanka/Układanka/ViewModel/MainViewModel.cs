using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Układanka.Services;

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
            set { selectedSplit = value; RaisePropertyChanged(() => SelectedSplit); if (!string.IsNullOrEmpty(value)) CheckSplit(); }
        }

        private void CheckSplit()
        {
            if (SelectedSplit=="3x3")
            {
                if(Image!=null)
                    navigationService.NavigateTo(ViewModelLocator.TrzyNaTrzyKey,Image);
                else
                    navigationService.NavigateTo(ViewModelLocator.TrzyNaTrzyKey,Image);
                
            }
            else if (SelectedSplit=="4x4")
            {

            }
            else if (SelectedSplit=="5x5")
            {

            }
            Image = null;
        }

        public MainViewModel(IMyNavigationService navService)
        {
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
                }
            });
        }
    }
}