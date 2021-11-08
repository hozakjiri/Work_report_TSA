using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;

namespace WorkReportWPF.Forms.ListOfModification
{
    /// <summary>
    /// Interaction logic for SendModification.xaml
    /// </summary>
    public partial class SendModification : Page
    {
        public SendModification()
        {
            InitializeComponent();
        }

        private void Send_Modification_click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var mails = LoginFunc.LoadAllMails();
                var data = ModificationFunc.LoadUserModificationTable();
                ModificationFunc.SendMail(data, Shift, mails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "EROOR");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var data = ModificationFunc.LoadUserModificationTable();
            sendGrid.ItemsSource = data;
            sendGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        public string Shift;
        public int nocna;

        public void Smena()
        {
            DateTime Time = Conversions.ToDate(DateTime.Now.ToLocalTime().ToLongTimeString());
            var Time0 = DateTime.Parse("6:30:00");
            var Time1 = DateTime.Parse("14:30:00");
            var Time2 = DateTime.Parse("22:30:00");
            var Time3 = DateTime.Parse("00:00:00");
            if (Time0.TimeOfDay < Time.TimeOfDay & Time.TimeOfDay < Time1.TimeOfDay)
            {
                Shift = "Ranná";
            }
            else if (Time1.TimeOfDay < Time.TimeOfDay & Time.TimeOfDay < Time2.TimeOfDay)
            {
                Shift = "Poobedná";
            }
            else if (Time.TimeOfDay > Time2.TimeOfDay)
            {
                Shift = "Nočná";
            }
            else if (Time.TimeOfDay < Time0.TimeOfDay)
            {
                Shift = "Nočná";
            }
            else
            {
            }
        }

    }

}
