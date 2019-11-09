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
            //addList = new BindingList<IAction>();

        }
        public BindingList<Filename> filenameList;
        private void RefreshButton_Clik(object sender, RoutedEventArgs e)
        {

        }

        private void HelpButton_Clik(object sender, RoutedEventArgs e)
        { 

        }
        List<StringOperation> _prototypes = new List<StringOperation>();
      //  BindingList<IAction> addList;

        // list of actions displayed to user to select and modify arguments
      //  BindingList<ActionGUI> actionList;
        public BindingList<StringOperation> _actions = new BindingList<StringOperation>();
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
    }
}

