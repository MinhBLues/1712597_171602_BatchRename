using BatchRename.Dialog;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BatchRename.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ImageClass Icon { get; set; } 
        public MainViewModel()
        {
            Icon = new ImageClass();
        }
    }
}
