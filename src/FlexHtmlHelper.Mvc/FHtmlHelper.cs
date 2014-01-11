using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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

    }

    public static class FlexHtmlHelperExtentions
    {
        public static FHtmlHelper f(this HtmlHelper htmlHelper)
        {
            return new FHtmlHelper(htmlHelper);
        }

        public static FHtmlHelper f(this HtmlHelper htmlHelper, string renderName)
        {
            return new FHtmlHelper(htmlHelper,renderName);
        }

        public static FHtmlHelper f(this HtmlHelper htmlHelper, IFlexRender render)
        {
            return new FHtmlHelper(htmlHelper, render);
        }
    }
}
