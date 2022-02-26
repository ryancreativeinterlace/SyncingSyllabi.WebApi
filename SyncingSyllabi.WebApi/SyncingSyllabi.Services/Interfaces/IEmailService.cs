using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(SendEmailModel mail);
    }
}
