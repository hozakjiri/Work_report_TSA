﻿using System;
using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;
using WorkReportWPF.Models;

namespace WorkReportWPF.Forms.ListOfComputers
{
    /// <summary>
    /// Interaction logic for EditComputers.xaml
    /// </summary>
    public partial class EditComputers : Page
    {
        public ListOfComputersView currentdata = new ListOfComputersView();

        public EditComputers()
        {
            InitializeComponent();
        }

        public EditComputers(ListOfComputersView val) : this()
        {
            currentdata = val;
            this.Loaded += new RoutedEventHandler(Page_Loaded);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cmbType.ItemsSource = Enum.GetValues(typeof(Enums.StationEnum));
            cmbProject.ItemsSource = ModificationFunc.LoadProjectList();

            Station external = new();
            if (currentdata != null)
            {
                txtdomain.Text = currentdata.Domain;
                txthostname.Text = currentdata.HostName;
                txtnote.Text = currentdata.Note;
                txtpass.Text = currentdata.Password;
                txtpassvnc.Text = currentdata.PasswordVnc;
                txtstation.Text = currentdata.Name;
                txtuser.Text = currentdata.User;


                if (cmbProject.Items.Contains(currentdata.Line))
                {
                    cmbProject.SelectedItem = currentdata.Line;
                }

                if (cmbType.Items.Contains(currentdata.Type))
                {
                    cmbType.SelectedItem = currentdata.Type;
                }

                datePicker.SelectedDate = currentdata.Maintenance;

            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Can you save data?", "Data", MessageBoxButton.OKCancel);

            if (txtdomain.Text == "" || txthostname.Text == "" || txtnote.Text == "" || txtpass.Text == "" || txtpassvnc.Text == "" || txtstation.Text == "" || txtuser.Text == "" || cmbType.Text == "")
            {
                MessageBox.Show("Please, fill all data", "Data");
            }
            else if (result == MessageBoxResult.OK)
            {
                ListOfComputersFunc.EditComputers(currentdata.StationID, cmbProject.Text, txtstation.Text, txthostname.Text, txtdomain.Text, txtuser.Text, txtpass.Text, txtpassvnc.Text, (int)(Enums.StatusEnum)cmbType.SelectedItem, datePicker.DisplayDate.ToString("dd.MM.yyyy"), txtnote.Text);

                MessageBox.Show("Data was modified !", "Data");

                OverviewComputers p = new OverviewComputers();
                this.NavigationService.Navigate(p);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (currentdata != null)
            {
                MessageBoxResult result = MessageBox.Show("Can you Delete?", "Data", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    ListOfComputersFunc.DeleteComputers(currentdata.StationID);

                    OverviewComputers p = new OverviewComputers();
                    this.NavigationService.Navigate(p);
                }
            }
        }

    }
}

