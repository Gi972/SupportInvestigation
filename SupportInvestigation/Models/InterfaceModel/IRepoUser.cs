using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.Model;

namespace SupportInvestigation.Models.InterfaceModel
{
    public interface IRepoUser
    {

        //-------------------------------------------------------------------------------------------//
        //--------------------------------AUTHENTIFICATION REQUEST--------------------------------//
        //-------------------------------------------------------------------------------------------//


        User GetLoginAndPass(string login, string mdp);
        /// <summary>
        /// Retourne true si le login mot de passe est valide et détermine le niveau de l'utilisateur
        /// </summary>
        /// <param name="login"></param>
        /// <param name="mdp"></param>
        /// <param name="level"></param>
        /// <returns></returns>  
        bool GetLoginAndPass(string login, string mdp, int level);
        bool GetLoginAndPass(string login, string mdp, string role);
      

        /// <summary>
        /// Détermine le niveau de l'utilisateur à partir de son login.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        bool GetLevelAdmin(string login);

        /// <summary>
        /// Retourne l'ID de l'utilisateur a l'aide de son login.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetIdByLogin(string login);

        //-------------------------------------------------------------------------------------------//
        //----------------------------------------REQUETES USERS-------------------------------------//
        //-------------------------------------------------------------------------------------------//

        //Add
        void Add(User user);

        //Update
        void Update(User user);

        //Delete
        void Delete(User user);

        //Get
        List<User> GetAllUser();
        List<User> GetLastUser();
        User GetUser(int id);
        User GetUser(string login);
        User GetUserByMail(string mail);

        void Save();


    }
}