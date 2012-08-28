using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SupportInvestigation.Models.CustomView
{
    public class AdministrationCreateUserViewModel
    {
        [Required(ErrorMessage="Un login est requis")]
        [StringLengthAttribute(10, ErrorMessage = "Votre mot de passe ne peut contenir plus de 10 caractères.")]
        public string Login { get; set; } 
       
        [Required(ErrorMessage="un mot de passe est requis")]
        [StringLengthAttribute(6, ErrorMessage = "Votre mot de passe ne peut contenir plus de 6 caractères.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "un mail est requis")]
        [RegularExpression(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6}",ErrorMessage="l'adresse mail n'est pas correcte")]
        public string Mail { get; set; }

        
        public int Level { get; set; }

        
    }
}