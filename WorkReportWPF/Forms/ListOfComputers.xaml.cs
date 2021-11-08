using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Forms.ListOfComputers;

namespace WorkReportWPF
{
    /// <summary>
    /// Interakční logika pro ListOfComputers.xaml
    /// </summary>
    public partial class ListOfComputers : Page
    {
        public ListOfComputers()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            frameComputers.Content = new OverviewComputers();
        }

        private void Click_Overview(object sender, RoutedEventArgs e)
        {
            frameComputers.Content = new OverviewComputers();
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            frameComputers.Content = new AddComputers();
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            frameComputers.Content = new EditComputers();
        }
    }
}
