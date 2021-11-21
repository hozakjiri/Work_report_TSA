using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WorkReportWPF.Functions;

namespace WorkReportWPF
{
    /// <summary>
    /// Interakční logika pro MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Page
    {
        public MainScreen()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Date_Text.Text = DateTime.Now.ToShortDateString();
            Day_Text.Text = DateTime.Now.DayOfWeek.ToString();
            Time_Text.Text = DateTime.Now.ToShortTimeString();
            Week_Text.Text = GetWeekNumber(DateTime.Now).ToString();
        }

        static int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }

        static int GetWeekNumber(DateTime date)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weeknum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weeknum;
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

            Date_Text.Text = DateTime.Now.ToShortDateString();
            Day_Text.Text = DateTime.Now.DayOfWeek.ToString();
            Time_Text.Text = DateTime.Now.ToShortTimeString();
            Week_Text.Text = GetWeekNumber(DateTime.Now).ToString();

            try
            {
                TaskGrid.ItemsSource = TaskFunc.LoadTaskTableTOP();
                TaskGrid.Columns[0].Visibility = Visibility.Hidden;

                ModificationGrid.ItemsSource = ModificationFunc.LoadModificationTableTOP();
                ModificationGrid.Columns[0].Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
