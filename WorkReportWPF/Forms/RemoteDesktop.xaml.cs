using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Forms.Remote;

namespace WorkReportWPF
{
    /// <summary>
    /// Interakční logika pro RemoteDesktop.xaml
    /// </summary>
    public partial class RemoteDesktop : Page
    {
        public RemoteDesktop()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            btnBack.Visibility = Visibility.Hidden;
            frameRemote.Content = new RemoteControl();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frameRemote.Content = new AddRemoteControl();
            btnBack.Visibility = Visibility.Visible;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            frameRemote.Content = new RemoteControl();
            btnBack.Visibility = Visibility.Hidden;
        }
    }


}

