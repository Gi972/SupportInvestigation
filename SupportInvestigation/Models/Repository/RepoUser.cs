using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Model;
using System.Data;

namespace SupportInvestigation.Models.Repository
{
    public class RepoUser : IRepoUser
    {
        private SIDatabaseEntities MsiRepo = new SIDatabaseEntities();


        //-------------------------------------------------------------------------------------------//
        //--------------------------------D'AUTHENTIFICATION REQUEST--------------------------------//
        //-------------------------------------------------------------------------------------------//


        public bool GetLoginAndPass(string login, string mdp, int level)
        {
            var findUser = MsiRepo.Users.SingleOrDefault(d => d.Login == login && d.Password == mdp && d.Level == level);
            if (findUser == null)
            {
                return false;
            }
            return true;
        }


        public bool GetLoginAndPass(string login, string mdp, string role)
        {
            //TODO ne pas oublier d'implementer aprés avoir créer ton modul d'indentification
            return true;
        }


        public User GetLoginAndPass(string login, string mdp)
        {
            try
            {
                var findUser = MsiRepo.Users.SingleOrDefault(d => d.Login == login && d.Password == mdp);
                if (findUser == null)
                {
                    return null;
                }
                return findUser;

            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool GetLevelAdmin(string login)
        {
            var findUser = MsiRepo.Users.SingleOrDefault(d => d.Login == login);
            if (findUser == null || findUser.Level == 1)
            {
                return false;
            }
            return true;
        }

        public int GetIdByLogin(string name)
        {
            return MsiRepo.Users.SingleOrDefault(d => d.Login == name).UserID;
        }


        public void updatePassword(int id, string password)
        {
            var pass = MsiRepo.Users.Single(d => d.UserID == id);
            pass.Password = password;
            MsiRepo.SaveChanges();
        }
        //-------------------------------------------------------------------------------------------//
        //--------------------------------------- USERS REQUEST-------------------------------------//
        //-------------------------------------------------------------------------------------------//


        public void Add(User user)
        {
            MsiRepo.Users.AddObject(user);
        }

        public void Update(User user)
        {
            MsiRepo.Users.Attach(user);
            MsiRepo.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
        }

        public void Delete(User user)
        {
            MsiRepo.Users.DeleteObject(user);
        }

        public List<User> GetAllUser()
        {
            return MsiRepo.Users.ToList();
        }

        public List<User> GetLastUser()
        {
            return (from dbms in MsiRepo.Users
                    orderby dbms.UserID descending
                    select dbms).ToList();
        }
        public User GetUser(int id)
        {
            return MsiRepo.Users.SingleOrDefault(d => d.UserID == id);
        }

        public User GetUser(string login)
        {
            return MsiRepo.Users.SingleOrDefault(d => d.Login == login);
        }

        public User GetUserByMail(string mail)
        {
            return MsiRepo.Users.SingleOrDefault(d => d.Mail == mail);
        }

        public void Save()
        {
            MsiRepo.SaveChanges();
        }



       
    }
}