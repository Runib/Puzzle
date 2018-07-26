using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Układanka.Services;

namespace Układanka.ViewModel
{
    public class DisplayImageViewModel : ViewModelBase
    {
        private IMyNavigationService navigationService;

        public RelayCommand ChooseImageCommand { get; set; }

        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(() => Image); }
        }

        public RelayCommand OnLoad { get;  set; }
        //probowalem wiele rzeczy i zapomnialem pouusuwac tez xD luzno kminie sobie tylko
        public DisplayImageViewModel(IMyNavigationService navService)
        {
            this.navigationService = navService;
            Image = navigationService.Parameter.ToString();
            InitCommand();
        }

        public void InitCommand()
        {
            OnLoad = new RelayCommand(() =>
            {
                //to sie wogole wywolalo?ktoro?idk, trzeba sprawdzic
                Image= ViewModelLocator.DisplayImage;
            });
        }
    }
}
