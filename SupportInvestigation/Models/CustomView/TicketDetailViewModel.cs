using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.Model;

namespace SupportInvestigation.Models.CustomView
{
    public class TicketDetailViewModel
    {
        public Ticket Ticket { get; set; }
        public List<Hypothesis> Investigation { get; set; }


        public TicketDetailViewModel(Ticket ticket, List<Hypothesis> investigation)
        {
            this.Ticket = ticket;
            this.Investigation = investigation;
        }

    }
}