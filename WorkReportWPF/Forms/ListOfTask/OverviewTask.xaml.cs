using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkReportWPF.Functions;
using WorkReportWPF.Models;

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
            if (dataGrid.SelectedItems.Count > 0)
            {
                TableTaskView data = (TableTaskView)dataGrid.SelectedItems[0];
                EditTask p = new EditTask(data);
                this.NavigationService.Navigate(p);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = TaskFunc.LoadTaskTable();
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}
