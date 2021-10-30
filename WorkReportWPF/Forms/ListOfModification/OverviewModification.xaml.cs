using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;


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
    }
}
