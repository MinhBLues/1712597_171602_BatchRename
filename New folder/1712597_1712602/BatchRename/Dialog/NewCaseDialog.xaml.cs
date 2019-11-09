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
    /// Interaction logic for NewCaseDialog.xaml
    /// </summary>
    public partial class NewCaseDialog : Window
    {
        NewCaseArgs myArgs;
        public NewCaseDialog(StringArgs args)
        {
            InitializeComponent();
            myArgs = args as NewCaseArgs;
            MainGrid.DataContext = this;
            MainListView.ItemsSource = cases;
            
        }
        BindingList<MyString> cases = new BindingList<MyString>()
            {
                new MyString(){ Value="LowerCase" },
                new MyString(){ Value="UpperCase" },
                new MyString(){ Value="CapitalizedCase" },
            };
        private void AddToListButton_Click(object sender, RoutedEventArgs e)
        {
            myArgs.Case = (MainListView.SelectedItem as MyString).Value;
            DialogResult = true;
            Close();
        }
    }
}
