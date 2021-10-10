using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NJTsoftware.Interfaces;
using NJTsoftware.Models;

namespace NJTsoftware.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ContactSettings _contactSettings;
        private readonly ILogger _logger;

        public ContactController(IEmailSender emailSender,
            IOptions<ContactSettings> contactSettings,
            ILogger<ContactController> logger)
        {
            _emailSender = emailSender;
            _contactSettings = contactSettings.Value;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Send(Message message)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            string subject = "New site message";
            string body = $"Hello. The following message has been posted on your site.\n\n" +
                $"Sender:\n{message.Name}\n\n" +
                $"Email:\n{message.Email}\n\n" +
                $"Message:\n{message.MessageText}";

            try
            {
                await _emailSender.SendEmailAsync(_contactSettings.Email, subject, body);
                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message to contact email.");

                return StatusCode(500);
            }
        }
    }
}