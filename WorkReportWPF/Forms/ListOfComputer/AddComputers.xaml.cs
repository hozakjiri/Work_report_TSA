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

            if (txtdomain.Text == "" || txthostname.Text == "" || txtnote.Text == "" || txtpass.Text == "" || txtpassvnc.Text == "" || txtstation.Text == "" || txtuser.Text == "" || cmbType.SelectedItem == null || cmbProject.SelectedItem == null)
            {
                MessageBox.Show("Please, fill all data", "Data");
            }
            else if (result == MessageBoxResult.OK)
            {
                //ListOfComputersFunc.AddComputers(cmbProject.Text, txtstation.Text, txthostname.Text, txtdomain.Text, txtuser.Text, txtpass.Text, txtpassvnc.Text, (int)(Enums.StatusEnum)cmbType.SelectedItem, ((DateTime)datePicker.SelectedDate).ToString("dd.MM.yyyy"), txtnote.Text, cbisVNC.IsChecked.Value);
                ListOfComputersFunc.AddComputers(cmbProject.Text, txtstation.Text, txthostname.Text, txtdomain.Text, txtuser.Text, txtpass.Text, txtpassvnc.Text, (int)(Enums.StationEnum)cmbType.SelectedItem, txtnote.Text, cbisVNC.IsChecked.Value, ((DateTime)datePickerRevisionDate.SelectedDate).ToString("dd.MM.yyyy"), ((DateTime)datePickerRevisionValidity.SelectedDate).ToString("dd.MM.yyyy"));

                MessageBox.Show("Data was added !", "Data");

                try
                {
                    var userMail = LoginFunc.LoadUserMailByUserName(Environment.UserName);
                    //var therm = (DateTime)datePicker.SelectedDate;
                    var therm = (DateTime)datePickerRevisionValidity.SelectedDate;
                    ListOfComputersFunc.CreateTask(userMail, DateTime.Now, therm, txtstation.Text + " " + txthostname.Text, "Computer maintenance is required : " + txtstation.Text );
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred while creating the task !", "Alert");
                }
                

                OverviewComputers p = new OverviewComputers();
                this.NavigationService.Navigate(p);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cmbType.ItemsSource = Enum.GetValues(typeof(Enums.StationEnum));
            cmbProject.ItemsSource = ModificationFunc.LoadProjectList();
            cmbType.SelectedIndex = 0;
            cmbProject.SelectedIndex = 0;
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
