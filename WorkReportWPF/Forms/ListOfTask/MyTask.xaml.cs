using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkReportWPF.Functions;

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

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = TaskFunc.LoadMyTaskTable();
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}
