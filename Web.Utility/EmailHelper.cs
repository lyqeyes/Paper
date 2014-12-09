using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Web.Utility
{
    public class EmailHelper
    {
        private EmailHelper()
		{
		}
		private static void SendEmail(string clientHost, string emailAddress, string receiveAddress, string userName, string password, string subject, string body)
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.From = new MailAddress(emailAddress);
			mailMessage.To.Add(new MailAddress(receiveAddress));
			mailMessage.Subject = subject;
			mailMessage.Body = body;
			mailMessage.IsBodyHtml = true;
			mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
			SmtpClient smtpClient = new SmtpClient();
			smtpClient.Host = clientHost;
			smtpClient.Credentials = new NetworkCredential(userName, password);
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			try
			{
				smtpClient.SendAsync(mailMessage, null);
			}
			catch (Exception)
			{
			}
		}
		public static void SendEmail(string subject, string content)
		{
            EmailHelper.SendEmail("smtp.qq.com", "2281880721@qq.com", "lyq@oureda.cn", "2281880721", "18840823312", subject, content);
		}
    }
}
