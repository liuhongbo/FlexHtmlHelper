using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Ajax;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexLink: FlexElement
    {
        private const string LinkOnClickFormat = "Sys.Mvc.AsyncHyperlink.handleClick(this, new Sys.UI.DomEvent(event), {0});";

        public FlexLink(FHtmlHelper flexHtmlHelper,FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper,tagBuilder)
        {

        }

        public FlexLink(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper,tagName)
        {

        }

        public FlexLink()
        {

        }

        public static FlexLink Empty = new FlexLink();

        public FlexButton Button()
        {
            var btn = new FlexButton(this.FHtmlHelper, this.TagBuilder);
            Render.LinkButtonHelper(this.TagBuilder);
            return btn;
        }

        public FlexLink Ajax(AjaxOptions ajaxOptions)
        {
            if (HtmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
            {
                this.TagBuilder.Tag().MergeAttributes(ajaxOptions.ToUnobtrusiveHtmlAttributes());
            }
            else
            {
                this.TagBuilder.Tag().MergeAttribute("onclick", GenerateAjaxScript(ajaxOptions, LinkOnClickFormat));
            }
            return this;
        }        
    }
}
