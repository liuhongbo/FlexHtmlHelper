using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public abstract class FlexInput: FlexElement
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
            flexInput.Render.Placeholder(flexInput.TagBuilder, text);
            return flexInput;
        }

        public static T disabled<T>(this T flexInput) where T : FlexInput
        {
            flexInput.Render.Disabled(flexInput.TagBuilder);
            return flexInput;
        }
        public static T focus<T>(this T flexInput) where T : FlexInput
        {
            flexInput.Render.Focus(flexInput.TagBuilder);
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
