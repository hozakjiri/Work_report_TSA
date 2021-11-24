using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkReportWPF.Functions;
using WorkReportWPF.Models;

namespace WorkReportWPF.Forms.ListOfMasterSamples
{
    /// <summary>
    /// Interaction logic for OverviewSamples.xaml
    /// </summary>
    public partial class OverviewSamples : Page
    {
        public OverviewSamples()
        {
            InitializeComponent();
        }

        private void samplesGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (samplesGrid.SelectedItems.Count > 0)
            {
                TableSampleView data = (TableSampleView)samplesGrid.SelectedItems[0];
                EditSamples p = new EditSamples(data);
                this.NavigationService.Navigate(p);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            samplesGrid.ItemsSource = SamplesFunc.LoadSamplesTable();
            samplesGrid.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}
