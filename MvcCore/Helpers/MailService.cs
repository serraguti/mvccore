using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class MailService
    {
        IConfiguration Configuration;

        public MailService(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        private MailMessage ConfigureMail
            (String receptor, String asunto, String mensaje)
        {
            MailMessage mail = new MailMessage();
            String usermail = this.Configuration["usuariomail"];
            String passwordmail = this.Configuration["passwordmail"];
            mail.From = new MailAddress(usermail);
            mail.To.Add(new MailAddress(receptor));
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            String smtpserver = this.Configuration["host"];
            int port = int.Parse(this.Configuration["port"]);
            bool ssl = bool.Parse(this.Configuration["ssl"]);
            bool defaultcredentials =
                bool.Parse(this.Configuration["defaultcredentials"]);
            return mail;
        }

        private void ConfigureSmtp(MailMessage mail)
        {
            String usermail = this.Configuration["usuariomail"];
            String passwordmail = this.Configuration["passwordmail"];
            String smtpserver = this.Configuration["host"];
            int port = int.Parse(this.Configuration["port"]);
            bool ssl = bool.Parse(this.Configuration["ssl"]);
            bool defaultcredentials =
                bool.Parse(this.Configuration["defaultcredentials"]);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = smtpserver;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
            smtpClient.UseDefaultCredentials = defaultcredentials;
            NetworkCredential usercredential =
                new NetworkCredential(usermail, passwordmail);
            smtpClient.Credentials = usercredential;
            smtpClient.Send(mail);
        }

        public void SendMail
            (String receptor, String asunto, String mensaje)
        {
            MailMessage mail = 
                this.ConfigureMail(receptor, asunto,mensaje);
            this.ConfigureSmtp(mail);
        }


        public void SendMail
            (String receptor, String asunto, String mensaje, String filepath)
        {
            MailMessage mail =
                this.ConfigureMail(receptor, asunto, mensaje);
            Attachment attachment = new Attachment(filepath);
            mail.Attachments.Add(attachment);
            this.ConfigureSmtp(mail);
        }
    }
}
