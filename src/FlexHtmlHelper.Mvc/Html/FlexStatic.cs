using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexStatic : FlexInput
    {
        public FlexStatic(FHtmlHelper flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {

        }

        public FlexStatic(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        private FlexStatic()
        {

        }

        public static FlexStatic Empty = new FlexStatic();
    }
}
