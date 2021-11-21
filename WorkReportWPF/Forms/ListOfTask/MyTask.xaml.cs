using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkReportWPF.Functions;
using WorkReportWPF.Models;

namespace WorkReportWPF.Forms.ListOfTask
{
    /// <summary>
    /// Interaction logic for MyTask.xaml
    /// </summary>
    public partial class MyTask : Page
    {
        public MyTask()
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
            dataGrid.ItemsSource = TaskFunc.LoadMyTaskTable();
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}
