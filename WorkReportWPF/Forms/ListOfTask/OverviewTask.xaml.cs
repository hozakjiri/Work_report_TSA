using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkReportWPF.Functions;

namespace WorkReportWPF.Forms.ListOfTask
{
    /// <summary>
    /// Interaction logic for OverviewTask.xaml
    /// </summary>
    public partial class OverviewTask : Page
    {
        public OverviewTask()
        {
            InitializeComponent();
        }

        private void dataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = TaskFunc.LoadTaskTable();
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}
