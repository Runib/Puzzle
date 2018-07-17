using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Układanka.Services
{
    public interface IMyNavigationService:INavigationService
    {
        object Parameter { get; }
    }
}
