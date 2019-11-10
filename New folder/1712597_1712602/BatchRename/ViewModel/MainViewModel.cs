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
        /// <summary>
        ///File 
        /// </summary>
        //public BindingList<string> NewNameFolders { get; }
        //public BindingList<Filename> OldNameFiles { get; }

        ///// <summary>
        ///// Folder
        ///// </summary>
        //public BindingList<Foldername> OldNameFolders { get; }
        //public BindingList<string> NewFilename { get; }
        public ImageClass Icon { get; set; } 
        private int _selectedIndex;

        //public ICommand SelectFileCommand { get; set; }
        public ICommand SelectFolderCommand { get; set; }
        public ICommand SelectDialog { get; set; }
        public ICommand BatchFileCommand { get; set; }
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => Set(ref _selectedIndex, value);
        }
        public MainViewModel()
        {
            RegisterCommands();
            SelectedIndex = -1;
            Icon = new ImageClass();
            
        }
        private void RegisterCommands()
        {
            // SelectDialog = new RelayCommand(AddDialog);
            /*SelectFileCommand = new RelayCommand(AddFile);*/
          //  SelectFolderCommand = new RelayCommand(AddFolder);
            BatchFileCommand = new RelayCommand<string>(AddOperation);
           
        }

        private void AddOperation(string operation)
        {
            //switch (operation)
            //{
            //    case Replace:
            //        var replace = new ReplaceCommand(_replaceOld, _replaceNew, _isUsingRegex);
            //        _operations.Push(replace);
            //        MessageText = $"追加操作 - {replace}";
            //        break;
            //    case Nothing:
            //        _operations.Push(new DoNothingCommand());
            //        break;
            //    default:
            //        throw new ArgumentException();
            //}
            //UpdateNewName();
        }
        //private void AddFile()
        //{
        //    var dialog = new CommonOpenFileDialog
        //    {
        //        IsFolderPicker = true,
        //    };

        //    if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
        //    {
        //        return;
        //    }

        //    var dir = new DirectoryInfo(dialog.FileName);
        //    if (!dir.Exists)
        //    {
        //        return;
        //    }
        //    string path = dir + "\\";
        //    foreach (var fInf in dir.GetFiles())
        //    {
        //        OldNameFiles.Add(new Filename() { Value = fInf.Name, Path = path });
        //    }
        //}
        //private void AddFolder()
        //{
        //    var dialog = new CommonOpenFileDialog
        //    {
        //        IsFolderPicker = true,
        //    };

        //    if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
        //    {
        //        return;
        //    }

        //    var dir = new DirectoryInfo(dialog.FileName);
        //    if (!dir.Exists)
        //    {
        //        return;
        //    }
        //    string path = dir + "\\";
        //    foreach (var fInf in dir.GetDirectories())
        //    {
        //        //OldNameFolders.Add(new Foldername() { Value = fInf.Name, Path = path });
        //    }
        //}

      
    }
}
