﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.Services
{
  public  interface IEmailService
    {
        Task SendAsync(List<string> recipients, string subject, string htmlContent, bool showAllRecipients = false);
    }
}
