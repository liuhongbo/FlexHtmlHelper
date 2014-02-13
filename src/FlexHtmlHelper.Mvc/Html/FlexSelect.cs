using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexSelect: FlexInput
    {
        public FlexSelect(FHtmlHelper flexHtmlHelper,FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper,tagBuilder)
        {

        }

        public FlexSelect(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper,tagName)
        {

        }

        public FlexSelect()
        {

        }

        public static FlexSelect Empty = new FlexSelect();

    }
}
