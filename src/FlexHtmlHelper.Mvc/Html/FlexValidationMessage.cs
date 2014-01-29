using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexValidationMessage : FlexElement
    {
        public FlexValidationMessage(FHtmlHelper flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {

        }

        public FlexValidationMessage(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        public FlexValidationMessage()
        {

        }

        public static FlexValidationMessage Empty = new FlexValidationMessage();

    }
}
