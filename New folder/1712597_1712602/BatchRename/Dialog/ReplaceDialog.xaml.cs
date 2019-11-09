using System;
using System.Collections.Generic;
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
            //fromTextBox.Text = myArgs.From;
        }

        private void AddToListButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
