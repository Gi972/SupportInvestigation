using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Repository;
using SupportInvestigation.Models.Model;
using SupportInvestigation.Models.CustomView;
using System.Collections;
using PagedList;

namespace SupportInvestigation.Controllers
{
    // [OutputCache(Duration=30)] //Mise en cache 30 secondes
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

        public ViewResult List(string sortOrder, string searchString, int? page)
        {

            ViewBag.MarchandSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.AuteurSortParm = sortOrder == "Auteur" ? "Auteur desc" : "Auteur";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "Title desc" : "Title";
            ViewBag.readSortParm = sortOrder == "Read" ? "Read desc" : "Read";


            var ticket = MsiRepoTicket.GetTicketNoSolved();


            switch (sortOrder)
            {
                case "Name desc":
                    ticket = ticket.OrderByDescending(m => m.Marchands.Url);
                    break;
                case "Name":
                    ticket = ticket.OrderBy(m => m.Marchands.Url);
                    break;
                case "Auteur desc":
                    ticket = ticket.OrderByDescending(m => m.Users.Login);
                    break;
                case "Auteur":
                    ticket = ticket.OrderBy(m => m.Users.Login);
                    break;
                case "Date desc":
                    ticket = ticket.OrderByDescending(m => m.DateCreation);
                    break;
                case "Title desc":
                    ticket = ticket.OrderByDescending(m => m.Title);
                    break;
                case "Title":
                    ticket = ticket.OrderBy(m => m.Title);
                    break;
                case "Read desc":
                    ticket = ticket.OrderByDescending(m => m.stateRead);
                    break;
                case "Read":
                    ticket = ticket.OrderBy(m => m.stateRead);
                    break;
                default:
                    ticket = ticket.OrderByDescending(m => m.DateCreation);
                    break;

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            // Section Statistique
            int nbrNoread = 0;
            int nbrRead = 0;
            int nbrSolved = MsiRepoTicket.GetTicketSolved().ToList().Count;
            foreach (var item in ticket)
            {
                if (item.stateRead == 0)
                {
                    nbrNoread++;
                }
                if (item.stateRead == 1)
                {
                    nbrRead++;
                }
               
            }
            ViewBag.ticketNoRead = nbrNoread;
            ViewBag.ticketRead = nbrRead;
            ViewBag.ticketSolved = nbrSolved;

            return View(ticket.ToPagedList(pageNumber, pageSize));


        }



        //Voir en détail un ticket
        //GET: /TICKETS/Details/2
        public ActionResult Details(int id)
        {
            Ticket ticket = MsiRepoTicket.GetTicket(id);
            List<Hypothesis> list = MsiRepoHypo.GestInvestigationByTicket(id);
            MsiRepoTicket.UpdateIcketRead(id);
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
        [ValidateInput(false)]
        public ActionResult Add(TicketsAddViewModel customModel)
        {

            if (customModel != null)
            {
                if (customModel.MarchandID == 0)
                {

                    if (customModel.MarchandID == 0 & customModel.Name != null & customModel.Url != null & customModel.Contact != null & customModel.Telephone != 0)
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


                //customModel.ID_User = MsiRepoUser.GetUserByMail((User as CustomPrincipal).Mail).UserID;
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
        [ValidateInput(false)]
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
            MsiRepoHypo.DeleteHypoBelongToTicket(id);
            MsiRepoTicket.Delete(MsiRepoTicket.GetTicket(id));
            MsiRepoTicket.Save();
            return View("DeleteTicketSuccess");
        }


        //Changer l'état d'un ticket en résolu
        public ActionResult Solved(int id)
        {
            MsiRepoHypo.UpdateHypothesysSolved(id);
            MsiRepoTicket.UpdateTicketSolved(id);
            return View("ArchivedSuccess");
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

        public ActionResult Archived(string sortOrder, string searchString, int? page)
        {

            ViewBag.MarchandSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.AuteurSortParm = sortOrder == "Auteur" ? "Auteur desc" : "Auteur";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";


            var archive = MsiRepoTicket.GetTicketSolved();


            if (!String.IsNullOrEmpty(searchString))
            {
                archive = archive.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
            }


            var archiveSort = archive.OrderBy(m => m.Marchands.Name);
            switch (sortOrder)
            {
                case "Name":
                    archiveSort = archive.OrderBy(m => m.Marchands.Url);
                    break;
                case "Auteur desc":
                    archiveSort = archive.OrderByDescending(m => m.IDUser);
                    break;
                case "Auteur":
                    archiveSort = archive.OrderBy(m => m.IDUser);
                    break;
                case "Date desc":
                    archiveSort = archive.OrderByDescending(m => m.DateCreation);
                    break;

                default:
                    archiveSort = archive.OrderByDescending(i => i.Marchands.Url);
                    break;

            }
         
            //Pagination
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(archiveSort.ToPagedList(pageNumber, pageSize));
        }

    }
}
