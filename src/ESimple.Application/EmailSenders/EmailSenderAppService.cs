using Abp.Application.Services;
using Abp.Configuration;
using ESimple.Configuration;
using ESimple.Configuration.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.EmailSenders
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class EmailSenderAppService : ApplicationService, IEmailSenderAppService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmailSenderAppService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        public async Task SendEmail(string email)
        {

            var emailSettingDto = new EmailSettingDto()
            {
                SenderEmail = await SettingManager.GetSettingValueAsync(AppSettingNames.SenderEmail),
                SenderPassword = await SettingManager.GetSettingValueAsync(AppSettingNames.SenderPassword),
                SenderHost = await SettingManager.GetSettingValueAsync(AppSettingNames.SenderHost),
                SenderPort = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.SenderPort),
                SenderEnableSsl = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.SenderEnableSsl),
                SenderUseDefaultCredentials = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.SenderUseDefaultCredentials),
                Message = await SettingManager.GetSettingValueAsync(AppSettingNames.Message),
                MessageForResetPassword = await SettingManager.GetSettingValueAsync(AppSettingNames.MessageForResetPassword)
            };
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailSettingDto.SenderEmail);
                mail.Subject = "ESimpleTest";
                mail.Body = emailSettingDto.Message;
                mail.IsBodyHtml = true;
                mail.To.Add(email);

                SmtpClient smtp = new SmtpClient(emailSettingDto.SenderHost, emailSettingDto.SenderPort);
                smtp.EnableSsl = emailSettingDto.SenderEnableSsl;

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential
                {
                    UserName = emailSettingDto.SenderEmail,
                    Password = emailSettingDto.SenderPassword
                };


                smtp.UseDefaultCredentials = emailSettingDto.SenderUseDefaultCredentials;
                smtp.Credentials = NetworkCred;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }


    }
}
