using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldies.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> _logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            this._logger = logger;
        }
        public void SendMessage(string to, string subject, string body)
        {
            //TO DO: Add proper code to send the message
            _logger.LogInformation($"To {to} Subject: {subject} Body: {body}");
        }
    }
}
