using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SupportInvestigation.Models.Model;

namespace SupportInvestigation.Models.CustomView
{
    public class PasswordViewModel
    {

        [Required(ErrorMessage="Un mot de passe est obligatoire")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vous devez confirmer le mot de passe")]
        public string PasswordConfirmed { get; set; }
        public User user { get; set; }

        public string Login { get; set; }
        public int UserID { get; set; }
    }
}