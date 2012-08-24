﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Repository;
using SupportInvestigation.Models.Model;
using SupportInvestigation.Models.CustomView;

namespace SupportInvestigation.Controllers
{
    public class InvestigationController : Controller
    {
         //Intégration du repository
        IRepoHypothesis MsiRepoHypo;
        IRepoUser MsiRepoUser;
        IRepoTicket MsiRepoTicket;
        //Constructor

        public  InvestigationController() : this(new RepoHypothesis(), new RepoUser(), new RepoTicket()){}


        public InvestigationController(IRepoHypothesis repositoryHypo , IRepoUser repositoryUser, IRepoTicket repositoryTicket)
        {
        MsiRepoHypo = repositoryHypo;
        MsiRepoUser = repositoryUser;
            MsiRepoTicket =repositoryTicket;
        }
        //
        // GET: /Investigations/


        //List les investigations en  cours 

        public ActionResult ListHypo()
        {
            var investigationInProgress = MsiRepoHypo.GetAllInvestigationInProgress();

            return View(investigationInProgress.ToList());
        }

        //Cree une investigation
        public ActionResult Create(int id)
        {       
                Hypothesis investigation = new Hypothesis();
                investigation.IDUser = MsiRepoUser.GetIdByLogin(User.Identity.Name);
            investigation.IDTicket = id;
                return View("Create", investigation);         
        }

        [HttpPost]
        public ActionResult Create(Hypothesis investigation)
        {
            if (investigation != null)
            {
                investigation.StateSolved = 0;
                investigation.DateCreation = DateTime.Now;
                

                
                MsiRepoHypo.Add(investigation);




                MsiRepoHypo.Save();

                

                return View("CreateHypoSuccess");
            }
            return View();
        }

        //Edite une investigation
        public ActionResult Edit(int id)
        {
            return View(MsiRepoHypo.GetInvestigation(id));
        }

        [HttpPost]
        public ActionResult Edit(Hypothesis investigation)
        {

            if (ModelState.IsValid)
            {
                MsiRepoHypo.Update(investigation);
                MsiRepoHypo.Save();
                return View("EditHypoSuccess");
            }
            return View(investigation);
        }


        //voit en détail une investigation
        public ActionResult Details(int id)
        {
            Ticket ticket = MsiRepoTicket.GetTicketBelongToInvestigation(id);
            List<Hypothesis> list = MsiRepoHypo.GestInvestigationByTicket(ticket.TicketID);
            Hypothesis hypothese = MsiRepoHypo.GetInvestigation(id);

            InvestigationDetailViewModel detailDuticket = new InvestigationDetailViewModel(hypothese,ticket, list);

            if (ticket == null)
                return View("NotFound");
            else
                return View("Details", detailDuticket);
        }

        //Détruit une investigation
        public ActionResult Delete(int id)
        {
            Hypothesis investigation = MsiRepoHypo.GetInvestigation(id);
            return View(investigation);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            MsiRepoHypo.Delete(MsiRepoHypo.GetInvestigation(id));
            MsiRepoHypo.Save();
            return View("DeleteHypoSuccess");
        }
    }
}
