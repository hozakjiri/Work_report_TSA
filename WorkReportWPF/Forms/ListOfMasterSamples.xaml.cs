using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Forms.ListOfMasterSamples;

namespace WorkReportWPF
{
    /// <summary>
    /// Interakční logika pro ListOfMasterSamples.xaml
    /// </summary>
    public partial class ListOfMasterSamples : Page
    {
        public ListOfMasterSamples()
        {
            InitializeComponent();
        }

        private void Overview_button_Click(object sender, RoutedEventArgs e)
        {
            frameSamples.Content = new OverviewSamples();
        }

        private void Add_button_Click(object sender, RoutedEventArgs e)
        {
            frameSamples.Content = new AddSamples();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            frameSamples.Content = new OverviewSamples();
        }
    }
}
