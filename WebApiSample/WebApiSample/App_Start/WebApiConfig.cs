using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using WebApiSample.Repository;

namespace WebApiSample
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API の設定およびサービス
            var container = new UnityContainer();
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API ルート
#if true
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
#else
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                //handler: new SessionHttpControllerRouteHandler()
            );
#endif

            //config.Routes.MapHttpRoute(
            //    name: "UsersApi",
            //    routeTemplate: "api/{controller}/{id}/{b0}/{b1}/{b2}",
            //    defaults: new { controller = "users", id = RouteParameter.Optional, b0 = RouteParameter.Optional, b1 = RouteParameter.Optional, b2 = RouteParameter.Optional }
            //);

            config.MapHttpAttributeRoutes();
        }
    }
}
