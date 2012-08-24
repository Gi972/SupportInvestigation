using System;
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
    public class TicketController : Controller
    {

        IRepoTicket MsiRepoTicket;
        IRepoUser MsiRepoUser;
        IRepoHypothesis MsiRepoHypo;
        IRepoMarchand MsiRepoMarchand;

        public TicketController() : this(new RepoTicket(), new RepoUser(), new RepoHypothesis(), new RepoMarchand()) { }

        public TicketController(IRepoTicket RepositoryTicket, IRepoUser RepositoryUser, IRepoHypothesis RepositoryHypo, IRepoMarchand RepositoryMarch)
        {
            MsiRepoTicket = RepositoryTicket;
            MsiRepoUser = RepositoryUser;
            MsiRepoHypo = RepositoryHypo;
            MsiRepoMarchand = RepositoryMarch;
        }

        /////////////////////////////////////TICKET///////////////////////////////////////
        //
        // GET: /Ticket/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            var ModelGetAll = MsiRepoTicket.GetTicketNoSolved();

            ViewData["UserAuth"] = MsiRepoUser.GetIdByLogin(User.Identity.Name);

            return View(ModelGetAll.ToList());
        }



        //Voir en détail un ticket
        //GET: /TICKETS/Details/2
        public ActionResult Details(int id)
        {
            Ticket ticket = MsiRepoTicket.GetTicket(id);
           
           // Hypothesis invest = MsiRepoHypo.GetInvestigation(id);
           
            List<Hypothesis> list = MsiRepoHypo.GestInvestigationByTicket(id);
            
            //if (invest == null)
            //{
            //    ticket = MsiRepoTicket.GetTicket(invest.IDTicket);    
            //}
            
            TicketDetailViewModel detailDuticket = new TicketDetailViewModel(ticket, list);

            if (ticket == null)
                return View("NotFound");
            else
                return View("Details", detailDuticket);
        }


        //Ajouter un ticket
        public ActionResult Add()
        {
            if (User.Identity.IsAuthenticated)
            {
                TicketsAddViewModel ticket = new TicketsAddViewModel();

                ViewData["marchandID"] = marchandBox();
                return View(ticket);
            }
            return RedirectToAction("LogOn", "Profile");
        }

        [HttpPost]
        public ActionResult Add(TicketsAddViewModel customModel)
        {

            if (customModel != null)
            {
                if (customModel.MarchandID == 0)
                {



                    if (customModel.MarchandID == 0 & customModel.Name != null & customModel.Url != null & customModel.Contact != null & customModel.Telephone !=0)
                    {
                        Marchand marchand = new Marchand()
                        {
                            Name = customModel.Name,
                            Url = customModel.Url,
                            Phone = customModel.Telephone,
                            ContactName = customModel.Contact,
                        };
                        MsiRepoMarchand.Add(marchand);
                        MsiRepoMarchand.Save();
                        customModel.MarchandID = MsiRepoMarchand.GetIdMarchand(customModel.Url).MarchandID;

                    }
                    else
                    {

                        ViewData["marchandID"] = marchandBox();
                        if (customModel.MarchandID == 0)
                        {
                            ModelState.AddModelError("choiceError", "Choisissez un marchand dans la liste déroulante ou ajouter un nouveau marchand à l'aide des champs");
                        }

                        if (customModel.Name == null)
                        {

                            ModelState.AddModelError("nameError", "vous devez entrer un nom");
                        }

                        if (customModel.Url == null)
                        {

                            ModelState.AddModelError("urlError", "vous devez entrer une URL");
                        }

                        if (customModel.Telephone == 0)
                        {

                            ModelState.AddModelError("phoneError", "vous devez rentrer un numéro de téléphone");
                        }


                        if (customModel.Contact == null)
                        {

                            ModelState.AddModelError("contactError", "vous devez rentrer le nom de votre contact technique");
                        }


                        return View("Add");
                    }
                }


                customModel.ID_User = MsiRepoUser.GetUser(User.Identity.Name).UserID;
                Ticket ticket = new Ticket()
                {
                    IDUser = customModel.ID_User,
                    Title = customModel.Title,
                    Summary = customModel.Summary,
                    Contents = customModel.Description,
                    DateCreation = DateTime.Now,
                    DateModification = DateTime.Now,
                    StateSolved = 0,
                    stateRead = 0,
                    IDMarchand = customModel.MarchandID
                };
                MsiRepoTicket.Add(ticket);
                MsiRepoTicket.Save();


                return View("CreateTicketSuccess");
            }
            else
                return View("NotFound");
        }

        //Editer un ticket
        public ActionResult Edit(int id)
        {
            return View(MsiRepoTicket.GetTicket(id));
        }

        [HttpPost]
        public ActionResult Edit(Ticket ticket)
        {

            if (ModelState.IsValid)
            {
                MsiRepoTicket.Update(ticket);
                MsiRepoTicket.Save();
                return View("EditTicketSuccess");
            }
            return View(ticket);
        }

        //Effacer un ticket

        public ActionResult Delete(int id)
        {
            Ticket ticket = MsiRepoTicket.GetTicket(id);
            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MsiRepoTicket.Delete(MsiRepoTicket.GetTicket(id));
            MsiRepoTicket.Save();
            return View("DeleteTicketSuccess");
        }


        //Changer l'état d'un ticket en résolu
        public ActionResult Solved(int id)
        {
            MsiRepoTicket.UpdateTicketSolved(id);
            return View("Success");
        }


        public List<SelectListItem> marchandBox()
        {

            TicketsAddViewModel ticket = new TicketsAddViewModel();
            ticket.marchands = MsiRepoMarchand.GetAllMarchand();
            var marchanbox = new List<SelectListItem>();
            marchanbox.Add(new SelectListItem { Value = "0", Text = "Please choose" });
            foreach (var item in ticket.marchands)
            {
                marchanbox.Add(new SelectListItem { Value = item.MarchandID.ToString(), Text = item.Url });
            };

            return marchanbox;
        }

            //Archives//
            
        public ActionResult Archived()
        {
            List<Ticket> ticket = MsiRepoTicket.GetTicketSolved().ToList();
            return View(ticket);
        }



    }
}
