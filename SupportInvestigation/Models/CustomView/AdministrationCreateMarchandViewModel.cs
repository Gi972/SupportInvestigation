using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SupportInvestigation.Models.CustomView
{
    public class AdministrationCreateMarchandViewModel
    {
        [Required(ErrorMessage="Le nom du marchand est requis")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Url du marchand est requis")]
        public string Url { get; set; }
        [Required(ErrorMessage = "Le Nom du contact du site marchand est requis")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Le n° de téléphone du du marchand est requis")]
        public int Phone { get; set; }
    }
}