using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportInvestigation.Models.Administration;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Repository;
using System.Web.Security;
using SupportInvestigation.Models.Model;

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





        int levelAdmin = 0;
        int levelUser = 1;



        //
        // GET: /Administration/connect

        public ActionResult Connect()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Connect(AccountUser model)
        {
            if (ModelState.IsValid)
            {
                if (MsiRepoUser.GetLoginAndPass(model.Login, model.Password, levelAdmin))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);

                    return RedirectToAction("Home");
                }
                else if (MsiRepoUser.GetLoginAndPass(model.Login, model.Password, levelUser))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);

                    return RedirectToAction("Home", "User");
                }
                else
                {
                    ModelState.AddModelError("", "mot de passe ou login invalide");
                }
            }
            return View();
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
            if (ModelState.IsValid)
            {
                User mTest = MsiRepoUser.GetUser(user.Login);

                if (mTest != null)
                {
                    if (user.Login == mTest.Login || user.Mail == mTest.Mail)
                    {
                        ModelState.AddModelError("errorModel", "Cet utilisateur existe déja");
                        return View();
                    }
                }
                try
                {
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

        //
        // POST: /Administration/Edit/5
        [HttpPost]
        public ActionResult ChoiceUser(User user)
        {
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
            List<Marchand> users = new List<Marchand>();
            users = MsiRepoMarchand.GetAllMarchand().ToList();
            var userbox = new List<SelectListItem>();
            userbox.Add(new SelectListItem { Value = "0", Text = "Please choose" });
            foreach (var item in users)
            {
                userbox.Add(new SelectListItem { Value = item.MarchandID.ToString(), Text = item.Url });
            };
            ViewData["MarchandID"] = userbox;

            return View();
        }

        [HttpPost]
        public ActionResult ChoiceMarchand(Marchand marchand)
        {
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
                // TODO: Add update logic here
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
