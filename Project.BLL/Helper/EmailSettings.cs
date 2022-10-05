﻿using Microsoft.AspNetCore.Identity;
using Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email,IdentityUser user) 
        {
            var Client = new SmtpClient("smtp.gmail.com",587);
            Client.UseDefaultCredentials = false;
            Client.Credentials = new NetworkCredential("mailservice7539@gmail.com", "jenhjysihojzpefr");
            Client.EnableSsl = true;
            Client.Send("mailservice7539@gmail.com", user.Email,email.Title,email.Body);
        }
    }
}
