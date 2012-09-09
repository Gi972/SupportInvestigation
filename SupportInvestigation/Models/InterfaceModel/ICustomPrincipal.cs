using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace SupportInvestigation.Models.InterfaceModel
{
    interface ICustomPrincipal: IPrincipal
    {
        int UserId { get; set; }
        string Mail { get; set; }
        string LastName { get; set; }
        string Role { get; set; }
    }
}