using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FlexHtmlHelper.Mvc.Html
{
    public static class IconExtensions
    {
        public static FlexIcon Icon(this FHtmlHelper htmlHelper, string name)
        {
            return Icon(htmlHelper, name, htmlAttributes:null);
        }

        public static FlexIcon Icon(this FHtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            return Icon(htmlHelper, name, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexIcon Icon(this FHtmlHelper htmlHelper, string name, IDictionary<string, object> htmlAttributes)
        {
            return IconHelper(htmlHelper, name, htmlAttributes);
        }

        internal static FlexIcon IconHelper(this FHtmlHelper htmlHelper, string name, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder icon = htmlHelper.Render.IconHelper(new FlexTagBuilder(), name, htmlAttributes);
            return new FlexIcon(htmlHelper, icon);    
        }
    }
}
