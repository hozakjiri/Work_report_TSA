using MaterialDesignThemes.Wpf;
using System;
using System.IO;
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
        public string lblGuid = "";

        public EditSamples()
        {
            InitializeComponent();
        }

        public EditSamples(TableSampleView val) : this()
        {
            currentdata = val;
            this.Loaded += new RoutedEventHandler(Page_Loaded);

        }

        //private int _numValue = 0;

        //public int NumValue
        //{
        //    get { return _numValue; }
        //    set
        //    {
        //        _numValue = value;
        //        txtNum.Text = value.ToString();
        //    }
        //}

        //public void NumberUpDown()
        //{
        //    InitializeComponent();
        //    txtNum.Text = _numValue.ToString();
        //}

        //private void cmdUp_Click(object sender, RoutedEventArgs e)
        //{
        //    NumValue += 1;
        //}

        //private void cmdDown_Click(object sender, RoutedEventArgs e)
        //{
        //    if (_numValue == 0)
        //    {
        //        NumValue = 0;
        //    }
        //    else
        //    {
        //        NumValue -= 1;
        //    }
        //}

        public DateTime DisplayDate { get; set; }

        //private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (txtNum == null)
        //    {
        //        return;
        //    }

        //    if (!int.TryParse(txtNum.Text, out _numValue))
        //        txtNum.Text = _numValue.ToString();
        //}

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {


            MessageBoxResult result = MessageBox.Show("Opravdu chcete upravit data?", "Data", MessageBoxButton.OKCancel);
            
            if (txtDescription.Text == ""  && txtName.Text == ""  && txtPlacement.Text == "" &&  cmbProject.Text == "" && cmbResponsible.Text == "" && datePickerRevisionDate.Text == "" && datePickerRevisionValidity.Text == "")
            {
                MessageBox.Show("Prosím, vyplňte všechna data", "Data");
            }
            else if (result == MessageBoxResult.OK)
            {
                SamplesFunc.EditSample(currentdata.ID, cmbProject.SelectedItem.ToString(), txtName.Text, txtDescription.Text, txtPlacement.Text, cmbResponsible.SelectedItem.ToString(), datePickerRevisionDate.DisplayDate, datePickerRevisionValidity.DisplayDate, txtLabel.Text, txtFolder.Text);
                //SamplesFunc.SendMail(cmbProject.SelectedItem.ToString(), txtName.Text, txtDescription.Text, txtPlacement.Text, cmbResponsible.SelectedItem.ToString(), (DateTime)datePickerRevisionDate.SelectedDate, (DateTime)datePickerRevisionValidity.SelectedDate, txtLabel.Text, txtFolder.Text);
                MessageBox.Show("Data byla upravena !", "Data");

                OverviewSamples p = new OverviewSamples();
                this.NavigationService.Navigate(p);

                try
                {
                    var userMail = LoginFunc.LoadUserMail(cmbResponsible.SelectedItem.ToString());
                    var therm = (DateTime)datePickerRevisionValidity.SelectedDate;
                    SamplesFunc.AddAppointment(userMail, (DateTime)datePickerRevisionDate.SelectedDate, therm, cmbProject.Text + " " + txtName.Text, "The sample " + txtName + " expires on: " + therm);

                    //SamplesFunc.Test(cmbProject.SelectedItem.ToString(), txtName.Text, txtDescription.Text, txtPlacement.Text, cmbResponsible.SelectedItem.ToString(), (DateTime)datePickerRevisionDate.SelectedDate, (DateTime)datePickerRevisionValidity.SelectedDate, txtLabel.Text, txtFolder.Text, guid);
                    //SamplesFunc.CreateTask(userMail, (DateTime)datePickerRevisionDate.SelectedDate, therm, cmbProject.Text + " " + txtName.Text, "The sample " + txtName + " expires on: " + therm);
                }
                catch (Exception)
                {
                    MessageBox.Show("Při vytváření úlohy došlo k chybě !", "Alert");
                }
            }
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
                txtFolder.Text = currentdata.Folder;
                txtLabel.Text = currentdata.Label;
                lblGuid = currentdata.Guid;

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
            MessageBoxResult result = MessageBox.Show("Opravdu chcete odstranit data ?", "Data", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                SamplesFunc.DeleteSample(currentdata.ID);

                MessageBox.Show("Data byla smazána !", "Data");

                OverviewSamples p = new OverviewSamples();
                this.NavigationService.Navigate(p);
            }
        }

        private void btnFolder_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbProject.Text))
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Samples"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Samples");
                }

                if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Samples/" + cmbProject.Text))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Samples/" + cmbProject.Text);
                }

                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Samples/" + cmbProject.Text + "/" + txtName.Text))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Samples/" + cmbProject.Text + "/" + txtName.Text);
                    }
                }

                txtFolder.Text = Directory.GetCurrentDirectory() + "/Samples/" + cmbProject.Text + "/" + txtName.Text + "/";

                Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
                openFileDlg.Multiselect = true;
                openFileDlg.Filter = "All files (*.*)|*.*";
                openFileDlg.InitialDirectory = @"C:\";

                Nullable<bool> result = openFileDlg.ShowDialog();


                if (result == true)
                {
                    foreach (var item in openFileDlg.FileNames)
                    {
                        string[] filename = item.Split("\\");
                        if (filename.Length > 0)
                        {
                            File.Copy(item, Directory.GetCurrentDirectory() + "/Samples/" + cmbProject.Text + "/" + txtName.Text + "/" + filename[filename.Length - 1], true);
                        }

                    }

                }

            }
        }

        private void btnFolder_Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var path = txtFolder.Text;

                if (!Directory.Exists(path) && path == "")
                    path = "C:\\";

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = path,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba adresáře ! " + ex.Message, "Alert");
            }
        }
    }
}
