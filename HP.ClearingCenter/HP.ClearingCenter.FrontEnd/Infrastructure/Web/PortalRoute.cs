using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Web
{
    public class PortalRoute : System.Web.Routing.Route
    {
        public PortalRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler) { }
        public PortalRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler) { }
        public PortalRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler) { }
        public PortalRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler) { }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData path = base.GetVirtualPath(requestContext, values);

            if (path != null)
            {
                AppendTrailingSlash(path);
            }

            return path;
        }

        private void AppendTrailingSlash(VirtualPathData path)
        {
            // don't append trailing slash if its empty.
            if (path.VirtualPath.IsNullOrEmpty())
            {
                return;
            }

            // append the slash before the query string starts
            if (path.VirtualPath.Contains('?'))
            {
                var queryStartIndex = path.VirtualPath.IndexOf('?');
                var newVirtualPath = path.VirtualPath.Insert(queryStartIndex, "/");
                path.VirtualPath = newVirtualPath;
                return;
            }

            // append it if necessary.
            path.VirtualPath = path.VirtualPath + "/";
        }
    }

    public class LocalProgramRoute : PortalRoute
    {
        public const string COUNTRY = "country";
        public const string LANGUAGE = "language";

        static LocalProgramRoute()
        {
            URL = "{{{0}}}/{{{1}}}/{{controller}}/{{action}}/{{id}}"
                .WithTokens(COUNTRY, LANGUAGE);
        }

        private static readonly string URL;

        public LocalProgramRoute()
            : base(URL, new MvcRouteHandler())
        {
            Defaults = new RouteValueDictionary(new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            });
        }
    }

    public static class RouteCollectionExtensions
    {
        public static Route MapPortalRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return routes.MapPortalRoute(name, url, defaults, null);
        }

        public static Route MapPortalRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");

            if (url == null)
                throw new ArgumentNullException("url");

            var route = new PortalRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            };

            MapPortalRoute(routes, name, route);

            return route;
        }

        public static Route MapPortalRoute(this RouteCollection routes, string name, PortalRoute portalRoute)
        {
            if (string.IsNullOrEmpty(name))
                routes.Add(portalRoute);
            else
                routes.Add(name, portalRoute);

            return portalRoute;
        }
    }
}