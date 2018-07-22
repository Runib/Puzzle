using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Układanka.Helper;
using Układanka.Models;
using Układanka.Services;

namespace Układanka.ViewModel
{
    public class PiecNaPiecViewModel :ViewModelBase
    {
        private IMyNavigationService navigationService;
        public ObservableCollection<ImageModel> Original { get; set; } = new ObservableCollection<ImageModel>();
        public ObservableCollection<ImageModel> GameList { get; set; } = new ObservableCollection<ImageModel>();
        public int[] num = new int[9];


        public RelayCommand ChooseImageCommand { get; set; }
        public RelayCommand<ImageModel> MouseClicked { get; set; }

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

        private string selectedSplit;
        public string SelectedSplit
        {
            get { return selectedSplit; }
            set { selectedSplit = value; RaisePropertyChanged(() => SelectedSplit); if (!string.IsNullOrEmpty(value)) CheckSplit(); }
        }

        private void CheckSplit()
        {
            if (SelectedSplit == "3x3")
            {
                if (Image != null)
                    navigationService.NavigateTo(ViewModelLocator.TrzyNaTrzyKey, Image);
                else
                    navigationService.NavigateTo(ViewModelLocator.TrzyNaTrzyKey);
            }
            else if (SelectedSplit == "4x4")
            {
                if (Image != null)
                    navigationService.NavigateTo(ViewModelLocator.CzteryNaCzteryKey, Image);
                else
                    navigationService.NavigateTo(ViewModelLocator.CzteryNaCzteryKey);
            }
            else if (SelectedSplit == "5x5")
            {
                if (Image != null)
                    navigationService.NavigateTo(ViewModelLocator.PiecNaPiecKey, Image);
                else
                    navigationService.NavigateTo(ViewModelLocator.PiecNaPiecKey);
            }
        }

        public PiecNaPiecViewModel(IMyNavigationService navService)
        {
            this.navigationService = navService;
            InitCommand();
            Image = navigationService.Parameter.ToString();
            GameList = GameHelper.SplitImage(Image, 5);

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

            MouseClicked = new RelayCommand<ImageModel>(pImg =>
            {
                var EmptyCell = GameList.Where(b => b.Image == null).Single();

                if (EmptyCell.Image != GameHelper.ChangeImage(pImg, EmptyCell, 4))
                {
                    EmptyCell.Image = GameHelper.ChangeImage(pImg, EmptyCell, 4);
                    pImg.Image = null;
                }
            });

        }
    }
}
