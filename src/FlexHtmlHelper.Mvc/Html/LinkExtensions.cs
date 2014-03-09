// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.WebPages;

namespace FlexHtmlHelper.Mvc.Html
{
    public static class LinkExtensions
    {
        public static FlexLink ActionLink(this FHtmlHelper htmlHelper, string linkText, string actionName)
        {
            return ActionLink(htmlHelper, linkText, actionName, null /* controllerName */, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static FlexLink ActionLink(this FHtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
        {
            return ActionLink(htmlHelper, linkText, actionName, null /* controllerName */, TypeHelper.ObjectToDictionary(routeValues), new RouteValueDictionary());
        }

        public static FlexLink ActionLink(this FHtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes)
        {
            return ActionLink(htmlHelper, linkText, actionName, null /* controllerName */, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexLink ActionLink(this FHtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues)
        {
            return ActionLink(htmlHelper, linkText, actionName, null /* controllerName */, routeValues, new RouteValueDictionary());
        }

        public static FlexLink ActionLink(this FHtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return ActionLink(htmlHelper, linkText, actionName, null /* controllerName */, routeValues, htmlAttributes);
        }

        public static FlexLink ActionLink(this FHtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            return ActionLink(htmlHelper, linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static FlexLink ActionLink(this FHtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            return ActionLink(htmlHelper, linkText, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexLink ActionLink(this FHtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException(FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_Common_NullOrEmpty), "linkText");
            }
            return htmlHelper.GenerateLink(htmlHelper.HtmlHelper.ViewContext.RequestContext, htmlHelper.HtmlHelper.RouteCollection, linkText, null /* routeName */, actionName, controllerName, routeValues, htmlAttributes);
        }

        public static FlexLink ActionLink(this FHtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            return ActionLink(htmlHelper, linkText, actionName, controllerName, protocol, hostName, fragment, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexLink ActionLink(this FHtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException(FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_Common_NullOrEmpty), "linkText");
            }
            return htmlHelper.GenerateLink(htmlHelper.HtmlHelper.ViewContext.RequestContext, htmlHelper.HtmlHelper.RouteCollection, linkText, null /* routeName */, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes);
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, object routeValues)
        {
            return RouteLink(htmlHelper, linkText, TypeHelper.ObjectToDictionary(routeValues));
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues)
        {
            return RouteLink(htmlHelper, linkText, routeValues, new RouteValueDictionary());
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, string routeName)
        {
            return RouteLink(htmlHelper, linkText, routeName, (object)null /* routeValues */);
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, string routeName, object routeValues)
        {
            return RouteLink(htmlHelper, linkText, routeName, TypeHelper.ObjectToDictionary(routeValues));
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues)
        {
            return RouteLink(htmlHelper, linkText, routeName, routeValues, new RouteValueDictionary());
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, object routeValues, object htmlAttributes)
        {
            return RouteLink(htmlHelper, linkText, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return RouteLink(htmlHelper, linkText, null /* routeName */, routeValues, htmlAttributes);
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
        {
            return RouteLink(htmlHelper, linkText, routeName, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException(FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_Common_NullOrEmpty), "linkText");
            }
            return htmlHelper.GenerateRouteLink(htmlHelper.HtmlHelper.ViewContext.RequestContext, htmlHelper.HtmlHelper.RouteCollection, linkText, routeName, routeValues, htmlAttributes);
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            return RouteLink(htmlHelper, linkText, routeName, protocol, hostName, fragment, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexLink RouteLink(this FHtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException(FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_Common_NullOrEmpty), "linkText");
            }
            return htmlHelper.GenerateRouteLink(htmlHelper.HtmlHelper.ViewContext.RequestContext, htmlHelper.HtmlHelper.RouteCollection, linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes);
        }

        #region Generate Link Helper

        public static FlexLink GenerateLink(this FHtmlHelper html, RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return html.GenerateLink(requestContext, routeCollection, linkText, routeName, actionName, controllerName, null /* protocol */, null /* hostName */, null /* fragment */, routeValues, htmlAttributes);
        }

        public static FlexLink GenerateLink(this FHtmlHelper html, RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return html.GenerateLinkInternal(requestContext, routeCollection, linkText, routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes, true /* includeImplicitMvcValues */);
        }

        private static FlexLink GenerateLinkInternal(this FHtmlHelper html, RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool includeImplicitMvcValues)
        {
            string url = UrlHelper.GenerateUrl(routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues);
            FlexTagBuilder tagBuilder = html.Render.LinkHelper(new FlexTagBuilder(), linkText, url, htmlAttributes);
            return new FlexLink(html, tagBuilder);
        }

        public static FlexLink GenerateRouteLink(this FHtmlHelper html, RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return html.GenerateRouteLink(requestContext, routeCollection, linkText, routeName, null /* protocol */, null /* hostName */, null /* fragment */, routeValues, htmlAttributes);
        }

        public static FlexLink GenerateRouteLink(this FHtmlHelper html, RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return html.GenerateLinkInternal(requestContext, routeCollection, linkText, routeName, null /* actionName */, null /* controllerName */, protocol, hostName, fragment, routeValues, htmlAttributes, false /* includeImplicitMvcValues */);
        }

        #endregion
    }
}
