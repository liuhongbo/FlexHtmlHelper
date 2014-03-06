using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexInput: FlexElement
    {
        public FlexInput(FHtmlHelper flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {

        }

        public FlexInput(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        internal FlexInput()
        {

        }        
    }

    public static class FlexInputExtensions
    {

        public static T placeholder<T>(this T flexInput, string text) where T : FlexInput
        {
            flexInput.Render.InputPlaceholder(flexInput.TagBuilder, text);
            return flexInput;
        }
        
        public static T focus<T>(this T flexInput) where T : FlexInput
        {
            flexInput.Render.InputFocus(flexInput.TagBuilder);
            return flexInput;
        }

        public static T large<T>(this T flexInput) where T : FlexInput
        {
            flexInput.Render.InputHeight(flexInput.TagBuilder, InputHeightStyle.Large);
            return flexInput;
        }

        public static T small<T>(this T flexInput) where T : FlexInput
        {
            flexInput.Render.InputHeight(flexInput.TagBuilder, InputHeightStyle.Small);
            return flexInput;
        }
    }
}
