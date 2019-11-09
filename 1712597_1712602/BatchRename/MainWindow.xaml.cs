using BatchRename.Dialog;
using BatchRename.ViewModel;
using System;
using System.Collections.Generic;
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

        }
        public BindingList<Filename> filenameList;
        private void RefreshButton_Clik(object sender, RoutedEventArgs e)
        {

        }

        private void HelpButton_Clik(object sender, RoutedEventArgs e)
        { 

        }
        List<StringOperation> _prototypes = new List<StringOperation>();

        BindingList<StringOperation> _actions = new BindingList<StringOperation>();
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

            _prototypes.Add(prototype1);

            ActionsListView.ItemsSource = _prototypes;

           // ActionsListView.ItemsSource = _actions;
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            var action = ActionsListView.SelectedItem as StringOperation;
            _actions.Add(action.Clone());
            var item = ActionsListView.SelectedItem as StringOperation;
            item.Config();
        }
    }
}

