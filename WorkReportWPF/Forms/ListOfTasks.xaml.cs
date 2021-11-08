using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Forms.ListOfTask;

namespace WorkReportWPF
{
    /// <summary>
    /// Interakční logika pro ListOfTasks.xaml
    /// </summary>
    public partial class ListOfTasks : Page
    {
        public ListOfTasks()
        {
            InitializeComponent();
        }

        private void Click_Overview(object sender, RoutedEventArgs e)
        {
            frameTask.Content = new OverviewTask();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            frameTask.Content = new OverviewTask();
        }

        private void Click_MyTask(object sender, RoutedEventArgs e)
        {
            frameTask.Content = new MyTask();
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            frameTask.Content = new AddTask();
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            frameTask.Content = new EditTask();
        }
    }
}
