using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Windows.ApplicationModel.Appointments;
using WorkReportWPF.Functions;
using System.Net.Mail;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;
using MaterialDesignThemes.Wpf;

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
            Guid guid = Guid.NewGuid();

            MessageBoxResult result = MessageBox.Show("Chcete uložit data?", "Data", MessageBoxButton.OKCancel);

            if ((DateTime)datePickerRevisionValidity.SelectedDate < (DateTime)datePickerRevisionDate.SelectedDate)
            {
                MessageBox.Show("Špatná data, vložte prosím všechna data", "Data");
                return;
            }


            if (txtDescription.Text == "" || txtName.Text == "" || txtPlacement.Text == "" || cmbProject.Text == "" || cmbResponsible.Text == "" || datePickerRevisionDate.Text == "" || datePickerRevisionValidity.Text == "")
            {
                MessageBox.Show("Prosím, vyplňte všechna pole", "Data");
            }
            else if (result == MessageBoxResult.OK)
            {

                SamplesFunc.AddSample(cmbProject.SelectedItem.ToString(), txtName.Text, txtDescription.Text, txtPlacement.Text, cmbResponsible.SelectedItem.ToString(), (DateTime)datePickerRevisionDate.SelectedDate, (DateTime)datePickerRevisionValidity.SelectedDate, txtLabel.Text, txtFolder.Text, guid);
                //SamplesFunc.AddAppointment(userMail, (DateTime)datePickerRevisionDate.SelectedDate, therm, cmbProject.Text + " " + txtName.Text, "The sample " + txtName + " expires on: " + therm);
                //SamplesFunc.SendMail(cmbProject.SelectedItem.ToString(), txtName.Text, txtDescription.Text, txtPlacement.Text, cmbResponsible.SelectedItem.ToString(), (DateTime)datePickerRevisionDate.SelectedDate, (DateTime)datePickerRevisionValidity.SelectedDate, txtLabel.Text, txtFolder.Text, guid);              
                MessageBox.Show("Data byla přidána !", "Data");

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
                    MessageBox.Show("Při vytvoření úlohy došlo k chybě !", "Alert");
                }

                OverviewSamples p = new OverviewSamples();
                this.NavigationService.Navigate(p);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cmbProject.ItemsSource = ModificationFunc.LoadProjectList();
            cmbResponsible.ItemsSource = LoginFunc.LoadAllUsersList();
        }

        private void btnFolder_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbProject.Text) && !string.IsNullOrEmpty(txtName.Text))
            {
                var name = OtherFunc.getReplaceString(txtName.Text, "/", "-", "\\", ".");

                var project = OtherFunc.getReplaceString(cmbProject.Text, "/", "-", "\\", ".");

                if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Samples"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Samples");
                }

                if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Samples/" + project))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Samples/" + project);
                }

                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Samples/" + project + "/" + name))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Samples/" + project + "/" + name);
                    }
                }





                txtFolder.Text = Directory.GetCurrentDirectory() + "/Samples/" + project + "/" + name + "/";

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
                            File.Copy(item, Directory.GetCurrentDirectory() + "/Samples/" + project + "/" + name + "/" + filename[filename.Length - 1], true);
                        }

                    }

                }

            }
            else
            {
                MessageBox.Show("Prosím vyplňte všechna pole", "Alert");
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
                Console.WriteLine(ex.Message);
            }
        }
    }
}

