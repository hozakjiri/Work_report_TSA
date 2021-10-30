using Microsoft.Win32;
using System;

namespace WorkReportWPF.Functions
{
    public class CustomFunc
    {


        public static string Drive = Environment.SpecialFolder.MyComputer.ToString();

        public static string GetFileToString()
        {
            OpenFileDialog dialog = new()
            {
                InitialDirectory = Drive
            };
            return dialog.ShowDialog() == true ? dialog.FileName == "" ? "" : dialog.FileName : "";
        }
    }
}
