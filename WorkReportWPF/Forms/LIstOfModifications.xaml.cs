using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Forms.ListOfModification;

namespace WorkReportWPF
{
    /// <summary>
    /// Interakční logika pro LIstOfModifications.xaml
    /// </summary>
    public partial class LIstOfModifications : Page
    {
        public LIstOfModifications()
        {
            InitializeComponent();
        }

        private void Click_Overview(object sender, RoutedEventArgs e)
        {
            frameModification.Content = new OverviewModification();
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            frameModification.Content = new AddModification();
        }

        private void Click_Send(object sender, RoutedEventArgs e)
        {
            frameModification.Content = new SendModification();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            frameModification.Content = new OverviewModification();
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            frameModification.Content = new EditModification();
        }
    }
}