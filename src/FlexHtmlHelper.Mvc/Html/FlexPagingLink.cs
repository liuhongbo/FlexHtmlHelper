using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexPagingLink : FlexElement
    {
        public FlexPagingLink(FHtmlHelper flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {

        }

        public FlexPagingLink(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        public FlexPagingLink()
        {

        }

        public static FlexPagingLink Empty = new FlexPagingLink();
    }


    public static class FlexPagingLinkExtensions
    {
        public static T pagination_lg<T>(this T flexPagingLink) where T : FlexPagingLink
        {
            flexPagingLink.Render.PagingLinkSize(flexPagingLink.TagBuilder, PagingLinkSizeStyle.Large);
            return flexPagingLink;
        }

        public static T pagination_sm<T>(this T flexPagingLink) where T : FlexPagingLink
        {
            flexPagingLink.Render.PagingLinkSize(flexPagingLink.TagBuilder, PagingLinkSizeStyle.Small);
            return flexPagingLink;
        }
    }
}
