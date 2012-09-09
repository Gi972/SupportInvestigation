using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SupportInvestigation.Models.InterfaceModel;
using System.Security.Principal;

namespace SupportInvestigation.Models.Repository
{
    public class CustomPrincipal : ICustomPrincipal
    {

        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
            
        }

        
        public IIdentity Identity{ get; private set; }


        public bool IsInRole(string role) {
            if (role == Role)
            {
                return true;
            }
            
            return false; }

        public int UserId { get; set; }
        public string Mail { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

     
    }
}