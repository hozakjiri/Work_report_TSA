using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            samplesGrid.ItemsSource = SamplesFunc.LoadSamplesTable();
            samplesGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void samplesGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var row = e.Row;
            var person = row.DataContext as TableSampleView;

            DateTime Mesiac = DateTime.Now.AddDays(30);
            DateTime TriMesiac = DateTime.Now.AddDays(90);
            DateTime Today = DateTime.Now;


            if (sender == samplesGrid)
            {
                if (person.RevisionValidity >= TriMesiac)
                {
                    row.Background = new SolidColorBrush(Colors.Green);
                }

                else if (person.RevisionValidity < TriMesiac && person.RevisionValidity > Mesiac)
                {
                    row.Background = new SolidColorBrush(Colors.Yellow);
                }

                else if (person.RevisionValidity <= Mesiac && person.RevisionValidity >= Today)
                {
                    row.Background = new SolidColorBrush(Colors.Red);
                }

                else
                {
                    row.Background = new SolidColorBrush(Colors.Gray);
                }

            }

        }
    }
}
