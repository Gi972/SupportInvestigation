using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Repository;
using SupportInvestigation.Models.Administration;
using SupportInvestigation.Models.Model;
using SupportInvestigation.Helpers;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace SupportInvestigation.Controllers
{
    public class UserController : BaseController 
    {
       //
        // GET: /Profile/
        IRepoUser MsiRepoUser;
       // int levelAdmin = 0;
       // int levelUser = 1;
        string roleUser = "";

        public string CookieName { get; set; }
        
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
                                
                // Initialize FormsAuthentification for web.config
                FormsAuthentication.Initialize();

               // Hash du mot de passe
                 MD5 md5Hash = MD5.Create();

                string hashPass = MD5Hash.GetMd5Hash(md5Hash, model.Password);

                MD5Hash.VerifyMd5Hash(md5Hash, model.Password, hashPass);
                
                User logUser = MsiRepoUser.GetLoginAndPass(model.Login, hashPass);

                if (logUser != null)
                {
                    if (logUser.Level == 0)
                    {
                        roleUser = "Admins";
                    }
                    else
                    {
                        roleUser = "Users";
                    }

                    
                    HttpCookie cookieId = new HttpCookie("Mycookie");
                    cookieId["UserId"] = logUser.UserID.ToString();



                    SerializeModel serializeModel = new SerializeModel();
                    serializeModel.UserId = logUser.UserID;
                    serializeModel.Mail = logUser.Mail;
                    serializeModel.LastName = logUser.Login;
                    serializeModel.Role = roleUser;


                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    string userData = serializer.Serialize(serializeModel);

                    //Create a new ticket used for authentification
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1, //Ticket version
                        model.Login, //Username associed with ticket
                        DateTime.Now, //Date/time issued
                        DateTime.Now.AddMinutes(30), //DateTime to expire
                        true,//Persistent cookie
                        userData,//roleUser,//role
                        FormsAuthentication.FormsCookiePath);


                    //Encrypt the Cookie using the machine key for secure transport
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                    //Add the cookie to the list for outgoing response
                    Response.Cookies.Add(cookie);

                    //Redirect to requested URL, or Homepage if no previous page requested
                    string returnUrl = Request.QueryString[""];
                    if (returnUrl == null) returnUrl = "/Ticket/list";

                    Response.Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Mot de passe ou login invalide");
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
