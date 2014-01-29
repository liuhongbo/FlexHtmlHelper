using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{  
    public class FlexValidationSummary : FlexElement
    {
        public FlexValidationSummary(FHtmlHelper flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {

        }

        public FlexValidationSummary(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        private FlexValidationSummary()
        {

        }

        public static FlexValidationSummary Empty = new FlexValidationSummary();

    }
}
