using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenHealth.Email
{
    public interface IEmailSender
    {
        //Sends Email with given information
        Task<SendEmailResponse> SendEmailAsync(string userEmail, string emailSubject, string message); 
    }
}
