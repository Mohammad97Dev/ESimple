using Abp.Application.Services;
using Abp.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.EmailSenders
{
    public interface IEmailSenderAppService : IApplicationService
    {
        Task SendEmail(string email);
    }
}
