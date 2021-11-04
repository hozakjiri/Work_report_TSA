using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;

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
            stationGrid.ItemsSource = ListOfComputersFunc.LoadListOfComputersTable();
            stationGrid.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}
