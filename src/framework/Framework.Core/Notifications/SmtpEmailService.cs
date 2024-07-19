// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmtpEmailService.cs" company="Usama Nada">
//   No Copyright .. Copy, Share, and Evolve.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Core.Notifications
{
    using System;

    #region usings

    // using System.Net.Mail;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using SmtpClient = System.Net.Mail.SmtpClient;
    using System.Linq;
    #endregion usings

    /// <summary>
    ///     The smtp email service.
    /// </summary>
    public class SmtpEmailService : IEmailService
    {
        //private readonly IEmailService emailService;
        private readonly SmtpConfiguration _smtpConfiguration;

        public SmtpEmailService(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            //this.emailService = emailService;
            _smtpConfiguration = smtpConfiguration.Value;
        }

        /// <summary>
        /// The send email.
        /// </summary>
        /// <param name="emailMessage">
        /// The email message.
        /// </param>
        /// <param name="notificationSettings">
        /// todo: describe notificationSettings parameter on SendEmail
        /// </param>
        //public void SendEmail(EmailMessage emailMessage
        //    , NotificationSettings notificationSettings

        //    )
        //{
        //    using (var smtpClient = new SmtpClient
        //    {
        //        Host = notificationSettings.SmtpServer,
        //        UseDefaultCredentials = false,
        //        Port = notificationSettings.SmtpPort,
        //        EnableSsl = notificationSettings.SmtpEnableSSL,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        Credentials = new NetworkCredential(
        //                                        notificationSettings.SmtpUserName,
        //                                        notificationSettings.SmtpPassword)
        //    })
        //    {
        //        var mail = emailMessage.ToMailMessage();

        //        ServicePointManager.SecurityProtocol =
        //            SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        //        smtpClient.Send(mail);
        //    }
        //}

        public void SendEmail(EmailMessage emailMessage, NotificationSettings notificationSettings)
        {
            using (
                SmtpClient smtp = new SmtpClient(
                    notificationSettings.SmtpServer,
                    notificationSettings.SmtpPort
                )
            )
            {
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = notificationSettings.IsSmtpAuthenticated;
                smtp.Credentials = new System.Net.NetworkCredential(
                    notificationSettings.SmtpUserName,
                    notificationSettings.SmtpPassword
                );
                var mail = emailMessage.ToMailMessage();

                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex) { }
            }
        }

        public void SendEmail(EmailMessage emailMessage)
        {
            emailMessage.From = _smtpConfiguration.UserName;

            using var client = new MailKit.Net.Smtp.SmtpClient();


            var mailMessage = new MimeMessage();
            mailMessage.To.AddRange(emailMessage.To.Select(x => new MailboxAddress(x, x))) ;
            mailMessage.From.Add( new MailboxAddress(emailMessage.From, emailMessage.From)) ;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = emailMessage.Body;
            mailMessage.Body = bodyBuilder.ToMessageBody();
            mailMessage.Subject = "emailMessage.Subject";

            try
            {

                client.Connect(_smtpConfiguration.SmtpServer, _smtpConfiguration.Port, false);
                client.Authenticate(_smtpConfiguration.UserName, _smtpConfiguration.Password);
                client.Send(mailMessage);
            }
            catch (Exception ee)
            {

                throw;
            }

            using (
                SmtpClient smtp = new SmtpClient(
                    _smtpConfiguration.SmtpServer,
                    _smtpConfiguration.Port
                )
            )
            {
                smtp.EnableSsl = false;
                smtp.Credentials = new System.Net.NetworkCredential(
                    _smtpConfiguration.UserName,
                    _smtpConfiguration.Password
                );
                var mail = emailMessage.ToMailMessage();

                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex) 
                {
                }
            }
        }



    }
}
