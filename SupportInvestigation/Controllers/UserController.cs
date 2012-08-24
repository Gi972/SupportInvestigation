using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Repository;
using SupportInvestigation.Models.Administration;

namespace SupportInvestigation.Controllers
{
    public class UserController : Controller
    {
       //
        // GET: /Profile/
        IRepoUser MsiRepoUser;
        int levelAdmin = 0;
        int levelUser = 1;
        
        public UserController() : this(new RepoUser()) { }

        public UserController(IRepoUser Repository) {
            MsiRepoUser = Repository;
        }

        //Connecte un utilisateur
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(AccountUser model)
        {           
            if (ModelState.IsValid)
            {                
                if (MsiRepoUser.GetLoginAndPass(model.Login, model.Password, levelAdmin))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                                                 
                    return RedirectToAction("Home","Administration");
                }
                else if (MsiRepoUser.GetLoginAndPass(model.Login, model.Password, levelUser))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);

                    return RedirectToAction("List", "Ticket");
                }         
            }
            return View();
        }

        public ActionResult Home()
        {         
            return View();
        }

        //Déconnecte un utilisateur
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        
    }

    
}
