// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Web.Mvc;
using System.Web.Routing;
using FlexHtmlHelper.Render;

namespace FlexHtmlHelper.Mvc
{
    public class FHtmlHelper<TModel> : FHtmlHelper
    {
        private HtmlHelper<TModel> _htmlHelper;

        public FHtmlHelper(HtmlHelper<TModel> htmlHelper):base(htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public FHtmlHelper(HtmlHelper<TModel> htmlHelper,string renderName)
            : base(htmlHelper,renderName)
        {
            _htmlHelper = htmlHelper;           
        }

        public FHtmlHelper(HtmlHelper<TModel> htmlHelper,IFlexRender render)
            : base(htmlHelper,render)
        {
            _htmlHelper = htmlHelper;
        }

        internal new HtmlHelper<TModel> HtmlHelper
        {
            get { return _htmlHelper; }
            set { _htmlHelper = value; }
        }
     
    }

    public static class FlexHtmlHelperForModelExtentions
    {
        private static string _viewDataKey = "__fhtmlhelper_model__";

        public static FHtmlHelper<TModel> f<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            string k = _viewDataKey + typeof(TModel).Name;
            var f = (FHtmlHelper<TModel>)htmlHelper.ViewData[k] ?? new FHtmlHelper<TModel>(htmlHelper);
            htmlHelper.ViewData[k] = f;
            return f;
        }

        public static FHtmlHelper<TModel> f<TModel>(this HtmlHelper<TModel> htmlHelper, string renderName)
        {
            string k = _viewDataKey + typeof(TModel).Name + "_" + renderName;
            var f = (FHtmlHelper<TModel>)htmlHelper.ViewData[k] ?? new FHtmlHelper<TModel>(htmlHelper, renderName);
            htmlHelper.ViewData[k] = f;
            return f;           
        }

        public static FHtmlHelper<TModel> f<TModel>(this HtmlHelper<TModel> htmlHelper, IFlexRender render)
        {
            string k = _viewDataKey + typeof(TModel).Name + "_" + render.Name;
            var f = (FHtmlHelper<TModel>)htmlHelper.ViewData[k] ?? new FHtmlHelper<TModel>(htmlHelper, render);
            htmlHelper.ViewData[k] = f;
            return f;    
        }
    }
}
