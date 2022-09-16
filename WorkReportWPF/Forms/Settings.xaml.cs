using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkReportWPF.Functions;
using WorkReportWPF.Models;
using WorkReportWPF.Models.DBModels;

namespace WorkReportWPF
{
    /// <summary>
    /// Interakční logika pro Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //btnDMember.Visibility = Visibility.Hidden;
            RefrashMembersTable();
            btnDProject.Visibility = Visibility.Hidden;
            btnEProject.Visibility = Visibility.Hidden;
            RefrashProjectsTable();
            cmbRights.ItemsSource = Enum.GetValues(typeof(Enums.LevelEnum));
            btnDUser.Visibility = Visibility.Hidden;
            btnEUser.Visibility = Visibility.Hidden;
            RefrashUsersTable();
        }

        private void BtnAMembers_Click(object sender, RoutedEventArgs e)
        {
            //MemberFunc.SaveMembers(txtMembers.Text);
            //txtMembers.Text = "";
            //RefrashMembersTable();
        }

        private void btnDMember_Click(object sender, RoutedEventArgs e)
        {

            //if (membersGrid.SelectedItems != null && membersGrid.SelectedItems.Count > 0)
            //{
            //    Member member = new();

            //    foreach (var obj in membersGrid.SelectedItems)
            //    {
            //        member = obj as Member;
            //        MemberFunc.DeleteMember(member.ID);
            //        txtMembers.Text = "";
            //    }

            //    RefrashMembersTable();
            //}
        }

        public void RefrashMembersTable()
        {
            //membersGrid.ItemsSource = MemberFunc.LoadMembersData();
            //membersGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        public void RefrashProjectsTable()
        {
            projectGrid.ItemsSource = ProjectFunc.LoadProjectsData();
            projectGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        public void RefrashUsersTable()
        {
            userGrid.ItemsSource = LoginFunc.LoadAllUsers();
            userGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void btnDProject_Click(object sender, RoutedEventArgs e)
        {
            if (projectGrid.SelectedItems != null && projectGrid.SelectedItems.Count > 0)
            {
                Project project = new();

                foreach (var obj in projectGrid.SelectedItems)
                {
                    try
                    {
                        project = obj as Project;
                        ProjectFunc.DeleteProject(project.ID);
                        txtProject.Text = "";
                    }
                    catch (Exception)
                    {
                    }
                }

                RefrashProjectsTable();
            }
        }

        private void btnEProject_Click(object sender, RoutedEventArgs e)
        {
            if (projectGrid.SelectedItems != null && projectGrid.SelectedItems.Count > 0)
            {
                Project project = new();

                foreach (var obj in projectGrid.SelectedItems)
                {
                    project = obj as Project;
                    project.Name = txtProject.Text;
                    ProjectFunc.EditProject(project);
                    txtProject.Text = "";
                }

                RefrashProjectsTable();
            }
        }

        private void btnAProject_Click(object sender, RoutedEventArgs e)
        {
            ProjectFunc.SaveProject(txtProject.Text);
            txtProject.Text = "";
            RefrashProjectsTable();
        }

        private void membersGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (membersGrid.SelectedItems.Count > 0)
            //{
            //    btnDMember.Visibility = Visibility.Visible;
            //    Member rowdata = (Member)membersGrid.SelectedItems[0];
            //    txtMembers.Text = rowdata.Mail;
            //}
            //else
            //{
            //    btnDMember.Visibility = Visibility.Hidden;
            //}
        }

        private void projectGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (projectGrid.SelectedItems.Count > 0)
            {
                btnDProject.Visibility = Visibility.Visible;
                btnEProject.Visibility = Visibility.Visible;

                Project rowdata = (Project)projectGrid.SelectedItems[0];
                txtProject.Text = rowdata.Name;
            }
            else
            {
                btnDProject.Visibility = Visibility.Hidden;
                btnEProject.Visibility = Visibility.Hidden;
            }
        }

        private void btnAUser_Click(object sender, RoutedEventArgs e)
        {
            Login userData = new()
            {
                UserLogin = txtLogin.Text,
                Mail = txtMail.Text,
                Level = (Enums.LevelEnum)cmbRights.Items.CurrentItem,
                Name = txtName.Text,
            };

            LoginFunc.SaveUser(userData);
            txtLogin.Text = "";
            txtMail.Text = "";
            txtName.Text = "";
            RefrashUsersTable();
        }

        private void btnEUser_Click(object sender, RoutedEventArgs e)
        {
            if (userGrid.SelectedItems != null && userGrid.SelectedItems.Count > 0)
            {
                Login user = new();

                foreach (var obj in userGrid.SelectedItems)
                {
                    user = obj as Login;
                    user.Name = txtName.Text;
                    user.Mail = txtMail.Text;
                    user.UserLogin = txtLogin.Text;
                    user.Level = (Enums.LevelEnum)cmbRights.SelectedItem;
                    LoginFunc.EditUser(user);
                }

                txtLogin.Text = "";
                txtMail.Text = "";
                txtName.Text = "";
                RefrashUsersTable();
            }
        }

        private void btnDUser_Click(object sender, RoutedEventArgs e)
        {
            if (userGrid.SelectedItems != null && userGrid.SelectedItems.Count > 0)
            {
                Login user = new();

                foreach (var obj in userGrid.SelectedItems)
                {
                    try
                    {
                        user = obj as Login;
                        LoginFunc.DeleteUser(user.UserID);
                    }
                    catch (Exception)
                    {
                    }
                }
                txtLogin.Text = "";
                txtMail.Text = "";
                txtName.Text = "";
                RefrashUsersTable();
            }
        }

        private void userGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (userGrid.SelectedItems.Count > 0)
            {
                btnDUser.Visibility = Visibility.Visible;
                btnEUser.Visibility = Visibility.Visible;

                Login rowdata = (Login)userGrid.SelectedItems[0];
                txtName.Text = rowdata.Name;
                txtLogin.Text = rowdata.UserLogin;
                txtMail.Text = rowdata.Mail;
                Enums.LevelEnum level = rowdata.Level;
                cmbRights.Text = level.ToString();
            }
            else
            {
                btnDUser.Visibility = Visibility.Hidden;
                btnEUser.Visibility = Visibility.Hidden;
            }
        }

        private void userGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
