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
        public static FHtmlHelper<TModel> f<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new FHtmlHelper<TModel>(htmlHelper);
        }

        public static FHtmlHelper<TModel> f<TModel>(this HtmlHelper<TModel> htmlHelper, string renderName)
        {
            return new FHtmlHelper<TModel>(htmlHelper,renderName);
        }

        public static FHtmlHelper<TModel> f<TModel>(this HtmlHelper<TModel> htmlHelper, IFlexRender render)
        {
            return new FHtmlHelper<TModel>(htmlHelper,render);
        }
    }
}
