using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Model;
using System.Data;
using SupportInvestigation.Models.CustomView;

namespace SupportInvestigation.Models.Repository
{
    public class RepoTicket : IRepoTicket
    {
        private SIDatabaseEntities MsiRepo = new SIDatabaseEntities();



        //-------------------------------------------------------------------------------------------//
        //-----------------------------------REQUETES TICKETS----------------------------------------//
        //-------------------------------------------------------------------------------------------//




        public void Add(Ticket ticket)
        {
            MsiRepo.Tickets.AddObject(ticket);
        }


        public void Update(Ticket ticket)
        {
            MsiRepo.Tickets.Attach(ticket);
            MsiRepo.ObjectStateManager.ChangeObjectState(ticket, EntityState.Modified);
        }

        public void UpdateTicketSolved(int id)
        {
            var ticket = MsiRepo.Tickets.Single(d => d.TicketID == id);
            ticket.StateSolved = 1;
            MsiRepo.SaveChanges();
            ////MsiRepo.ObjectStateManager.ChangeObjectState(ticket, EntityState.Modified);
        }

        public void Delete(Ticket ticket)
        {
            MsiRepo.Tickets.DeleteObject(ticket);
        }

        public Ticket GetTicket(int id)
        {
            return MsiRepo.Tickets.SingleOrDefault(d => d.TicketID == id);
        }

        public List<Ticket> GetAllTickets()
        {
            return MsiRepo.Tickets.ToList();
        }

        public List<Ticket> GetTicketNoSolved()
        {
            return (from ticket in MsiRepo.Tickets
                    where ticket.StateSolved == 0
                    select ticket).ToList();
        }


        public List<Ticket> GetTicketSolved()
        {
            var listTicket = from ticket in MsiRepo.Tickets where ticket.StateSolved == 1 select ticket;
            return listTicket.ToList();
        }


        public List<Ticket> GetLastTickets()
        {
            return (from ticket in MsiRepo.Tickets
                    orderby ticket.TicketID descending
                    select ticket).ToList();
        }


        public Ticket GetTicketBelongToInvestigation(int id)
        {
            return MsiRepo.Tickets.SingleOrDefault(d => d.TicketID == id);
        }

        //public List<HomeIndexViewModel> GetHomeTicket()
        //{
        //    var list = from tick in MsiRepo.GetListTicketsSql() select tick;
        //    HomeIndexViewModel home;
        //    List<HomeIndexViewModel> listehome = new List<HomeIndexViewModel>();

        //    foreach (var item in list)
        //    {
        //        home = new HomeIndexViewModel();
        //        home.Name = item.Name;
        //        home.summary = item.Summary;
        //        home.Title = item.Title;
        //        home.url = item.Url;
        //        home.TicketID = item.TicketID;
        //        home.Description = item.Description;
        //        home.DateCreation = item.DateCreation;

        //        listehome.Add(home);
        //    }
        //    return listehome;
        //}







        public void Save()
        {
            MsiRepo.SaveChanges();
        }
    }


}
