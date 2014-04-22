using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FlexHtmlHelper.Mvc.Html
{
    public static class ModalExtensions
    {
        public static FlexModal Modal(this FHtmlHelper htmlHelper)
        {
            return Modal(htmlHelper, title: null, htmlAttributes: null);
        }

        public static FlexModal Modal(this FHtmlHelper htmlHelper, string title)
        {
            return ModalHelper(htmlHelper, title, htmlAttributes: null);
        }

        public static FlexModal Modal(this FHtmlHelper htmlHelper, string title, object htmlAttributes)
        {
            return ModalHelper(htmlHelper, title, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexModal Modal(this FHtmlHelper htmlHelper, string title, IDictionary<string, object> htmlAttributes)
        {
            return ModalHelper(htmlHelper, title, htmlAttributes);
        }

        internal static FlexModal ModalHelper(this FHtmlHelper htmlHelper, string title, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder modal = htmlHelper.Render.ModalHelper(new FlexTagBuilder(), title, htmlAttributes);
            return new FlexModal(htmlHelper, modal);
        }
    }
}
