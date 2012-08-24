using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportInvestigation.Models.CustomView
{
    public class HomeIndexViewModel
    {

        //Retourne la liste des tickets

        public string Name { get; set; }
        public string summary { get; set; }
        public string Title { get; set; }
        public string url { get; set; }
        public int TicketID { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }


        //Retourne la liste des investigations

        public int IdInvestigation { get; set; }
        public int IDTicket { get; set; }
        public string titleInvestigation { get; set; }
        public DateTime dateCreationInvest { get; set; }
        public string NameInvest { get; set; }

    }
}