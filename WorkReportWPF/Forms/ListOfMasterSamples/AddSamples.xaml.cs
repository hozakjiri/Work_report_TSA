using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;

namespace WorkReportWPF.Forms.ListOfMasterSamples
{
    /// <summary>
    /// Interaction logic for AddSamples.xaml
    /// </summary>
    public partial class AddSamples : Page
    {
        public AddSamples()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Can you save data?", "Data", MessageBoxButton.OKCancel);

            if (txtDescription.Text == "" || txtName.Text == "" || txtPlacement.Text == "" || cmbProject.Text == "" || cmbResponsible.Text == "" || datePickerRevisionDate.Text == "" || datePickerRevisionValidity.Text == "")
            {
                MessageBox.Show("Please, fill all data", "Data");
            }
            else if (result == MessageBoxResult.OK)
            {
                SamplesFunc.AddSample(cmbProject.SelectedItem.ToString(), txtName.Text, txtDescription.Text, txtPlacement.Text, cmbResponsible.SelectedItem.ToString(), datePickerRevisionDate.DisplayDate, datePickerRevisionValidity.DisplayDate);

                MessageBox.Show("Data was added !", "Data");

                OverviewSamples p = new OverviewSamples();
                this.NavigationService.Navigate(p);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cmbProject.ItemsSource = ModificationFunc.LoadProjectList();
            cmbResponsible.ItemsSource = LoginFunc.LoadAllUsersList();
        }
    }
}
