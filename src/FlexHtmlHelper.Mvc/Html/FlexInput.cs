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
        public static T FormControl<T>(this T flexInput) where T: FlexInput
        {
            flexInput.Render.FormControl(flexInput.TagBuilder);
            return flexInput;
        }
    }
}
