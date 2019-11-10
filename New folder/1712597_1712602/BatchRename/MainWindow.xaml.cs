using BatchRename.Dialog;
using BatchRename.ViewModel;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _actions = new BindingList<StringOperation>();

            OldNameFolders = new BindingList<Foldername>();
            NewFolderName = new BindingList<string>();

            OldNameFiles = new BindingList<Filename>();
            NewFileName = new BindingList<string>();

            FileListView.ItemsSource = OldNameFiles;
            FolderListView.ItemsSource = OldNameFolders;

            OldNamesFolder = new ObservableCollection<DirectoryInfo>();
            OldNamesFile = new ObservableCollection<FileInfo>();
        }
        public BindingList<string> NewFolderName { get; }
        public BindingList<Filename> OldNameFiles { get; }

        public string pathfolder;
        public ObservableCollection<DirectoryInfo> OldNamesFolder { get; }
        public ObservableCollection<FileInfo> OldNamesFile { get; }
        /// <summary>
        /// Folder
        /// </summary>
        public BindingList<Foldername> OldNameFolders { get; }
        public BindingList<string> NewFileName { get; }

        public BindingList<Filename> filenameList;
        private void RefreshButton_Clik(object sender, RoutedEventArgs e)
        {
            _actions.Clear();

            // clear all filenames added
            OldNameFiles.Clear();

            // clear all folderames added
            OldNameFolders.Clear();

        }
        private void HelpButton_Clik(object sender, RoutedEventArgs e)
        {
            string message = "An project from Window Programming Course\n"
                + "Performed by:\n"
                + "1712597 - Pham Ba Minh\n"
                + "1712602 - Nguyen Thi Cam My\n"
                + "Contact: 1712597@student.hcmus.edu.vn\n"
                + "Contact: 1712602@student.hcmus.edu.vn";

            string caption = "Batch Rename Information";
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;

            // Show message box
            MessageBoxResult result = System.Windows.MessageBox.Show(message, caption, buttons, icon);
        }
        List<StringOperation> _prototypes = new List<StringOperation>();

        // list of actions displayed to user to select and modify arguments
        public BindingList<StringOperation> _actions { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var prototype1 = new ReplaceOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = "From",
                    To = "To"
                }
            };
            var prototype2 = new NewCaseOperation()
            {
                Args = new NewCaseArgs()
                {

                }
            };
            var prototype3 = new NameNormalizeOperation()
            {
                Args = new NameNormalizeArgs()
                {

                }
            };
            var prototype4 = new MoveOperation()
            {
                Args = new MoveArgs()
                {

                }
            };
            var prototype5 = new UniqueOperation()
            {
                Args = new UniqueArgs()
                {

                }
            };

            _prototypes.Add(prototype1);
            _prototypes.Add(prototype2);
            _prototypes.Add(prototype3);
            _prototypes.Add(prototype4);
            _prototypes.Add(prototype5);

            ActionsListView.ItemsSource = _prototypes;

            operationsListBox.ItemsSource = _actions;

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var action = ActionsListView.SelectedItem as StringOperation;
            _actions.Add(action.Clone());
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = operationsListBox.SelectedItem as StringOperation;

            item.Config();
        }
        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var index = operationsListBox.SelectedIndex;
            _actions.RemoveAt(index);
        }
        private void AddFile(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
            };

            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return;
            }

            var dir = new DirectoryInfo(dialog.FileName);
            if (!dir.Exists)
            {
                return;
            }
            string path = dir + "\\";
            foreach (var fInf in dir.GetFiles())
            {
                OldNameFiles.Add(new Filename() { Value = fInf.Name, Path = path });
                OldNamesFile.Add(fInf);
            }
        }
        private void AddFolder(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
            };

            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return;
            }

            var dir = new DirectoryInfo(dialog.FileName);
            if (!dir.Exists)
            {
                return;
            }
            string path = dir + "\\";
            pathfolder = dir.FullName;
            foreach (var fInf in dir.GetDirectories())
            {
                OldNameFolders.Add(new Foldername() { Value = fInf.Name, Path = path });
                OldNamesFolder.Add(fInf);
            }
        }
        private void BatchFileButton_Click(object sender, RoutedEventArgs e)
        {
            // show message box warns user about options selected such as: make new name or skip, ...

            foreach (var filename in OldNameFiles)
            {
                // each filename
                string newFilename = filename.Value;
                filename.BatchState = "Success";
                filename.FailedActions = "Failed Actions List:\n";
                bool isSuccess = true;

                foreach (var action in _actions)
                {
                    try
                    {
                        newFilename = action.ActionProcess(newFilename, true);
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        filename.FailedActions += action.Description + "\n";

                    }
                }
                filename.NewFilename = newFilename;
                NewFileName.Add(newFilename);

                if (!isSuccess)
                {
                    filename.BatchState = "Fail";
                }
            }
        }
        private void BatchFolderButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var foldername in OldNameFolders)
            {
                // each filename
                string newFoldername = foldername.Value;
                foldername.BatchState = "Success";
                foldername.FailedActions = "Failed Actions List:\n";
                bool isSuccess = true;

                foreach (var action in _actions)
                {
                    try
                    {
                        newFoldername = action.ActionProcess(newFoldername, true);
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        foldername.FailedActions += action.Description + "\n";
                    }
                }
                foldername.NewFoldername = newFoldername;
                NewFolderName.Add(newFoldername);
                if (!isSuccess)
                {
                    foldername.BatchState = "Fail";
                }

            }
        }
        private void SaveFolder_Click(object sender, RoutedEventArgs e)
        {
            var error = 0;
            for (var i = 0; i < OldNameFolders.Count; ++i)
            {
                if (!IsValidFileName(NewFolderName[i]))
                {
                    ++error;
                    continue;
                }
                string name = OldNamesFolder[i].FullName + "25031999";
                Directory.Move(OldNamesFolder[i].FullName, name);
                Directory.Move(name, $"{pathfolder}\\{NewFolderName[i]}");
            }
            UpdateOldNameFolder();
            UpdateNewNameFolder();
        }
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            var error = 0;
            for (var i = 0; i < OldNameFiles.Count; ++i)
            {
                if (!IsValidFileName(NewFileName[i]))
                {
                    ++error;
                    continue;
                }
                var dir = OldNamesFile[i].Directory;
                OldNamesFile[i].MoveTo($"{dir?.FullName}/{NewFileName[i]}");
            }
            UpdateOldNameFile();
            UpdateNewNameFile();

        }
        private void UpdateNewNameFolder()
        {
            int i = 0;
            foreach (var fInf in NewFolderName)
            {
                OldNameFolders[i].Value = fInf;
                i++;
            }
        }
        private void UpdateOldNameFolder()
        {
            var tempList = OldNamesFolder.ToList();
            OldNamesFolder.Clear();

            foreach (var fInf in tempList)
            {
                fInf.Refresh();
                OldNamesFolder.Add(fInf);
            }
        }
        private void UpdateNewNameFile()
        {
            int i = 0;
            foreach (var fInf in NewFileName)
            {
                OldNameFiles[i].Value = fInf;
                i++;
            }
        }
        private void UpdateOldNameFile()
        {
            var tempList = OldNamesFile.ToList();
            OldNamesFile.Clear();

            foreach (var fInf in tempList)
            {
                fInf.Refresh();
                OldNamesFile.Add(fInf);
            }
        }
        private static bool IsValidFileName(string fName)
        {
            return !InvalidChars.Any(fName.Contains);

        }

        private static readonly char[] InvalidChars = @"\/:*?""<>|".ToCharArray();

    }
    }


