using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SupportInvestigation.Models.Model;

namespace SupportInvestigation.Models.CustomView
{
    public class TicketsAddViewModel
    {
        //Class Marchand
        public int MarchandID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int Telephone { get; set; }
        public string Contact { get; set; }
        public int ID_Ticket { get; set; }

        //Class Ticket
        public int ID_User { get; set; }
        [Required(ErrorMessage = "Un titre est requis ")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Un résumé est requis ")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "une description du message est requis ")]
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModified { get; set; }
        public int State { get; set; }

        public List<Marchand> marchands { get; set; }

        public Marchand marchand { get; set; }

        public TicketsAddViewModel()
        {
            Telephone = 0;
        }
    }
}