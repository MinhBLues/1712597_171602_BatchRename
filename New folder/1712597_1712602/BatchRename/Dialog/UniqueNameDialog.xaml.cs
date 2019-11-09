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
    /// Interaction logic for UniqueNameDialog.xaml
    /// </summary>
    public partial class UniqueNameDialog : Window
    {
        UniqueArgs myArgs;
        public UniqueNameDialog(StringArgs args)
        {
            InitializeComponent();
            myArgs = args as UniqueArgs;
            MainListView.ItemsSource = Options;
        }


        BindingList<MyString> Options = new BindingList<MyString>()
        {
            new MyString() { Value = "Change name to GUID" },
        };
        private void AddToListButton_Click(object sender, RoutedEventArgs e)
        {
            myArgs.From = (MainListView.SelectedItem as MyString).Value;
            DialogResult = true;
            Close();
        }
    }
}
