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
    public class CzteryNaCzteryViewModel : ViewModelBase 
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


        public CzteryNaCzteryViewModel(IMyNavigationService navService)
        {
            this.navigationService = navService;
            InitCommand();
            Image = navigationService.Parameter.ToString();
            GameList = GameHelper.SplitImage(Image, 4);

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
                }
            });

        }
    }
}
