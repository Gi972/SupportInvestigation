using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SupportInvestigation.Models.Administration
{
    public class AccountUser
    {

        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        public int Level { get; set; }

    }
}