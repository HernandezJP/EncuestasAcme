using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EncuestasAcme.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly string[] _rolesPermitidos;

        public AuthorizeRoleAttribute(params string[] rolesPermitidos)
        {
            _rolesPermitidos = rolesPermitidos ?? new string[0];
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                return false;
            }

            if (httpContext.User == null || !httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var rolSesion = httpContext.Session != null
                ? Convert.ToString(httpContext.Session["ROL_Nombre"])
                : null;

            if (string.IsNullOrWhiteSpace(rolSesion))
            {
                return false;
            }

            if (_rolesPermitidos.Length == 0)
            {
                return true;
            }

            return _rolesPermitidos.Any(r =>
                string.Equals(r, rolSesion, StringComparison.OrdinalIgnoreCase));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Auth",
                        action = "AccesoDenegado"
                    })
                );
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}