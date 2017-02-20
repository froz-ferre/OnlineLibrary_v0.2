using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace OnlineLibrary.Helpers
{
    public class Mail
    {
        private string login = "Admin@OLibrary.com";
        private string password = "password123";

        public void SendMessage(string toEmail)
        {
            try
            {
                string smtp = "";
                int port = 0;

                var mailCoding = new Dictionary<string, int>
                {
                    {"gmail.com", 587},
                    {"yandex.ru", 225},
                    {"mail.ru", 235},
                    {"list.ru", 254},
                    {"inbox.ru", 215},
                    {"bk.ru", 255}
                };

                FileStream file = new FileStream("D:\\Prog\\CS\\OnlineLibrary\\OnlineLibrary\\Helpers\\mail.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file);
                // поиск нужного порта и smtp при отправке
                foreach (var kvp in mailCoding.Where(kvp => login.IndexOf(kvp.Key, StringComparison.Ordinal) > -1))
                {
                    smtp = "smtp." + kvp.Key;
                    port = kvp.Value;
                }

                using (var mailMessage = new MailMessage())
                {
                    mailMessage.To.Add(toEmail);
                    mailMessage.From = new MailAddress(login);
                    mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                    mailMessage.IsBodyHtml = true;

                    mailMessage.Subject = "Online Library " + DateTime.Now; 
                    mailMessage.Body = reader.ReadToEnd(); 

                    using (var sc = new SmtpClient(smtp, port))
                    {
                        sc.EnableSsl = true;
                        sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                        sc.UseDefaultCredentials = false;
                        sc.Credentials = new NetworkCredential(login, password);
                        sc.Send(mailMessage);
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}