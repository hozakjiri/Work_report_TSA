using Microsoft.Win32;
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

                if (!string.IsNullOrEmpty(attachment) && attachment != "")
                {
                    Attachment Data = new Attachment(attachment, MediaTypeNames.Application.Octet);
                    // '// Add time stamp information for the file.
                    ContentDisposition disposition1 = Data.ContentDisposition;
                    disposition1.CreationDate = File.GetCreationTime(attachment);
                    disposition1.ModificationDate = File.GetLastWriteTime(attachment);
                    disposition1.ReadDate = File.GetLastAccessTime(attachment);
                    // '// Add the file attachment to this e-mail message.
                    oMail.Attachments.Add(new Attachment(attachment, MediaTypeNames.Application.Octet));
                    Data.Dispose();
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
                
                smtp.Send(oMail);
                MessageBox.Show("Email send", "Mail");
            }
            catch (SmtpException ex)
            {
                MessageBox.Show("You don't connect to Hella SMTP server: " + ex.Message, "Alert");
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
