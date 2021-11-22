using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;
using WorkReportWPF.Models;

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

        private void stationGrid_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (stationGrid.SelectedItems.Count > 0)
            {
                ListOfComputersView data = (ListOfComputersView)stationGrid.SelectedItems[0];
                EditComputers p = new EditComputers(data);
                this.NavigationService.Navigate(p);
            }
        }
    }
}
