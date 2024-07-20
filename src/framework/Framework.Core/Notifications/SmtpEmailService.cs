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
    using System.Diagnostics;
    using Microsoft.Extensions.Logging;
    #endregion usings

    /// <summary>
    ///     The smtp email service.
    /// </summary>
    public class SmtpEmailService : IEmailService
    {
        //private readonly IEmailService emailService;

        private readonly ILogger<SmtpEmailService> _logger;
        private readonly SmtpConfiguration _smtpConfiguration;

        public SmtpEmailService(IOptions<SmtpConfiguration> smtpConfiguration, ILogger<SmtpEmailService> logger)
        {
            //this.emailService = emailService;
            _smtpConfiguration = smtpConfiguration.Value;
            _logger = logger;   
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

            _logger.LogInformation(_smtpConfiguration.UserName);
            var mailMessage = new MimeMessage();
            mailMessage.To.AddRange(emailMessage.To.Select(x => new MailboxAddress(x, x))) ;
            mailMessage.From.Add(new MailboxAddress("No Reply", "NoReplay@notify.alqassim.gov.sa"));
            //mailMessage.From.Add( new MailboxAddress(_smtpConfiguration.From)) ;
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendEmail=> Error");
                throw;
            }

             
        }



    }
}
