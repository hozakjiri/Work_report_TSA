using System.Windows;
using System;
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

        public static string File = "";

        private void Send_Email_button_Click(object sender, RoutedEventArgs e)
        {
            if (Subject_Text.Text == "" || Comment_Text.Text == "")
            {
                MessageBox.Show("Please fill all data !", "Info");
            }
            else { 
            try
            {
                SupportFunc.SendSupportMail(Subject_Text.Text, Comment_Text.Text, File, "peter.hajduch@hella.com, jiri.hozak@hella.com");
            } 
            catch (System.Exception ex)
            {
                MessageBox.Show("Problem with sending message : " + ex.Message, "Alert");
            }
            }
        }

        private void Insert_Image_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File = SupportFunc.GetFileToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Problem with selected file : " + ex.Message, "Alert");
            }
        }
    }
}
