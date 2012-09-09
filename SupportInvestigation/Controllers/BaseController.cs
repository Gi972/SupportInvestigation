﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportInvestigation.Models.Repository;

namespace SupportInvestigation.Controllers
{
    public class BaseController : Controller
    {   
            protected virtual new CustomPrincipal User
            {
                get { return HttpContext.User as CustomPrincipal; }
            }
        

    }
}