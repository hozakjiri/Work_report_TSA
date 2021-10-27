using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WorkReportWPF.Models;
using WorkReportWPF.Functions;
using WorkReportWPF;
using System.Windows.Input;

namespace Work_Report_1
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // To move the window mouse down
            if (e.ChangedButton == MouseButton.Left)
                DragMove(); 
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void maximizeButton_Click(object sender, RoutedEventArgs e)
        {
            //first detect if windows is in normal state or maximized
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private readonly DbSettingsContext _dbSettings;
        public MainWindow(DbSettingsContext context)
        {
            _dbSettings = context;
        }

        private readonly DbDataContext2 _dbData;
        public MainWindow(DbDataContext2 context)
        {
            _dbData = context;
        }

        public static string UserName, UserLevel, UserMail;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var username = Environment.UserName;
            bool HasPremisions = LoginFunc.CheckPremisions(username);

            if (HasPremisions)
            {

                var user = LoginFunc.LoadUserData(username);
                UserName = user.Name;
                UserLevel = user.Level.ToString();
                UserMail = user.Mail;

                lblUserName.Text = user.Name;
                lblUserLevel.Text = user.Level.ToString();
            }
            else
            {
                MessageBox.Show("Nemáš prístup do aplikácie, aplikácia sa vypína!", "WORK REPORT");
                Close();
            }
        }
    }
}
