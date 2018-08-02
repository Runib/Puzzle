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

namespace Układanka.ViewModel
{
    public class TrzyNaTrzyViewModel : ViewModelBase
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

        public TrzyNaTrzyViewModel(IMyNavigationService navService)
        {
            this.navigationService = navService;
            InitCommand();
            Image = ViewModelLocator.DisplayImage;
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
                }
                MyCounter++;
            });

            OnLoad = new RelayCommand(() =>
            {
                Image = ViewModelLocator.DisplayImage;
                GameList = GameHelper.SplitImage(Image, 3);
                MyCounter = 0;
            });

        }
    }
}
