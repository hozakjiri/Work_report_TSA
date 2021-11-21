using System;
using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;
using WorkReportWPF.Models;

namespace WorkReportWPF.Forms.ListOfTask
{
    /// <summary>
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class EditTask : Page
    {

        public TableTaskView currentdata = new TableTaskView();

        public EditTask()
        {
            InitializeComponent();
        }


        public EditTask(TableTaskView val) : this()
        {
            currentdata = val;
            this.Loaded += new RoutedEventHandler(Page_Loaded);
        }

        private void Save_Task_button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Can you save data?", "Data", MessageBoxButton.OKCancel);

            if (txtDescription.Text == "" || txtNote.Text == "" || txtTopic.Text == "")
            {
                MessageBox.Show("Please, fill all data", "Data");
            }
            else if (result == MessageBoxResult.OK)
            {
                TaskFunc.EditTask(currentdata.ID, txtTopic.Text, txtDescription.Text, (int)(Enums.TaskEnum)cmbPriority.SelectedItem, (int)(Enums.StatusEnum)cmbStatus.SelectedItem, OtherFunc.user, cmbUser.SelectedItem.ToString(), datePicker.DisplayDate, txtNote.Text);

                MessageBox.Show("Task was added !", "Data");

                ListOfTasks p = new ListOfTasks();
                this.NavigationService.Navigate(p);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TableTaskView external = new TableTaskView();

            cmbPriority.ItemsSource = Enum.GetValues(typeof(Enums.TaskEnum));
            cmbStatus.ItemsSource = Enum.GetValues(typeof(Enums.StatusEnum));

            cmbUser.ItemsSource = LoginFunc.LoadAllUsersList();

            if (currentdata != null)
            {
                txtTopic.Text = currentdata.Subject;
                txtDescription.Text = currentdata.Description;
                txtNote.Text = currentdata.Note;

                if (cmbUser.Items.Contains(currentdata.Recipient))
                {
                    cmbUser.SelectedItem = currentdata.Recipient;
                }

                if (cmbStatus.Items.Contains(currentdata.Status))
                {
                    cmbStatus.SelectedItem = currentdata.Status;
                }

                if (cmbPriority.Items.Contains(currentdata.Priority))
                {
                    cmbPriority.SelectedItem = currentdata.Priority;
                }

                datePicker.SelectedDate = currentdata.Term;


            }
        }
    }
}
