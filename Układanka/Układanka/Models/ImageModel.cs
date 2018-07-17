using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Układanka.Models
{
    public class ImageModel :ViewModelBase
    {
        private ImageSource image;

        public ImageSource Image
        {
            get { return image; }
            set { image = value;RaisePropertyChanged(() => Image); }
        }

        public int Col { get; set; }
        public int Row { get; set; }
    }
}
