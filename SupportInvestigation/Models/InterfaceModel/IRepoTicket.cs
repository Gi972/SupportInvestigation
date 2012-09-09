using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.Model;
using SupportInvestigation.Models.CustomView;

namespace SupportInvestigation.Models.InterfaceModel
{
    public interface IRepoTicket
    {



        //-------------------------------------------------------------------------------------------//
        //-----------------------------------REQUETES TICKETS----------------------------------------//
        //-------------------------------------------------------------------------------------------//


        //Add

        void Add(Ticket ticket);

        //Update

        void Update(Ticket ticket);
        void UpdateTicketSolved(int id);
        void UpdateIcketRead(int id);

        //Delete
        void Delete(Ticket ticket);

        //Get

        Ticket GetTicket(int id);
        List<Ticket> GetAllTickets();

        /// <summary>
        /// Retourne la liste des tickets dont l'état est défini à non résolu. 
        /// </summary>
        /// <returns></returns>
        IQueryable<Ticket> GetTicketNoSolved();

        /// <summary>
        /// Retourne la liste des tickets dont l'état est défini à résolu.
        /// </summary>
        /// <returns></returns>
        List<Ticket> GetTicketSolved();

        /// <summary>
        /// Retourne les derniers tickets rentrés en base de donnée
        /// </summary>
        /// <returns></returns>
        List<Ticket> GetLastTickets();

        /// <summary>
        /// Retourne un ticket correspondant à une investigation 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Ticket GetTicketBelongToInvestigation(int id);

        /// <summary>
        /// Retourne la liste des tickets avec jointure des tables marchands et user a l'aide d'une procédure stockées 
        /// </summary>
        /// <returns></returns>
        //List<HomeIndexViewModel> GetHomeTicket();

        void Save();
        
          

    }
}