using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Układanka.Models;
using Układanka.ViewModel;

namespace Układanka.Helper
{
    public class GameHelper
    {
        private static ObservableCollection<ImageModel> Original { get; set; } = new ObservableCollection<ImageModel>();
        private static ObservableCollection<ImageModel> GameList { get; set; } = new ObservableCollection<ImageModel>();

        public static ObservableCollection<ImageModel> SplitImage(string img, int length)
        {
            Original.Clear();
            GameList.Clear();
            Bitmap original = new Bitmap(img);
            int position = 0;
            int index = 0;


            int widthOrg = original.Width / length, heighOrg = original.Height / length;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Rectangle srcRect = new Rectangle(j * widthOrg, i * heighOrg, widthOrg, heighOrg);
                    Bitmap objImg = original.Clone(srcRect, original.PixelFormat);
                    using (var memory = new MemoryStream())
                    {
                        objImg.Save(memory, ImageFormat.Png);
                        memory.Position = position;

                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memory;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        bitmapImage.Freeze();
                        Original.Add(new ImageModel { Image = bitmapImage, Row = i, Col = j, Text = index.ToString() });
                        


                    }
                    index++;
                }
            }
            Original.Last().Image = null;

            do
            {
                Randomize(length);
            } while (CheckIt(length) == false);

            return GameList;
        }

        private static bool CheckIt(int dlugosc)
        {
            int parity = 0;
            var EmptyField = GameList.Where(b => b.Image == null).SingleOrDefault();
            var numberOfRow = EmptyField.Row;

            for (int i = 0; i < GameList.Count - 1; i++)
            {
                for (int j = i + 1; j < GameList.Count - 1; j++)
                {
                    if (Int32.Parse(GameList[i].Text) > Int32.Parse(GameList[j].Text) && Int32.Parse(GameList[j].Text) != 0)
                    {
                        parity++;
                    }
                }
            }

            if (dlugosc % 2 == 1) //parzysty wiersz
            {
                if (numberOfRow % 2 == 1) // puste pole w nieparzystym wierszu
                    return parity % 2 == 0;
                else
                    return parity % 2 != 0;
            }
            else
                return parity % 2 == 0;
        }

        private static void Randomize(int length)
        {
            Random ran = new Random();
            GameList.Clear();
            int index = 0, newX = 0, newY = 0;
            List<System.Windows.Point> ListPoint = new List<System.Windows.Point>();
            var point = new System.Windows.Point(newX, newY);


            while (index < length*length)
            {
                newX = ran.Next(0, length);
                newY = ran.Next(0, length);
                point = new System.Windows.Point(newX, newY);

                if (!(ListPoint.Contains(point)))
                {
                    ListPoint.Add(point);
                    GameList.Add(Original[index]);
                    GameList[index].Row = newX;
                    GameList[index].Col = newY;
                    index++;
                }
            }
            ListPoint.Clear();
            sortGameList();
        }

        private static void sortGameList()
        {
            ImageModel help = new ImageModel();

            for (int i = 0; i < GameList.Count - 1; i++)
            {
                for (int j = 0; j < GameList.Count - 1; j++)
                {
                    if (GameList[j].Row > GameList[j + 1].Row)
                    {
                        help = GameList[j];
                        GameList[j] = GameList[j + 1];
                        GameList[j + 1] = help;
                    }
                    else if (GameList[j].Row == GameList[j + 1].Row)
                    {
                        if (GameList[j].Col > GameList[j + 1].Col)
                        {
                            help = GameList[j];
                            GameList[j] = GameList[j + 1];
                            GameList[j + 1] = help;
                        }
                        else if (GameList[j].Col == GameList[j + 1].Col)
                        {
                            MessageBox.Show("cuhjowu algorytm", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                            continue;
                    }
                }

            }
        }

        public static ImageSource ChangeImage(ImageModel ImgModel, ImageModel EmptyModel, int length)
        {
            if (ImgModel.Row == 0)
            {
                if (ImgModel.Col == 0)
                {
                    if ((ImgModel.Col + 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row) || (ImgModel.Col == EmptyModel.Col && ImgModel.Row + 1 == EmptyModel.Row))
                    {
                        EmptyModel.Image = ImgModel.Image;
                    }
                }
                else if (ImgModel.Col == length)
                {
                    if ((ImgModel.Col - 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row) || (ImgModel.Col == EmptyModel.Col && ImgModel.Row + 1 == EmptyModel.Row))
                    {
                        EmptyModel.Image = ImgModel.Image;
                    }
                }
                else
                {
                    if ((ImgModel.Col == EmptyModel.Col && ImgModel.Row + 1 == EmptyModel.Row) || (ImgModel.Col - 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row) || (ImgModel.Col + 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row))
                    {
                        EmptyModel.Image = ImgModel.Image;
                    }
                }
            }
            else if (ImgModel.Row == length)
            {
                if (ImgModel.Col == 0)
                {
                    if ((ImgModel.Col + 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row) || (ImgModel.Col == EmptyModel.Col && ImgModel.Row - 1 == EmptyModel.Row))
                    {
                        EmptyModel.Image = ImgModel.Image;
                    }
                }
                else if (ImgModel.Col == length)
                {
                    if ((ImgModel.Col - 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row) || (ImgModel.Col == EmptyModel.Col && ImgModel.Row - 1 == EmptyModel.Row))
                    {
                        EmptyModel.Image = ImgModel.Image;
                    }
                }
                else
                {
                    if ((ImgModel.Col + 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row) || (ImgModel.Col - 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row) || (ImgModel.Col == EmptyModel.Col && ImgModel.Row - 1 == EmptyModel.Row))
                    {
                        EmptyModel.Image = ImgModel.Image;
                    }
                }
            }
            else 
            {
                if (ImgModel.Col == 0)
                {
                    if ((ImgModel.Col + 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row) || (ImgModel.Col == EmptyModel.Col && ImgModel.Row + 1 == EmptyModel.Row) || (ImgModel.Col == EmptyModel.Col && ImgModel.Row - 1 == EmptyModel.Row))
                    {
                        EmptyModel.Image = ImgModel.Image;
                    }
                }
                else if (ImgModel.Col == length)
                {
                    if ((ImgModel.Col - 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row) || (ImgModel.Col == EmptyModel.Col && ImgModel.Row + 1 == EmptyModel.Row) || (ImgModel.Col == EmptyModel.Col && ImgModel.Row - 1 == EmptyModel.Row))
                    {
                        EmptyModel.Image = ImgModel.Image;
                    }
                }
                else
                {
                    if ((ImgModel.Col == EmptyModel.Col && ImgModel.Row + 1 == EmptyModel.Row) || (ImgModel.Col == EmptyModel.Col && ImgModel.Row - 1 == EmptyModel.Row) || (ImgModel.Col + 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row) || (ImgModel.Col - 1 == EmptyModel.Col && ImgModel.Row == EmptyModel.Row))
                    {
                        EmptyModel.Image = ImgModel.Image;
                    }
                }
            }

            return EmptyModel.Image;
        }





        }


        
}

