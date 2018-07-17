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

namespace Układanka.ViewModel
{
    public class TrzyNaTrzyViewModel : ViewModelBase
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
                    navigationService.NavigateTo(ViewModelLocator.TrzyNaTrzyKey,Image);
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

        public TrzyNaTrzyViewModel(IMyNavigationService navService)
        {
            this.navigationService = navService;
            InitCommand();
            Image = navigationService.Parameter.ToString();
            SplitImage();

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

            MouseClicked = new RelayCommand<ImageModel>( pImg =>
            {
                var EmptyCell = GameList.Where(b => b.Image == null).Single();
                if(pImg.Row == 0)
                {
                    if (pImg.Col == 0)
                    {
                        if ((pImg.Col+1==EmptyCell.Col && pImg.Row==EmptyCell.Row)|| (pImg.Col  == EmptyCell.Col && pImg.Row + 1 == EmptyCell.Row))
                        {
                            EmptyCell.Image = pImg.Image;
                            pImg.Image = null;
                        }
                    }
                    else if (pImg.Col == 2)
                    {
                        if ((pImg.Col -1 == EmptyCell.Col && pImg.Row  == EmptyCell.Row)|| (pImg.Col  == EmptyCell.Col && pImg.Row + 1 == EmptyCell.Row))
                        {
                            EmptyCell.Image = pImg.Image;
                            pImg.Image = null;
                        }
                    }
                    else 
                    {
                        if ((pImg.Col  == EmptyCell.Col && pImg.Row + 1 == EmptyCell.Row)|| (pImg.Col -1 == EmptyCell.Col && pImg.Row == EmptyCell.Row)|| (pImg.Col+1 == EmptyCell.Col && pImg.Row == EmptyCell.Row))
                        {
                            EmptyCell.Image = pImg.Image;
                            pImg.Image = null;
                        }
                    }
                }
                else if(pImg.Row==1)
                {
                    if (pImg.Col == 0)
                    {
                        if ((pImg.Col + 1 == EmptyCell.Col && pImg.Row == EmptyCell.Row) || (pImg.Col == EmptyCell.Col && pImg.Row + 1 == EmptyCell.Row) || (pImg.Col == EmptyCell.Col && pImg.Row - 1 == EmptyCell.Row))
                        {
                            EmptyCell.Image = pImg.Image;
                            pImg.Image = null;
                        }
                    }
                    else if (pImg.Col == 2)
                    {
                        if ((pImg.Col - 1 == EmptyCell.Col && pImg.Row == EmptyCell.Row) || (pImg.Col == EmptyCell.Col && pImg.Row + 1 == EmptyCell.Row)|| (pImg.Col == EmptyCell.Col && pImg.Row - 1 == EmptyCell.Row))
                        {
                            EmptyCell.Image = pImg.Image;
                            pImg.Image = null;
                        }
                    }
                    else
                    {
                        if ((pImg.Col  == EmptyCell.Col && pImg.Row + 1 == EmptyCell.Row) || (pImg.Col == EmptyCell.Col && pImg.Row -1 == EmptyCell.Row) || (pImg.Col +1  == EmptyCell.Col && pImg.Row  == EmptyCell.Row) || (pImg.Col - 1 == EmptyCell.Col && pImg.Row == EmptyCell.Row))
                        {
                            EmptyCell.Image = pImg.Image;
                            pImg.Image = null;
                        }
                    }
                }
                else if (pImg.Row == 2)
                {
                    if (pImg.Col == 0)
                    {
                        if ((pImg.Col + 1 == EmptyCell.Col && pImg.Row == EmptyCell.Row) || (pImg.Col == EmptyCell.Col && pImg.Row - 1 == EmptyCell.Row))
                        {
                            EmptyCell.Image = pImg.Image;
                            pImg.Image = null;
                        }
                    }
                    else if (pImg.Col == 2)
                    {
                        if ((pImg.Col-1 == EmptyCell.Col && pImg.Row == EmptyCell.Row) || (pImg.Col == EmptyCell.Col && pImg.Row -1 == EmptyCell.Row))
                        {
                            EmptyCell.Image = pImg.Image;
                            pImg.Image = null;
                        }
                    }
                    else
                    {
                        if ((pImg.Col + 1 == EmptyCell.Col && pImg.Row  == EmptyCell.Row) || (pImg.Col -1 == EmptyCell.Col && pImg.Row == EmptyCell.Row) || (pImg.Col == EmptyCell.Col && pImg.Row - 1 == EmptyCell.Row))
                        {
                            EmptyCell.Image = pImg.Image;
                            pImg.Image = null;
                        }
                    }
                }
            });

        }

        public void SplitImage()
        {
            Bitmap original = new Bitmap(Image);
            int position = 0;
            

            int widthOrg=original.Width/3, heighOrg=original.Height/3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Rectangle srcRect = new Rectangle(j*widthOrg, i*heighOrg, widthOrg, heighOrg);
                    Bitmap objImg = original.Clone(srcRect, original.PixelFormat);
                    using (var memory = new MemoryStream())
                    {
                        objImg.Save(memory, ImageFormat.Png);
                        memory.Position = position ;

                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memory;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        bitmapImage.Freeze();
                        Original.Add(new ImageModel { Image = bitmapImage, Row = i, Col = j });
                        Randomize(bitmapImage);
                        
                    }
                }
            }
            Original.Last().Image = null;

        }

        public void Randomize(BitmapImage img)
        {
            Random ran = new Random();

            
        }
    }
}
