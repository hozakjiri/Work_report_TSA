using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;
using WorkReportWPF.Models;
using WorkReportWPF.Models.DBModels;

namespace WorkReportWPF.Forms.ListOfMasterSamples
{
    /// <summary>
    /// Interaction logic for EditSamples.xaml
    /// </summary>
    public partial class EditSamples : Page
    {
        public TableSampleView currentdata = new TableSampleView();

        public EditSamples()
        {
            InitializeComponent();
        }

        public EditSamples(TableSampleView val) : this()
        {
            currentdata = val;
            this.Loaded += new RoutedEventHandler(Page_Loaded);

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Can you edit data?", "Data", MessageBoxButton.OKCancel);

            if (txtDescription.Text == "" || txtName.Text == "" || txtPlacement.Text == "" || cmbProject.Text == "" || cmbResponsible.Text == "" || datePickerRevisionDate.Text == "" || datePickerRevisionValidity.Text == "")
            {
                MessageBox.Show("Please, fill all data", "Data");
            }
            else if (result == MessageBoxResult.OK)
            {
                SamplesFunc.EditSample(currentdata.ID, cmbProject.SelectedItem.ToString(), txtName.Text, txtDescription.Text, txtPlacement.Text, cmbResponsible.SelectedItem.ToString(), datePickerRevisionDate.DisplayDate, datePickerRevisionValidity.DisplayDate);

                MessageBox.Show("Data was edited !", "Data");

                OverviewSamples p = new OverviewSamples();
                this.NavigationService.Navigate(p);
            }
        }

        private void btnPrint_click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cmbProject.ItemsSource = ModificationFunc.LoadProjectList();
            cmbResponsible.ItemsSource = LoginFunc.LoadAllUsersList();

            Sample external = new();
            if (currentdata != null)
            {
                txtDescription.Text = currentdata.Description;
                txtName.Text = currentdata.Name;
                txtPlacement.Text = currentdata.Placement;

                if (cmbProject.Items.Contains(currentdata.Project))
                {
                    cmbProject.SelectedItem = currentdata.Project;
                }

                if (cmbResponsible.Items.Contains(currentdata.Responsible))
                {
                    cmbResponsible.SelectedItem = currentdata.Responsible;
                }

                datePickerRevisionDate.SelectedDate = currentdata.RevisionDate;
                datePickerRevisionValidity.SelectedDate = currentdata.RevisionValidity;

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Can you edit data?", "Data", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                SamplesFunc.DeleteSample(currentdata.ID);

                MessageBox.Show("Data was deleted !", "Data");

                OverviewSamples p = new OverviewSamples();
                this.NavigationService.Navigate(p);
            }
        }
    }
}
