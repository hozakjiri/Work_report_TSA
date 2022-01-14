﻿using System;
using System.IO;
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

                SamplesFunc.AddSample(cmbProject.SelectedItem.ToString(), txtName.Text, txtDescription.Text, txtPlacement.Text, cmbResponsible.SelectedItem.ToString(), datePickerRevisionDate.DisplayDate, datePickerRevisionValidity.DisplayDate, txtLabel.Text, txtFolder.Text);

                MessageBox.Show("Data was added !", "Data");

                try
                {
                    var userMail = LoginFunc.LoadUserMail(cmbResponsible.SelectedItem.ToString());
                    var therm = datePickerRevisionValidity.DisplayDate;
                    SamplesFunc.CreateTask(userMail, datePickerRevisionDate.DisplayDate, therm, cmbProject.Text + " " + txtName.Text, "The sample " + txtName + " expires on:" + datePickerRevisionValidity.DisplayDate);
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred while creating the task !", "Alert");
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
                Console.WriteLine(ex.Message);
            }
        }
    }
}
