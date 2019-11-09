using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatchRename.Dialog
{
    /// <summary>
    /// Interaction logic for MoveDialog.xaml
    /// </summary>
    public partial class MoveDialog : Window
    {
        MoveArgs myArgs;
        public MoveDialog(StringArgs args)
        {
            InitializeComponent();
            myArgs = args as MoveArgs;
            MainListView.ItemsSource = Destination;
        }

        private void AddToListButton_Click(object sender, RoutedEventArgs e)
        {
            myArgs.From = StartAtTextBox.Text;
            myArgs.To = LengthTextBox.Text;

            var selectedItem = MainListView.SelectedItem as MyString;
            myArgs.Index = selectedItem.Value;
          
            DialogResult = true;
            Close();
        }


        BindingList<MyString> Destination = new BindingList<MyString>()
        {
            new MyString() { Value = "Begin" },
            new MyString() { Value = "End" },
        };


    }
}
