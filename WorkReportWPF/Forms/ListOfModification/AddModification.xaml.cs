using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WorkReportWPF.Functions;


namespace WorkReportWPF.Forms.ListOfModification
{
    /// <summary>
    /// Interaction logic for AddModification.xaml
    /// </summary>
    public partial class AddModification : Page
    {
        public AddModification()
        {
            InitializeComponent();
        }

        private int _numValue = 0;

        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                txtNum.Text = value.ToString();
            }
        }

        public void NumberUpDown()
        {
            InitializeComponent();
            txtNum.Text = _numValue.ToString();
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValue += 5;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            NumValue -= 5;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null)
            {
                return;
            }

            if (!int.TryParse(txtNum.Text, out _numValue))
                txtNum.Text = _numValue.ToString();
        }

        public static string Drive = Environment.SpecialFolder.MyComputer.ToString();

        private void Attachments_button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                TextPathImage.Text = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                imageBox.Source = bitmap;
            }

        }

        public DateTime DisplayDate { get; set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProjectBox.ItemsSource = ModificationFunc.LoadProjectList();
            datePicker.SelectedDate = DateTime.Now;
        }

        private void Save_Modification_click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Can you save data?", "Data", MessageBoxButton.OKCancel);

            string name = System.IO.Path.GetFileName(TextPathImage.Text);
            string fullpath = !string.IsNullOrEmpty(name) ? Directory.GetCurrentDirectory() + "/Image/" + Environment.UserName + DateTime.Now.ToString("yyyyMMddHHmm") + ".jpg" : "";

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Image"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Image");
            }

            try
            {
                if (!string.IsNullOrEmpty(TextPathImage.Text))
                {
                    File.Copy(TextPathImage.Text, fullpath, true);
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (ProjectBox.Text == "" || Comment_Text.Text == "" || txtNum.Text == "" || datePicker.Text == "")
            {
                MessageBox.Show("Please, fill all data", "Data");
            }
            else if (result == MessageBoxResult.OK)
            {
                ModificationFunc.SaveModification(ProjectBox.Text, Comment_Text.Text, datePicker.DisplayDate.ToString("dd.MM.yyyy"), txtNum.Text, fullpath, datePicker.DisplayDate.ToString("yyyyMMddHHmm"));

                MessageBox.Show("Data was added !", "Data");
            }
        }
    }
}