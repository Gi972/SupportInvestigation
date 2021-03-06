﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportInvestigation.Models.Administration;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Repository;
using System.Web.Security;
using SupportInvestigation.Models.Model;
using System.Security.Cryptography;
using System.Text;
using SupportInvestigation.Helpers;
using SupportInvestigation.Models.CustomView;

namespace SupportInvestigation.Controllers
{
    public class AdministrationController : Controller
    {

        //Repository injection de dependance pour user
        IRepoUser MsiRepoUser;
        IRepoMarchand MsiRepoMarchand;


        public AdministrationController() : this(new RepoUser(), new RepoMarchand()) { }

        public AdministrationController(IRepoUser repositoryUser, IRepoMarchand repositoryMarchand)
        {

            MsiRepoUser = repositoryUser;
            MsiRepoMarchand = repositoryMarchand;
        }

       


        public ActionResult Home()
        {
            return View();
        }


        //-------------------------------------------------------------------------------------------//
        //-----------------------------------  Gestion Users ---------------------------------------//
        //-------------------------------------------------------------------------------------------//

        // Create User //

        //
        // GET: /Administration/Create

        public ActionResult CreateUser()
        {
            return View();
        }

        //
        // POST: /Administration/Create

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (user.Level == -1)
            {
                ModelState.AddModelError("errorRole", "Veuillez choisir un role si'l vous plait");
                return View();
            }
           
            if (ModelState.IsValid)
            {                     
                User mTest = MsiRepoUser.GetUserByMail(user.Mail);
            
                if (mTest != null)
                {                  
                    if (user.Mail == mTest.Mail)
                    {
                        ModelState.AddModelError("errorModel", "Un utilisateur utilisant cette adresse email existe déja");
                        return View();
                    }
                }
                try
                {
                    MD5 md5Hash = MD5.Create();
                    user.Password = MD5Hash.GetMd5Hash(md5Hash, user.Password);                   
                    MsiRepoUser.Add(user);
                    MsiRepoUser.Save();
                    return View("CreateUserSuccess");
                }
                catch
                {
                    ModelState.AddModelError("errorModel", "Nous somme désolé votre ticket n'a pas pu être créer");
                    return View();
                }
            }
            return View();
        }

        // Edit User //


        //
        // GET: /Administration/Edit/5

        public ActionResult ChoiceUser()
        {
            return View();
        }

        //
        // POST: /Administration/Edit/5
        [HttpPost]
        public ActionResult ChoiceUser(User user)
        {
            if (user.UserID == -1)
            {
                ModelState.AddModelError("ErrorChoice", "Vous devez choisir un utilisateur");
                return View();
            }
            
            int idUser = user.UserID;
            return RedirectToAction("EditUser", new { id = idUser });
        }

