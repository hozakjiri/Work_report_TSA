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

        private readonly DbSettingsContext _db;
        public MainWindow(DbSettingsContext context)
        {
            _db = context;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            var username = Environment.UserName;
            bool HasPremisions = LoginFunc.CheckPremisions(username);
            Console.WriteLine(HasPremisions);
            var level = WorkReportWPF.Functions.LoginFunc.LoadUserLevelString(username);
        }
    }
}
