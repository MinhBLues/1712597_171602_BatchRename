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
    /// Interaction logic for ReplaceDialog.xaml
    /// </summary>
    public partial class ReplaceDialog : Window
    {
        ReplaceArgs myArgs;
        public ReplaceDialog(StringArgs args)
        {
            InitializeComponent();
            myArgs = args as ReplaceArgs;
            FromTextBox.Text = myArgs.From;
            ToTextBox.Text = myArgs.To;
            AreaComboBox.ItemsSource = cases;
        }
        BindingList<MyString> cases = new BindingList<MyString>()
        {
            new MyString(){ Value="Name" },
            new MyString(){ Value="Extension" },
        };

        private void AddToListButton_Click(object sender, RoutedEventArgs e)
        {
            myArgs.From = FromTextBox.Text;
            myArgs.To = ToTextBox.Text;
            myArgs.Area = (AreaComboBox.SelectedItem as MyString).Value;
            DialogResult = true;
            Close();
        }
    }
}
