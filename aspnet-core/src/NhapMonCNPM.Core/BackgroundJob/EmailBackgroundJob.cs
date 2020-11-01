using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NhapMonCNPM.BackgroundJob
{
    /// <summary>
    /// Send all EmailMessages to all Recevers
    /// </summary>
    public class EmailBackgroundJob : BackgroundJob<EmailBackgroundJobArgs>, ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        public EmailBackgroundJob(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [UnitOfWork]
        public override void Execute(EmailBackgroundJobArgs args)
        {
            Logger.Info("Email backgroung trigger!");
            try
            {
                foreach (var email in args.TargetEmails)
                {
                    _emailSender.Send(
                        to:email,
                        subject:args.Subject,
                        body: args.Body,
                        isBodyHtml: true                      
                    );
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}
