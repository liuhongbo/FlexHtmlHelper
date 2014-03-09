using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexButton: FlexElement
    {
        public FlexButton(FHtmlHelper flexHtmlHelper,FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper,tagBuilder)
        {

        }

        public FlexButton(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper,tagName)
        {

        }

        public FlexButton()
        {

        }

        public static FlexButton Empty = new FlexButton();

    }

    public static class FlexButtonExtensions
    {
        public static T btn_lg<T>(this T flexButton) where T : FlexButton
        {
            flexButton.addClass("btn-lg");
            return flexButton;

        }
    }
}
