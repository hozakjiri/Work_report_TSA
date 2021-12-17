using System;
using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;

namespace WorkReportWPF.Forms.ListOfComputers
{
    /// <summary>
    /// Interaction logic for AddComputers.xaml
    /// </summary>
    public partial class AddComputers : Page
    {


        public AddComputers()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Can you save data?", "Data", MessageBoxButton.OKCancel);

            if (txtdomain.Text == "" || txthostname.Text == "" || txtnote.Text == "" || txtpass.Text == "" || txtpassvnc.Text == "" || txtstation.Text == "" || txtuser.Text == "")
            {
                MessageBox.Show("Please, fill all data", "Data");
            }
            else if (result == MessageBoxResult.OK)
            {
                ListOfComputersFunc.AddComputers(cmbProject.Text, txtstation.Text, txthostname.Text, txtdomain.Text, txtuser.Text, txtpass.Text, txtpassvnc.Text, (int)(Enums.StatusEnum)cmbType.SelectedItem, datePicker.DisplayDate.ToString("dd.MM.yyyy"), txtnote.Text, cbisVNC.IsChecked.Value);

                MessageBox.Show("Data was added !", "Data");

                OverviewComputers p = new OverviewComputers();
                this.NavigationService.Navigate(p);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cmbType.ItemsSource = Enum.GetValues(typeof(Enums.StationEnum));
            cmbProject.ItemsSource = ModificationFunc.LoadProjectList();
        }
    }
}
