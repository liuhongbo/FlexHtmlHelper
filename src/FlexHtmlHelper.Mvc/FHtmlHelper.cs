using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.WebPages.Scope;
using FlexHtmlHelper.Render;

namespace FlexHtmlHelper.Mvc
{
    public class FHtmlHelper
    {        
        private HtmlHelper _htmlHelper;
        private IFlexRender _render;        

        private static ResourceManager _mvcResource = new ResourceManager("System.Web.Mvc.Properties.MvcResources", typeof(System.Web.Mvc.MvcHtmlString).Assembly);
        internal static string MvcResources_Common_ValueNotValidForProperty = "Common_ValueNotValidForProperty";
        internal static string MvcResources_SelectExtensions_InvalidExpressionParameterNoMetadata = "SelectExtensions_InvalidExpressionParameterNoMetadata";
        internal static string MvcResources_SelectExtensions_InvalidExpressionParameterNoModelType = "SelectExtensions_InvalidExpressionParameterNoModelType";
        internal static string MvcResources_SelectExtensions_InvalidExpressionParameterTypeHasFlags = "SelectExtensions_InvalidExpressionParameterTypeHasFlags";
        internal static string MvcResources_SelectExtensions_InvalidExpressionParameterType = "SelectExtensions_InvalidExpressionParameterType";
        internal static string MvcResources_HtmlHelper_MissingSelectData = "HtmlHelper_MissingSelectData";
        internal static string MvcResources_HtmlHelper_WrongSelectDataType = "HtmlHelper_WrongSelectDataType";
        internal static string MvcResources_HtmlHelper_SelectExpressionNotEnumerable = "HtmlHelper_SelectExpressionNotEnumerable";
        internal static string MvcResources_Common_NullOrEmpty = "Common_NullOrEmpty";
        internal static string MvcResources_EnumHelper_InvalidParameterType = "EnumHelper_InvalidParameterType";
        internal static string MvcResources_EnumHelper_InvalidMetadataParameter = "EnumHelper_InvalidMetadataParameter";
        internal static string MvcResources_EnumHelper_InvalidValueParameter = "EnumHelper_InvalidValueParameter";
        internal static string MvcResources_HtmlHelper_TextAreaParameterOutOfRange = "HtmlHelper_TextAreaParameterOutOfRange";
        


        private static readonly object _lastFormNumKey = new object();

        public FHtmlHelper(HtmlHelper htmlHelper):this(htmlHelper,FlexRenders.Renders.DefaultRender)
        {            

        }

        public FHtmlHelper(HtmlHelper htmlHelper, string renderName):this(htmlHelper,FlexRenders.Renders.GetRender(renderName))
        {
            
        }

        public FHtmlHelper(HtmlHelper htmlHelper, IFlexRender render)
        {
            _htmlHelper = htmlHelper;
            _render = render;
        }

        public IFlexRender Render
        {
            get { return _render; }
            set { _render = value; }
        }

        internal HtmlHelper HtmlHelper
        {
            get { return _htmlHelper; }
            set { _htmlHelper = value; }
        }

        private static int IncrementFormCount(IDictionary items)
        {
            object lastFormNum = items[_lastFormNumKey];
            int newFormNum = (lastFormNum != null) ? ((int)lastFormNum) + 1 : 0;
            items[_lastFormNumKey] = newFormNum;
            return newFormNum;
        }

        internal string FormIdGenerator()
        {
            int formNum = IncrementFormCount(HtmlHelper.ViewContext.HttpContext.Items);
            return String.Format(CultureInfo.InvariantCulture, "flexform{0}", formNum);
        }

        internal bool EvalBoolean(string key)
        {
            return Convert.ToBoolean(this.HtmlHelper.ViewData.Eval(key), CultureInfo.InvariantCulture);
        }

        internal string EvalString(string key)
        {
            return Convert.ToString(this.HtmlHelper.ViewData.Eval(key), CultureInfo.CurrentCulture);
        }

        internal string EvalString(string key, string format)
        {
            return Convert.ToString(this.HtmlHelper.ViewData.Eval(key, format), CultureInfo.CurrentCulture);
        }

        internal object GetModelStateValue(string key, Type destinationType)
        {
            ModelState modelState;
            if (this.HtmlHelper.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.ConvertTo(destinationType, null /* culture */);
                }
            }
            return null;
        }

        public static string MvcResource(string name)
        {
            return _mvcResource.GetString(name);
        }

        public FlexFormContext FormConext
        {
            get
            {
                return this.HtmlHelper.ViewData["__FLEX_FORM_CONTEXT__"] as FlexFormContext;
            }
            set
            {
                this.HtmlHelper.ViewData["__FLEX_FORM_CONTEXT__"] = value;
            }
        }
    }

    public static class FlexHtmlHelperExtentions
    {
        private static string _viewDataKey = "__fhtmlhelper__";

        public static FHtmlHelper f(this HtmlHelper htmlHelper)
        {
            string k = _viewDataKey;
            var f = (FHtmlHelper)htmlHelper.ViewData[k] ?? new FHtmlHelper(htmlHelper);
            htmlHelper.ViewData[k] = f;
            return f;
        }

        public static FHtmlHelper f(this HtmlHelper htmlHelper, string renderName)
        {
            string k = _viewDataKey + renderName;
            var f = (FHtmlHelper)htmlHelper.ViewData[k] ?? new FHtmlHelper(htmlHelper, renderName);
            htmlHelper.ViewData[k] = f;
            return f;
        }

        public static FHtmlHelper f(this HtmlHelper htmlHelper, IFlexRender render)
        {
            string k = _viewDataKey + render.Name;
            var f = (FHtmlHelper)htmlHelper.ViewData[k] ?? new FHtmlHelper(htmlHelper, render);
            htmlHelper.ViewData[k] = f;
            return f;
        }

        public static FlexTagBuilder Tag(this FHtmlHelper htmlHelper, string tagName)
        {
            var tagBuilder = new FlexTagBuilder(tagName);
            tagBuilder.BuildContext = htmlHelper;
            return tagBuilder;
        }

        public static FlexTagBuilder Tag(this FHtmlHelper htmlHelper, IHtmlString htmlString)
        {
            var tagBuilder = new FlexTagBuilder();
            tagBuilder.AddHtmlText(htmlString);
            tagBuilder.BuildContext = htmlHelper;
            return tagBuilder;
        }
    }
}
