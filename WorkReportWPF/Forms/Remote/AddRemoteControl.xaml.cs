using System;
using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;

namespace WorkReportWPF.Forms.Remote
{
    /// <summary>
    /// Interaction logic for AddRemoteControl.xaml
    /// </summary>
    public partial class AddRemoteControl : Page
    {
        public AddRemoteControl()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                VncFunc.SaveVNC(cmbProject.Text, txtstation.Text, txthostname.Text, txtdomain.Text, txtuser.Text, txtpass.Text, txtpassvnc.Text);
                MessageBox.Show("Station was added", "Info");
                NULLData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error with save data " + ex.Message, "ERROR");
            }
        }

        public void NULLData()
        {
            cmbProject.ItemsSource = ModificationFunc.LoadProjectList();
            txthostname.Text = "";
            txtdomain.Text = "";
            txtuser.Text = "";
            txtpass.Text = "";
            txtpassvnc.Text = "";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbProject.ItemsSource = ModificationFunc.LoadProjectList();
            }
            catch (Exception)
            {
            }
        }
    }
}
