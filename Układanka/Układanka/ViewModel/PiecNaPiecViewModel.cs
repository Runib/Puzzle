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
        public static ObservableCollection<ImageModel> Original { get; set; } = new ObservableCollection<ImageModel>();
        public static ObservableCollection<ImageModel> GameList { get; set; } = new ObservableCollection<ImageModel>();
        public int[] num = new int[9];


        public RelayCommand<ImageModel> MouseClicked { get; set; }
        public RelayCommand OnLoad { get; set; }

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
            set { myCounter = value; RaisePropertyChanged(() => MyCounter); }
        }

        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(() => Image); }
        }

        public PiecNaPiecViewModel(IMyNavigationService navService)
        {
            this.navigationService = navService;
            InitCommand();
            Image = ViewModelLocator.DisplayImage;
            GameList = GameHelper.SplitImage(Image, 5);
            MyCounter = 0;

        }

        public void InitCommand()
        {
            MouseClicked = new RelayCommand<ImageModel>(pImg =>
            {
                var EmptyCell = GameList.Where(b => b.Image == null).Single();

                if (EmptyCell.Image != GameHelper.ChangeImage(pImg, EmptyCell, 4))
                {
                    EmptyCell.Image = GameHelper.ChangeImage(pImg, EmptyCell, 4);
                    pImg.Image = null;
                }
                MyCounter++;
            });

            OnLoad = new RelayCommand(() =>
            {
                Image = ViewModelLocator.DisplayImage;
                GameList = GameHelper.SplitImage(Image, 5);
                MyCounter = 0;
            });

        }
    }
}
