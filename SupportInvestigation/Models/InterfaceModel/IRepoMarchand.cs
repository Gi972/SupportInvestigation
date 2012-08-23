using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.Model;

namespace SupportInvestigation.Models.InterfaceModel
{
    public interface IRepoMarchand
    {
        //-------------------------------------------------------------------------------------------//
        //-------------------------------------REQUETES MARCHANDS------------------------------------//
        //-------------------------------------------------------------------------------------------//


        //Add
        void Add(Marchand marchand);

        //Update
        void Update(Marchand marchand);

        //Delete 
        void Delete(Marchand marchand);

        //Get
        List<Marchand> GetAllMarchand();
        List<Marchand> GetLastMarchand();
        Marchand GetMarchand(int id);
        Marchand GetMarchand(string url);
        Marchand GetIdMarchand(string url);

        void Save();
    }
}