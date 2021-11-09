using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;
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
            btnDMember.Visibility = Visibility.Hidden;
            RefrashMembersTable();

        }

        private void BtnAMembers_Click(object sender, RoutedEventArgs e)
        {
            SettingsFunc.SaveMembers(txtMembers.Text);
            RefrashMembersTable();
        }

        private void btnDMember_Click(object sender, RoutedEventArgs e)
        {

            if (membersGrid.SelectedItems != null && membersGrid.SelectedItems.Count > 0)
            {
                Member member = new();

                foreach (var obj in membersGrid.SelectedItems)
                {
                    member = obj as Member;
                    SettingsFunc.DeleteMember(member.ID);
                }

                RefrashMembersTable();
            }
        }
        public void RefrashMembersTable()
        {
            membersGrid.ItemsSource = SettingsFunc.LoadMembersData();
            membersGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void membersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (membersGrid.SelectedItems.Count > 0)
            {
                btnDMember.Visibility = Visibility.Visible;

            }
            else
            {
                btnDMember.Visibility = Visibility.Hidden;
            }
        }

        private void membersGrid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (membersGrid.SelectedItems.Count > 0)
            {
                btnDMember.Visibility = Visibility.Visible;

            }
            else
            {
                btnDMember.Visibility = Visibility.Hidden;
            }
        }
    }
}
