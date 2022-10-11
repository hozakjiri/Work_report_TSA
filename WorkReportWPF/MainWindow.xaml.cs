using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WorkReportWPF.Functions;

namespace WorkReportWPF
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            CheckTask();
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

        private readonly DbDataContext _dbData;
        public MainWindow(DbDataContext context)
        {
            _dbData = context;
        }

        public static string UserName, UserLevel, UserMail;

        private void Click_Settings(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Settings();
        }

        private void Click_Home(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new MainScreen();
        }

        private void Click_ListOfComputers(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ListOfComputers();
        }

        private void Click_RemoteDesktop(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new RemoteDesktop();
        }

        private void Click_ListOfModifications(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new LIstOfModifications();
        }

        private void Click_Calendar(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new WorkReportWPF.Calendar();
        }

        private void Click_ListOfMasterSamples(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ListOfMasterSamples();
        }

        private void Click_ListOfTasks(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ListOfTasks();
        }

        private void Click_Support(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Support();
        }

        private void TaskBell_Click(object sender, RoutedEventArgs e)
        {
            int MyTask = 1;
            MainContent.Content = new ListOfTasks(MyTask);
        }

        public void CheckTask()
        {
            //var countTasks = TaskFunc.LoadMyTask();
            //txtMyTask.Text = countTasks.ToString();

            //if (countTasks > 0)
            //{
            //    ElipseMyTask.Fill = System.Windows.Media.Brushes.Red;
            //}
            //else
            //{
            //    ElipseMyTask.Fill = System.Windows.Media.Brushes.Green;

            //}
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var username = Environment.UserName;
            bool HasPremisions = LoginFunc.CheckPremisions(username);

            CheckTask();

            if (HasPremisions)
            {
                var user = LoginFunc.LoadUserData(username);
                UserName = user.Name;
                UserLevel = user.Level.ToString();
                UserMail = user.Mail;

                if (user.Level == Enums.LevelEnum.Admin)
                {
                    Settings_button.Visibility = Visibility.Visible;
                }

                lblUserName.Text = user.Name;
                lblUserLevel.Text = user.Level.ToString();

                MainContent.Content = new MainScreen();
            }
            else
            {
                MessageBox.Show("Nemáte přístup k aplikaci, požádejte Admin správnce o přístup. Aplikace se ukončuje!", "WORK REPORT");
                Close();
            }
        }

    }
}
