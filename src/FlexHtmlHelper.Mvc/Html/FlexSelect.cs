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

        public FlexSelect ForEachOption(Action<FlexTagBuilder, int, string> action)
        {
            int i = 0;
            foreach (var option in TagBuilder.Tag().Tags("option"))
            {
                action(option, i, option.TagAttributes.ContainsKey("value")? option.TagAttributes["value"] :null);
                i++;
            }
            return this;
        }
    }
}
