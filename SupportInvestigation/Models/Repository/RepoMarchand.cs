using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Model;
using System.Data;

namespace SupportInvestigation.Models.Repository
{
    public class RepoMarchand : IRepoMarchand
    {
        private SIDatabaseEntities MsiRepo = new SIDatabaseEntities();

        public void Add(Marchand marchand)
        {
            MsiRepo.Marchands.AddObject(marchand);
        }

        public void Update(Marchand marchand)
        {
            MsiRepo.Marchands.Attach(marchand);
            MsiRepo.ObjectStateManager.ChangeObjectState(marchand, EntityState.Modified);
        }

        public void Delete(Marchand marchand)
        {
            MsiRepo.Marchands.DeleteObject(marchand);
        }

        public List<Marchand> GetAllMarchand()
        {
            return MsiRepo.Marchands.ToList();
        }

        public List<Marchand> GetLastMarchand()
        {
            return (from dbms in MsiRepo.Marchands
                   orderby dbms.MarchandID descending
                   select dbms).ToList();
        }

        public Marchand GetMarchand(int id)
        {
            return MsiRepo.Marchands.SingleOrDefault(d => d.MarchandID == id);
        }


        public Marchand GetMarchand(string url)
        {
            return MsiRepo.Marchands.SingleOrDefault(d => d.Url == url);
        }


        public Marchand GetIdMarchand(string url)
        {
            return MsiRepo.Marchands.SingleOrDefault(d => d.Url == url);
        }


        public void Save()
        {
            MsiRepo.SaveChanges();
        }





    }
}