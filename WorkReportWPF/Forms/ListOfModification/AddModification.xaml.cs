using System.Windows;
using System.Windows.Controls;

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

    }

}
