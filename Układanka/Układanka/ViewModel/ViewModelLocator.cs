using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Układanka.Services;

namespace Układanka.ViewModel
{

    public class ViewModelLocator
    {
        //Tutaj stringi kluczy dla innych pagow
        public const string TrzyNaTrzyKey = "TrzyNaTrzyView";
        public const string CzteryNaCzteryKey = "CzteryNaCzteryView";
        public const string PiecNaPiecKey = "PiecNaPiecView";

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SetupNavigation();

            SimpleIoc.Default.Register<TrzyNaTrzyViewModel>();
            SimpleIoc.Default.Register<CzteryNaCzteryViewModel>();
            SimpleIoc.Default.Register<PiecNaPiecViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        private static void SetupNavigation()
        {
            var navigationService = new MyNavigationService();
            navigationService.Configure("TrzyNaTrzyView", new Uri("../View/TrzyNaTrzyView.xaml", UriKind.Relative));
            navigationService.Configure("CzteryNaCzteryView", new Uri("../View/CzteryNaCzteryView.xaml", UriKind.Relative));
            navigationService.Configure("PiecNaPiecView", new Uri("../View/PiecNaPiecView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IMyNavigationService>(() => navigationService);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public CzteryNaCzteryViewModel Cztery
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CzteryNaCzteryViewModel>();
            }
        }

        public TrzyNaTrzyViewModel Trzy
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TrzyNaTrzyViewModel>();
            }
        }

        public PiecNaPiecViewModel Piec
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PiecNaPiecViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}