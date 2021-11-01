using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;

namespace WorkReportWPF
{
    /// <summary>
    /// Interakční logika pro Support.xaml
    /// </summary>
    public partial class Support : Page
    {
        public Support()
        {
            InitializeComponent();
        }

        public static string File;

        private void Send_Mail(object sender, RoutedEventArgs e)
        {
            File = SupportFunc.GetFileToString();
            SupportFunc.SendSupportMail(Subject_Text.Text, Comment_Text.Text, File, "peter.hajduch@hella.com, jiri.hozak@hella.com");
        }
    }
}
