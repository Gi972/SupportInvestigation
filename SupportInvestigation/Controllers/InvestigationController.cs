using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Repository;
using SupportInvestigation.Models.Model;
using SupportInvestigation.Models.CustomView;
using PagedList;

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

        public ViewResult ListHypo(string sortOrder, string searchString, int? page)
        {
            ViewBag.MarchandSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.AuteurSortParm = sortOrder == "Auteur" ? "Auteur desc" : "Auteur";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

            var hypo = MsiRepoHypo.GetAllInvestigationInProgress();
            var hypoSort = hypo.OrderBy(m => m.IDTicket);

            switch (sortOrder)
            {
               
                case "Name":
                    hypoSort = hypo.OrderBy(m => m.IDTicket);
                    break;
                case "Auteur desc":
                    hypoSort = hypo.OrderByDescending(m => m.IDUser);
                    break;
                case "Auteur":
                    hypoSort = hypo.OrderBy(m => m.IDUser);
                    break;
                case "Date desc":
                    hypoSort = hypo.OrderByDescending(m => m.DateCreation);
                    break;
               
                default:
                    hypoSort = hypo.OrderByDescending(i => i.IDTicket);
                    break;

            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(hypoSort.ToPagedList(pageNumber, pageSize));
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
        [ValidateInput(false)]
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
        [ValidateInput(false)]
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
            Hypothesis hypothese = MsiRepoHypo.GetInvestigation(id);
            Ticket ticket = MsiRepoTicket.GetTicketBelongToInvestigation(hypothese.IDTicket);
            List<Hypothesis> list = MsiRepoHypo.GestInvestigationByTicket(ticket.TicketID);
            
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
