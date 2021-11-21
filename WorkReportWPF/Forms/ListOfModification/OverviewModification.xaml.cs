using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;
using WorkReportWPF.Models;

namespace WorkReportWPF.Forms.ListOfModification
{
    /// <summary>
    /// Interaction logic for OverviewModification.xaml
    /// </summary>
    public partial class OverviewModification : Page
    {
        public OverviewModification()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = ModificationFunc.LoadModificationTable();
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void dataGrid_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                TableModificationView data = (TableModificationView)dataGrid.SelectedItems[0];
                EditModification p = new EditModification(data);
                this.NavigationService.Navigate(p);
            }
        }
    }
}
