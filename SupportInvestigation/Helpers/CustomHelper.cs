using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using SupportInvestigation.Models.CustomView;
using SupportInvestigation.Models.InterfaceModel;
using SupportInvestigation.Models.Repository;
using System.Linq.Expressions;
using SupportInvestigation.Models.Model;

namespace SupportInvestigation.Helpers
{
    public static class CustomHelper
    {
        public static MvcHtmlString DropDownUser(this HtmlHelper html)
        {
            IRepoUser MsiRepoUser = new RepoUser();
            List<User> users = new List<User>();
            users = MsiRepoUser.GetAllUser().ToList();
            var userbox = new List<SelectListItem>();
            userbox.Add(new SelectListItem { Value = "-1", Text = "Please choose" });
            foreach (var item in users)
            {
                userbox.Add(new SelectListItem { Value = item.UserID.ToString(), Text = item.Login });
            };
            return html.DropDownList("UserID", userbox);
           
        }


        public static List<SelectListItem> DDPmarchandBox(this HtmlHelper helper)
        {
            IRepoMarchand MsiRepoMarchand = new RepoMarchand();
            TicketsAddViewModel ticket = new TicketsAddViewModel();
            ticket.marchands = MsiRepoMarchand.GetAllMarchand();
            var marchanbox = new List<SelectListItem>();
            marchanbox.Add(new SelectListItem { Value = "0", Text = "Please choose" });
            foreach (var item in ticket.marchands)
            {
                marchanbox.Add(new SelectListItem { Value = item.MarchandID.ToString(), Text = item.Url });
            };

            return marchanbox;
        }


        public static MvcHtmlString DropDownMarchand (this HtmlHelper html) { 
         
            IRepoMarchand MsiRepoMarchand = new RepoMarchand();
            TicketsAddViewModel ticket = new TicketsAddViewModel();
            ticket.marchands = MsiRepoMarchand.GetAllMarchand();
            var marchanbox = new List<SelectListItem>();
            marchanbox.Add(new SelectListItem { Value = "0", Text = "Please choose" });
            foreach (var item in ticket.marchands)
            {
                marchanbox.Add(new SelectListItem { Value = item.MarchandID.ToString(), Text = item.Url });
            };

            return html.DropDownList("Marchand",marchanbox);
            
        }


        public static MvcHtmlString DropDownMarchandFor<TModel, Tvalue>(this HtmlHelper<TModel> html, Expression<Func<TModel, Tvalue>> expression)
        {

            IRepoMarchand MsiRepoMarchand = new RepoMarchand();
            TicketsAddViewModel ticket = new TicketsAddViewModel();
            ticket.marchands = MsiRepoMarchand.GetAllMarchand();
            var marchanbox = new List<SelectListItem>();
            marchanbox.Add(new SelectListItem { Value = "0", Text = "Please choose" });
            foreach (var item in ticket.marchands)
            {
                marchanbox.Add(new SelectListItem { Value = item.MarchandID.ToString(), Text = item.Url });
            };

            return html.DropDownListFor(expression, marchanbox);

        }


        public static MvcHtmlString DropDownLevelAdmin(this HtmlHelper html) { 
                    
            Dictionary<int,string> RoleList = new Dictionary<int,string>()
            {
                {-1, "Veuillez choisir un rôle"},
                {0,"Administrateur"},
                {1,"utilisateur"}
            };

        
        return html.DropDownList("Level", new SelectList(RoleList,"key","Value"));
        }

        public static MvcHtmlString DropDownLevelAdminFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {

            Dictionary<int, string> RoleList = new Dictionary<int, string>()
            {
                {-1, "Veuillez choisir un rôle"},
                {0,"Administrateur"},
                {1,"utilisateur"}
            };


            return html.DropDownListFor(expression, new SelectList(RoleList, "key", "Value"));
        }

        //exemple
        public static MvcHtmlString StateDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            Dictionary<string, string> stateList = new Dictionary<string, string>()
        {
            {"AL"," Alabama"},
            {"AK"," Alaska"},
            {"AZ"," Arizona"},
            {"AR"," Arkansas"},
            {"CA"," California"},
            {"CO"," Colorado"},
            {"CT"," Connecticut"},
            {"DE"," Delaware"},
            {"FL"," Florida"},
            {"GA"," Georgia"},
            {"HI"," Hawaii"},
            {"ID"," Idaho"},
            {"IL"," Illinois"},
            {"IN"," Indiana"},
            {"IA"," Iowa"},
            {"KS"," Kansas"},
            {"KY"," Kentucky"},
            {"LA"," Louisiana"},
            {"ME"," Maine"},
            {"MD"," Maryland"},
            {"MA"," Massachusetts"},
            {"MI"," Michigan"},
            {"MN"," Minnesota"},
            {"MS"," Mississippi"},
            {"MO"," Missouri"},
            {"MT"," Montana"},
            {"NE"," Nebraska"},
            {"NV"," Nevada"},
            {"NH"," New Hampshire"},
            {"NJ"," New Jersey"},
            {"NM"," New Mexico"},
            {"NY"," New York"},
            {"NC"," North Carolina"},
            {"ND"," North Dakota"},
            {"OH"," Ohio"},
            {"OK"," Oklahoma"},
            {"OR"," Oregon"},
            {"PA"," Pennsylvania"},
            {"RI"," Rhode Island"},
            {"SC"," South Carolina"},
            {"SD"," South Dakota"},
            {"TN"," Tennessee"},
            {"TX"," Texas"},
            {"UT"," Utah"},
            {"VT"," Vermont"},
            {"VA"," Virginia"},
            {"WA"," Washington"},
            {"WV"," West Virginia"},
            {"WI"," Wisconsin"},
            {"WY"," Wyoming"},
            {"AS"," American Samoa"},
            {"DC"," District of Columbia"},
            {"FM"," Federated States of Micronesia"},
            {"MH"," Marshall Islands"},
            {"MP"," Northern Mariana Islands"},
            {"PW"," Palau"},
            {"PR"," Puerto Rico"},
            {"VI"," Virgin Islands"},
            {"GU"," Guam"}
        };
            return html.DropDownListFor(expression, new SelectList(stateList, "key", "value"));
        }


        public static int LoginUserID(this HtmlHelper html ) {

            IRepoUser user = new RepoUser();




            return 1;
        }
    }
}