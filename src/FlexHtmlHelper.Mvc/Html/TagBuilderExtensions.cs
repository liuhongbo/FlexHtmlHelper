using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public static class TagBuilderExtensions
    {
        public static FlexElement AsElement(this FlexTagBuilder tagBuilder)
        {
            return new FlexElement((FHtmlHelper)tagBuilder.BuildContext, tagBuilder);
        }

        public static FlexInput AsInput(this FlexTagBuilder tagBuilder)
        {
            return new FlexInput((FHtmlHelper)tagBuilder.BuildContext, tagBuilder);
        }

        public static FlexButton AsButton(this FlexTagBuilder tagBuilder)
        {
            return new FlexButton((FHtmlHelper)tagBuilder.BuildContext, tagBuilder);
        }

        public static FlexLink AsLink(this FlexTagBuilder tagBuilder)
        {
            return new FlexLink((FHtmlHelper)tagBuilder.BuildContext, tagBuilder);
        }
    }
}
