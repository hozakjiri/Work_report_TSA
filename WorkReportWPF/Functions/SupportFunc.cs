﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Windows;

namespace WorkReportWPF.Functions
{
    public class SupportFunc
    {
        public static void SendSupportMail(string subject, string description, string attachment, string mailTo)
        {
            try
            {
                MailMessage oMail = new()
                {
                    From = new MailAddress("WorkReport_Support@hella.com")
                };
                oMail.To.Add(mailTo);

                oMail.Subject = "Support for WorkReport";
                var Data = new Attachment(attachment, MediaTypeNames.Application.Octet);
                if (!string.IsNullOrEmpty(attachment))
                {
                    // '// Add time stamp information for the file.
                    ContentDisposition disposition1 = Data.ContentDisposition;
                    disposition1.CreationDate = File.GetCreationTime(attachment);
                    disposition1.ModificationDate = File.GetLastWriteTime(attachment);
                    disposition1.ReadDate = File.GetLastAccessTime(attachment);
                    // '// Add the file attachment to this e-mail message.
                    oMail.Attachments.Add(Data);
                }

                StringBuilder sb = new();
                sb.AppendLine("-------------------------------------");
                sb.AppendLine("User Name : " + Environment.UserName);
                sb.AppendLine("Host Name : " + Environment.MachineName);
                sb.AppendLine("Domain : " + Environment.UserDomainName);
                sb.AppendLine("-------------------------------------");
                sb.AppendLine("Subject :" + subject);
                sb.AppendLine("-------------------------------------");
                sb.AppendLine("Description :" + description);
                sb.AppendLine("-------------------------------------");
                oMail.Body = sb.ToString();
                SmtpClient smtp = new("smtphub.dc.hella.com");
                smtp.Port = 25;
                MessageBox.Show("Email send", "Mail");
                smtp.Send(oMail);
                Data.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

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