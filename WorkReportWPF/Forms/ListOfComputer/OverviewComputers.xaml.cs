using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;

namespace WorkReportWPF.Forms.ListOfComputers
{
    /// <summary>
    /// Interaction logic for OverviewComputers.xaml
    /// </summary>
    public partial class OverviewComputers : Page
    {
        public OverviewComputers()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            stationGrid.ItemsSource = ListOfComputersFunc.LoadListOfComputersTable();
            stationGrid.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}
