using Microsoft.Win32;
using System;

namespace WorkReportWPF.Functions
{
    public class CustomFunc
    {


        public static string Drive = Environment.SpecialFolder.MyComputer.ToString();

        public static string GetFileToString()
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = Drive;
            if (dialog.ShowDialog() == true)
            {
                if (dialog.FileName == "")
                {
                    return "";
                }
                else
                {
                    return dialog.FileName;
                }
            }
            return "";
        }
    }
}
