using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NJTsoftware.Models
{
    public class PersonalSettings
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => FirstName + " " + LastName; }
        public string EmailUser { get; set; }
        public string EmailDomain { get; set; }
        public string Phone { get; set; }

        public string ResumeImageFile { get; set; }
        public string ResumePdfFile { get; set; }

        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
    }
}
