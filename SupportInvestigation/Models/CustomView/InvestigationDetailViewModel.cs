using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.Model;

namespace SupportInvestigation.Models.CustomView
{
    public class InvestigationDetailViewModel
    {
        
        public Ticket Ticket { get; set; }
        public Hypothesis hypothese { get; set; }
        public List<Hypothesis> Investigation { get; set; }


        public InvestigationDetailViewModel(Hypothesis hypothese, Ticket ticket, List<Hypothesis> investigation)
        {
            this.hypothese = hypothese;
            this.Ticket = ticket;
            this.Investigation = investigation;
        }
    }
}