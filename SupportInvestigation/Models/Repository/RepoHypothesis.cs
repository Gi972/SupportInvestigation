using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Model;
using SupportInvestigation.Models.CustomView;
using System.Data;

namespace SupportInvestigation.Models.Repository
{
    public class RepoHypothesis : IRepoHypothesis
    {
        private SIDatabaseEntities MsiRepo = new SIDatabaseEntities(); 

        public void Add(Hypothesis investigation)
        {
            MsiRepo.Hypothesis.AddObject(investigation);
        }


        public void Update(Hypothesis investigation)
        {
            MsiRepo.Hypothesis.Attach(investigation);
            MsiRepo.ObjectStateManager.ChangeObjectState(investigation, EntityState.Modified);
        }

        public void Delete(Hypothesis investigation)
        {
            MsiRepo.Hypothesis.DeleteObject(investigation);
        }

        public Hypothesis GetInvestigation(int id)
        {
            return MsiRepo.Hypothesis.FirstOrDefault(d => d.hypothesisID == id);
        }

        //TODO revoir l'implementation pour retourner les 5 derniers investigations et non pas tout pareil pour ticket et dans fakeDB aussi
        public IQueryable<Hypothesis> GetLastInvestigation()
        {
            return (from dbms in MsiRepo.Hypothesis
                    orderby dbms.hypothesisID descending
                    select dbms).Take(5);

        }

        public List<Hypothesis> GetAllInvestigation()
        {
            return MsiRepo.Hypothesis.ToList();
        }


        public List<Hypothesis> GetAllInvestigationInProgress()
        {
            return MsiRepo.Hypothesis.Where(d => d.StateSolved == 0).ToList();
        }

        //public List<HomeIndexViewModel> GetHomeInvestigation()
        //{
        //    var list = from tick in MsiRepo.GetListInvestigationSql() select tick;
        //    HomeIndexViewModel home;
        //    List<HomeIndexViewModel> listehome = new List<HomeIndexViewModel>();

        //    foreach (var item in list)
        //    {
        //        home = new HomeIndexViewModel();
        //        home.titleInvestigation = item.Title;
        //        home.IdInvestigation = item.InvestigationID;
        //        home.dateCreationInvest = item.DateCreation;
        //        home.NameInvest = item.TitreTicket;
        //        home.IDTicket = item.ID_Ticket;

        //        listehome.Add(home);
        //    }
        //    return listehome;
        //}

       public  List<Hypothesis> GestInvestigationByTicket(int idTicket)
        {
            var listInvestigation = from db in MsiRepo.Hypothesis where db.IDTicket == idTicket select db;

            return listInvestigation.ToList();
        }



        List<Hypothesis> IRepoHypothesis.GetLastInvestigation()
        {
            throw new NotImplementedException();
        }





        public void Save()
        {
            MsiRepo.SaveChanges();
        }
    }
}