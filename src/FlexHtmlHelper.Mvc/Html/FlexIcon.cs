using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexIcon: FlexElement
    {
        public FlexIcon(FHtmlHelper flexHtmlHelper,FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper,tagBuilder)
        {

        }

        public FlexIcon(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper,tagName)
        {

        }

        public FlexIcon()
        {

        }

        public static FlexIcon Empty = new FlexIcon();

    }
}
