using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WorkReportWPF.Functions;
using WorkReportWPF.Models;

namespace WorkReportWPF
{
    /// <summary>
    /// Interakční logika pro RemoteDesktop.xaml
    /// </summary>
    public partial class RemoteDesktop : Page
    {
        public RemoteDesktop()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Station> allMenu = new List<Station>();
            allMenu = VncFunc.GetAllStations();

            var newSource = allMenu.Select(a => new { a.Line }).Distinct().ToList();

            // Get every logical drive on the machine
            foreach (var node in newSource)
            {
                // Create a new item for it
                var item = new TreeViewItem()
                {
                    // Set the header
                    Header = node.Line,
                    // And the full path
                    Tag = node.Line
                };

                // Add a dummy item
                item.Items.Add(null);

                // Listen out for item being expanded
                item.Expanded += Name_Expanded;

                // Add it to the main tree-view
                stationView.Items.Add(item);
            }
        }

        private void Name_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;

            // If the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            // Clear dummy data
            item.Items.Clear();

            // Get full path
            var currentLine = (string)item.Tag;

            // Create a blank list for directories
            List<string> lines = new List<string>();

            // Try and get directories from the folder
            // ignoring any issues doing so
            try
            {
                List<Station> ListStations = new List<Station>();
                ListStations = VncFunc.GetAllStations();

                //var nodeStation = ListStations.Where(a => a.Line.Contains((string)item.Tag)).Select(a => new { Name = (string)a.Name }).ToList();
                var nodeStation = ListStations.Where(a => a.Line.Contains(currentLine)).ToList();

                foreach (string name in nodeStation.Select(a => a.Name))
                {
                    lines.Add(name);
                }

            }
            catch
            {
            }

            // For each directory...
            lines.ForEach(LineName =>
            {
                // Create directory item
                var subItem = new TreeViewItem()
                {
                    // Set header as folder name
                    Header = LineName,
                    // And tag as full path
                    Tag = LineName
                };

                // Add dummy item so we can expand folder
                subItem.Items.Add(null);

                // Handle expanding
                subItem.Expanded += HostName_Expanded;

                // Add this item to the parent
                item.Items.Add(subItem);
            });
        }

        private void HostName_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;

            // Clear dummy data
            item.Items.Clear();

            // Get full path
            var currentName = (string)item.Tag;

            var hostname = new List<string>();

            // Try and get files from the folder
            // ignoring any issues doing so
            try
            {
                List<Station> ListStations = new List<Station>();
                ListStations = VncFunc.GetAllStations();

                var nodeStation = ListStations.Where(a => a.Name.Contains((string)item.Tag)).Select(a => new { HostName = (string)a.HostName }).ToList();

                foreach (string name in nodeStation.Select(a => a.HostName))
                {
                    hostname.Add(name);
                }
            }
            catch { }

            // For each file...
            hostname.ForEach(hostlist =>
            {
                // Create file item
                var subItem = new TreeViewItem()
                {
                    // Set header as folder name
                    Header = hostlist,
                    // And tag as full path
                    Tag = hostlist

                };

                // Add this item to the parent
                item.Items.Add(subItem);
            });
        }

        public static string ChooseHostname;

        private void OnItemSelected(object sender, RoutedEventArgs e)

        {
            stationView.Tag = e.OriginalSource;


            TreeViewItem selectedTVI = stationView.Tag as TreeViewItem;
            if (!selectedTVI.IsExpanded && !selectedTVI.HasItems)
            {
                if (selectedTVI.Header != null)
                {
                    ChooseHostname = selectedTVI.Header.ToString();
                    ConnectButton.IsEnabled = true;
                    ConnectButton.Content = "Connect : " + ChooseHostname;
                }

            }
            else
            {
                ChooseHostname = string.Empty;
                ConnectButton.Content = "Connect";
                ConnectButton.IsEnabled = false;
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