        public ActionResult EditUser(int id)
        {
            User user = MsiRepoUser.GetUser(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MsiRepoUser.Update(user);
                    MsiRepoUser.Save();

                    return View("EditUserSuccess");
                }
                catch
                {
                    return View(user);
                }

            }
            return View(user);
        }

        public ActionResult ChoiceUserToDelete()
        {
            List<User> users = new List<User>();
            users = MsiRepoUser.GetAllUser().ToList();
            var userbox = new List<SelectListItem>();
            userbox.Add(new SelectListItem { Value = "0", Text = "Please choose" });
            foreach (var item in users)
            {
                userbox.Add(new SelectListItem { Value = item.UserID.ToString(), Text = item.Login });
            };
            ViewData["UserID"] = userbox;

            return View();
        }

        [HttpPost]
        public ActionResult ChoiceUserToDelete(User user)
        {
            int idUser = user.UserID;
            return RedirectToAction("DeleteUser", new { id = idUser });           
        }

        // Delete User //
        //
        // GET: /Administration/Delete/5

        public ActionResult DeleteUser(int id)
        {
            User user = MsiRepoUser.GetUser(id);
            return View(user);
        }

        //
        // POST: /Administration/Delete/5

        [HttpPost]
        public ActionResult DeleteUser(int id, User user)
        {
            try
            {
                MsiRepoUser.Delete(MsiRepoUser.GetUser(id));
                MsiRepoUser.Save();

                return View("DeleteUserSuccess");
            }
            catch
            {
                ModelState.AddModelError("errorModel", "Nous somme désolé votre ticket n'a pas pu être supprimer");
                return View();
            }
        }


        public ActionResult ListUser()
        {
            var listUser = MsiRepoUser.GetAllUser();
            return View(listUser);
        }


        public ActionResult ReinitiatePassword(User user)
        {
            PasswordViewModel pass = new PasswordViewModel();
            pass.UserID = user.UserID;
            pass.Login = user.Login;

            return View(pass);
        }


        [HttpPost]
        public ActionResult ReinitiatePassword(PasswordViewModel user)
        {

            if (user.Password != user.PasswordConfirmed || user.Password == string.Empty)
            {
                ModelState.AddModelError("ErrorPassword", "Le mot de passe n'est pas le même");
                    return View(user);
            }

            MD5 md5Hash = MD5.Create();   
           string hashPass =  MD5Hash.GetMd5Hash(md5Hash, user.Password);
           MsiRepoUser.updatePassword(user.UserID, hashPass);
          // User updateUser = MsiRepoUser.GetUser(user.UserID);
           return RedirectToAction("EditUser", new {id = user.UserID});
        }
        //-------------------------------------------------------------------------------------------//
        //-----------------------------------  Creation Marchands ---------------------------------------//
        //-------------------------------------------------------------------------------------------//

        // Create Marchand //

        //
        // GET: /Administration/CreateMarchand

        public ActionResult CreateMarchand()
        {
            return View();
        }

        //
        // POST: /Administration/Create

        [HttpPost]
        public ActionResult CreateMarchand(Marchand marchand)
        {
            if (ModelState.IsValid)
            {
                Marchand mTest = MsiRepoMarchand.GetMarchand(marchand.Url);

                if (mTest != null)
                {
                    if (marchand.Url == mTest.Url)
                    {
                        ModelState.AddModelError("errorModel", "Cet utilisateur existe déja");
                        return View();
                    }
                }

                try
                {
                   
                    MsiRepoMarchand.Add(marchand);
                    MsiRepoMarchand.Save();
                    return View("CreateMarchandSuccess");
                }
                catch
                {
                    ModelState.AddModelError("errorModel", "Nous somme désolé votre ticket n'a pas pu être créer");
                    return View();
                }
            }
            return View();
        }

        // Edit User //


        //
        // GET: /Administration/Edit/5

        public ActionResult ChoiceMarchand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChoiceMarchand(Marchand marchand)
        {
            if (marchand.MarchandID == -1)
            {
                ModelState.AddModelError("ErrorChoice", "Vous devez choisir un marchand");
                return View();
            }
            int idMarchand = marchand.MarchandID;
            return RedirectToAction("EditMarchand", new { id = idMarchand });
        }

        //
        // POST: /Administration/Edit/5

        public ActionResult EditMarchand(int id)
        {
            Marchand marchand = MsiRepoMarchand.GetMarchand(id);
            return View(marchand);
        }


        [HttpPost]
        public ActionResult EditMarchand(int id, Marchand marchand)
        {
            try
            {
                
                MsiRepoMarchand.Update(marchand);
                MsiRepoMarchand.Save();

                return View("EditMarchandSuccess");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ChoiceMarchandToDelete()
        {
            List<Marchand> marchands = new List<Marchand>();
            marchands = MsiRepoMarchand.GetAllMarchand().ToList();
            var userbox = new List<SelectListItem>();
            userbox.Add(new SelectListItem { Value = "0", Text = "Please choose" });
            foreach (var item in marchands)
            {
                userbox.Add(new SelectListItem { Value = item.MarchandID.ToString(), Text = item.Url });
            };
            ViewData["MarchandID"] = userbox;

            return View();
        }

        [HttpPost]
        public ActionResult ChoiceMarchandToDelete(Marchand marchand)
        {
            int idMarchand = marchand.MarchandID;
            return RedirectToAction("DeleteMarchand", new { id = idMarchand });
        }


        // Delete User //
        //
        // GET: /Administration/Delete/5

        public ActionResult DeleteMarchand(int id)
        {
            Marchand marchand = MsiRepoMarchand.GetMarchand(id);
            return View(marchand);
        }

        //
        // POST: /Administration/Delete/5

        [HttpPost]
        public ActionResult DeleteMarchand(int id, Marchand marchand)
        {
            try
            {
                MsiRepoMarchand.Delete(MsiRepoMarchand.GetMarchand(id));
                MsiRepoMarchand.Save();

                return View("DeleteMarchandSuccess");
            }
            catch
            {
                ModelState.AddModelError("errorModel", "Nous somme désolé votre ticket n'a pas pu être supprimer");
                return View();
            }
        }


        public ActionResult ListMarchand()
        {
            var listMarchand = MsiRepoMarchand.GetAllMarchand();
            return View(listMarchand);



        }

    }
}
