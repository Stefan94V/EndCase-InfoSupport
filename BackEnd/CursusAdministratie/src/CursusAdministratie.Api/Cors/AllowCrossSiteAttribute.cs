using System;
using System.Web.Mvc;


namespace CursusAdministratie.Api.Cors
{
    public class AllowCrossSiteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:4200");
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "*");
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            filterContext.RequestContext.HttpContext.Response.AddHeader("Vary", "Origin");

            base.OnActionExecuting(filterContext);
        }
    }
}