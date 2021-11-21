using System;
using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;

namespace WorkReportWPF.Forms.ListOfTask
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Page
    {
        public AddTask()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var user = OtherFunc.user;
            datePicker.DisplayDate = DateTime.Now;
            datePicker.SelectedDate = DateTime.Now;

            cmbPriority.ItemsSource = Enum.GetValues(typeof(Enums.TaskEnum));
            cmbUser.ItemsSource = LoginFunc.LoadAllUsersList();
        }

        private void Save_Task_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Can you save data?", "Data", MessageBoxButton.OKCancel);

            if (txtDescription.Text == "" || txtTopic.Text == "" || cmbPriority.Text == "" || cmbUser.Text == "")
            {
                MessageBox.Show("Please, fill all data", "Data");
            }
            else if (result == MessageBoxResult.OK)
            {
                TaskFunc.AddTaskNew(txtTopic.Text, txtDescription.Text, (int)(Enums.StatusEnum)cmbPriority.SelectedItem, OtherFunc.user, cmbUser.SelectedItem.ToString(), datePicker.DisplayDate);

                MessageBox.Show("Data was added !", "Data");

                OverviewTask p = new OverviewTask();
                this.NavigationService.Navigate(p);
            }
        }

        private void Delete_Task_Click(object sender, RoutedEventArgs e)
        {
            OverviewTask p = new OverviewTask();
            this.NavigationService.Navigate(p);
        }
    }
}
