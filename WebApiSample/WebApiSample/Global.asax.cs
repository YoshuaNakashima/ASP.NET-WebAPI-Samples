using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace WebApiSample
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SessionHttpControllerRouteHandler : HttpControllerRouteHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new SessionHttpControllerHandler(requestContext.RouteData);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SessionHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeData"></param>
        public SessionHttpControllerHandler(RouteData routeData) : base(routeData) { }
    }
}
