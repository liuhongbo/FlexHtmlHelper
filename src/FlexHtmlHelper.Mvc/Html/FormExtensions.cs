// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. see http://aspnetwebstack.codeplex.com/SourceControl/latest#License.txt for license information.
// ported from aspnetwebstack
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace FlexHtmlHelper.Mvc.Html
{
    public static class FormExtensions
    {
        public static FlexForm Form(this FHtmlHelper htmlHelper)
        {
            // generates <form action="{current url}" method="post">...</form>
            string formAction = htmlHelper.HtmlHelper.ViewContext.HttpContext.Request.RawUrl;
            return FormHelper(htmlHelper, formAction, FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string tagName)
        {
            return FormHelper(htmlHelper, tagName, null, null, null);
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string tagName, object htmlAttributes)
        {
            return FormHelper(htmlHelper, tagName, null, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, object routeValues)
        {
            return Form(htmlHelper, null, null, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, RouteValueDictionary routeValues)
        {
            return Form(htmlHelper, null, null, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string actionName, string controllerName)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            return Form(htmlHelper, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return Form(htmlHelper, actionName, controllerName, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), method, new RouteValueDictionary());
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method)
        {
            return Form(htmlHelper, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), method, new RouteValueDictionary());
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method)
        {
            return Form(htmlHelper, actionName, controllerName, routeValues, method, new RouteValueDictionary());
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method, object htmlAttributes)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), method, htmlAttributes);
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method, object htmlAttributes)
        {
            return Form(htmlHelper, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexForm Form(this FHtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            string formAction = UrlHelper.GenerateUrl(null /* routeName */, actionName, controllerName, routeValues, htmlHelper.HtmlHelper.RouteCollection, htmlHelper.HtmlHelper.ViewContext.RequestContext, true /* includeImplicitMvcValues */);
            return FormHelper(htmlHelper, formAction, method, htmlAttributes);
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, object routeValues)
        {
            return RouteForm(htmlHelper, null /* routeName */, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, RouteValueDictionary routeValues)
        {
            return RouteForm(htmlHelper, null /* routeName */, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, string routeName)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, string routeName, object routeValues)
        {
            return RouteForm(htmlHelper, routeName, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues)
        {
            return RouteForm(htmlHelper, routeName, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, string routeName, FormMethod method)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), method, new RouteValueDictionary());
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method)
        {
            return RouteForm(htmlHelper, routeName, TypeHelper.ObjectToDictionary(routeValues), method, new RouteValueDictionary());
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method)
        {
            return RouteForm(htmlHelper, routeName, routeValues, method, new RouteValueDictionary());
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, string routeName, FormMethod method, object htmlAttributes)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, string routeName, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), method, htmlAttributes);
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method, object htmlAttributes)
        {
            return RouteForm(htmlHelper, routeName, TypeHelper.ObjectToDictionary(routeValues), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexForm RouteForm(this FHtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            string formAction = UrlHelper.GenerateUrl(routeName, null, null, routeValues, htmlHelper.HtmlHelper.RouteCollection, htmlHelper.HtmlHelper.ViewContext.RequestContext, false /* includeImplicitMvcValues */);
            return FormHelper(htmlHelper, formAction, method, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Because disposing the object would write to the response stream, you don't want to prematurely dispose of this object.")]
        private static FlexForm FormHelper(this FHtmlHelper htmlHelper, string formAction, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            return FormHelper(htmlHelper, "form", formAction, method, htmlAttributes);
        }

        private static FlexForm FormHelper(this FHtmlHelper htmlHelper, string tagName, string formAction, FormMethod? method, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder form = htmlHelper.Render.FormHelper(new FlexTagBuilder(), tagName, formAction, method.HasValue ? HtmlHelper.GetFormMethodString(method.Value) : null, htmlAttributes);

            return new FlexForm(htmlHelper, form);
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper)
        {
            // generates <form action="{current url}" method="post">...</form>
            string formAction = htmlHelper.HtmlHelper.ViewContext.HttpContext.Request.RawUrl;
            return FormHelper<TModel>(htmlHelper, formAction, FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string tagName)
        {
            return FormHelper<TModel>(htmlHelper, tagName, null, null, null);
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string tagName, object htmlAttributes)
        {
            return FormHelper<TModel>(htmlHelper, tagName, null, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, object routeValues)
        {
            return Form(htmlHelper, null, null, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, RouteValueDictionary routeValues)
        {
            return Form(htmlHelper, null, null, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string actionName, string controllerName)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string actionName, string controllerName, object routeValues)
        {
            return Form(htmlHelper, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return Form(htmlHelper, actionName, controllerName, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string actionName, string controllerName, FormMethod method)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), method, new RouteValueDictionary());
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method)
        {
            return Form(htmlHelper, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), method, new RouteValueDictionary());
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method)
        {
            return Form(htmlHelper, actionName, controllerName, routeValues, method, new RouteValueDictionary());
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string actionName, string controllerName, FormMethod method, object htmlAttributes)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string actionName, string controllerName, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), method, htmlAttributes);
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method, object htmlAttributes)
        {
            return Form(htmlHelper, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexForm<TModel> Form<TModel>(this FHtmlHelper<TModel> htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            string formAction = UrlHelper.GenerateUrl(null /* routeName */, actionName, controllerName, routeValues, htmlHelper.HtmlHelper.RouteCollection, htmlHelper.HtmlHelper.ViewContext.RequestContext, true /* includeImplicitMvcValues */);
            return FormHelper(htmlHelper, formAction, method, htmlAttributes);
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, object routeValues)
        {
            return RouteForm(htmlHelper, null /* routeName */, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, RouteValueDictionary routeValues)
        {
            return RouteForm(htmlHelper, null /* routeName */, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, string routeName)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, string routeName, object routeValues)
        {
            return RouteForm(htmlHelper, routeName, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, string routeName, RouteValueDictionary routeValues)
        {
            return RouteForm(htmlHelper, routeName, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, string routeName, FormMethod method)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), method, new RouteValueDictionary());
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, string routeName, object routeValues, FormMethod method)
        {
            return RouteForm(htmlHelper, routeName, TypeHelper.ObjectToDictionary(routeValues), method, new RouteValueDictionary());
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method)
        {
            return RouteForm(htmlHelper, routeName, routeValues, method, new RouteValueDictionary());
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, string routeName, FormMethod method, object htmlAttributes)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, string routeName, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), method, htmlAttributes);
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, string routeName, object routeValues, FormMethod method, object htmlAttributes)
        {
            return RouteForm(htmlHelper, routeName, TypeHelper.ObjectToDictionary(routeValues), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexForm<TModel> RouteForm<TModel>(this FHtmlHelper<TModel> htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            string formAction = UrlHelper.GenerateUrl(routeName, null, null, routeValues, htmlHelper.HtmlHelper.RouteCollection, htmlHelper.HtmlHelper.ViewContext.RequestContext, false /* includeImplicitMvcValues */);
            return FormHelper(htmlHelper, formAction, method, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Because disposing the object would write to the response stream, you don't want to prematurely dispose of this object.")]
        private static FlexForm<TModel> FormHelper<TModel>(this FHtmlHelper<TModel> htmlHelper, string formAction, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            return FormHelper(htmlHelper, "form", formAction, method, htmlAttributes);
        }

        private static FlexForm<TModel> FormHelper<TModel>(this FHtmlHelper<TModel> htmlHelper, string tagName, string formAction, FormMethod? method, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder form = htmlHelper.Render.FormHelper(new FlexTagBuilder(), tagName, formAction, method.HasValue ? HtmlHelper.GetFormMethodString(method.Value) : null, htmlAttributes);

            return new FlexForm<TModel>(htmlHelper, form);
        }

        public static void EndForm(this FHtmlHelper htmlHelper)
        {
            EndForm(htmlHelper.HtmlHelper.ViewContext);
        }

        internal static void EndForm(ViewContext viewContext)
        {
            viewContext.Writer.Write("</form>");
            viewContext.OutputClientValidation();
            viewContext.FormContext = null;
        }


        public static FlexMvcForm PartialForm(this FHtmlHelper htmlHelper)
        {
            var formContext = htmlHelper.FormConext ?? new FlexFormContext();
            return new FlexMvcForm(htmlHelper, formContext);
        }

        public static FlexMvcForm<TModel> PartialForm<TModel>(this FHtmlHelper<TModel> htmlHelper)
        {
            var formContext = htmlHelper.FormConext ?? new FlexFormContext();
            return new FlexMvcForm<TModel>(htmlHelper, formContext);
        }

    }
}
