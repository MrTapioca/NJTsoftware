using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NJTsoftware.Models;

namespace NJTsoftware.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public readonly PersonalSettings PersonalSettings;
        public Message Message;

        public IndexModel(ILogger<IndexModel> logger, IOptions<PersonalSettings> personalSettings)
        {
            _logger = logger;
            PersonalSettings = personalSettings.Value;
        }

        public void OnGet()
        {

        }
    }
}
