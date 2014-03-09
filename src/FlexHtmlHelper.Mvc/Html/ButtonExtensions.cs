using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FlexHtmlHelper.Mvc.Html
{
    public static class ButtonExtensions
    {

        public static FlexButton Button(this FHtmlHelper html)
        {
            return Button(html, type: null, text: null, value: null, name: null, htmlAttributes: null);
        }       

        public static FlexButton Button(this FHtmlHelper html, object htmlAttributes)
        {
            return Button(html, type:null, text: null, value: null, name: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexButton Button(this FHtmlHelper html, IDictionary<string, object> htmlAttributes)
        {
            return ButtonHelper(html, type:null, text: null, value: null, name: null, htmlAttributes: htmlAttributes);
        }

        public static FlexButton Button(this FHtmlHelper html, string type)
        {
            return Button(html, type, text: null, value: null, name: null, htmlAttributes: null);
        }
      
        public static FlexButton Button(this FHtmlHelper html, string type, object htmlAttributes)
        {
            return Button(html, type, text:null, value:null, name: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexButton Button(this FHtmlHelper html, string type, IDictionary<string, object> htmlAttributes)
        {
            return ButtonHelper(html, type, text:null, value:null, name: null, htmlAttributes: htmlAttributes);
        }

        public static FlexButton Button(this FHtmlHelper html, string type, string text)
        {
            return Button(html, type, text, value: null, name: null, htmlAttributes: null);
        }      

        public static FlexButton Button(this FHtmlHelper html, string type, string text, object htmlAttributes)
        {
            return Button(html, type, text, value:null, name: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexButton Button(this FHtmlHelper html, string type, string text, IDictionary<string, object> htmlAttributes)
        {
            return ButtonHelper(html, type, text, value:null, name: null, htmlAttributes: htmlAttributes);
        }

        public static FlexButton Button(this FHtmlHelper html, string type, string text, string value)
        {
            return Button(html, type, text, value, name: null, htmlAttributes: null);
        }

        public static FlexButton Button(this FHtmlHelper html, string type, string text, string value, object htmlAttributes)
        {
            return Button(html, type, text, value, name: null, htmlAttributes:HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexButton Button(this FHtmlHelper html, string type, string text, string value, IDictionary<string, object> htmlAttributes)
        {
            return ButtonHelper(html, type, text, value, name: null, htmlAttributes:htmlAttributes);
        }

        public static FlexButton Button(this FHtmlHelper html, string type, string text, string value, string name)
        {
            return Button(html, type, text, value, name, htmlAttributes:null);
        }

        public static FlexButton Button(this FHtmlHelper html, string type, string text, string value, string name, object htmlAttributes)
        {
            return Button(html, type, text, value, name, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexButton Button(this FHtmlHelper html, string type, string text, string value, string name, IDictionary<string, object> htmlAttributes)
        {
            return ButtonHelper(html, type, text, value, name, htmlAttributes);
        }

        internal static FlexButton ButtonHelper(this FHtmlHelper html, string type, string text, string value, string name, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder button = html.Render.ButtonHelper(new FlexTagBuilder(), type, text, value,name, htmlAttributes);

            return new FlexButton(html, button);
        }
    }
}
