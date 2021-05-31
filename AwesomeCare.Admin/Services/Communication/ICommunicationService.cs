﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.Communication;
using Refit;

namespace AwesomeCare.Admin.Services
{
   public interface ICommunicationService
    {
        [Get("/Communication")]
        Task<GetCommunication> Get();
        [Get("/Communication/Inbox/{messageId}")]
        Task<InboxMessage> GetInbox(int messageId);
        [Get("/Communication/Sent/{messageId}")]
        Task<SentMessage> GetSent(int messageId);
        [Post("/Communication")]
        Task<HttpResponseMessage> Post([Body]PostCommunication model);
    }
}
