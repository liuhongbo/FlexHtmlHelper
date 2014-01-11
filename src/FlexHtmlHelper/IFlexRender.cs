using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper
{
    public enum GridStyle{
        ExtraSmall,
        Small,
        Medium,
        Large
    }

    public interface IFlexRender
    {
        string Name { get; }

        FlexTagBuilder LabelHelper(FlexTagBuilder tagBuilder, string @for, string text, IDictionary<string, object> htmlAttributes = null);

        void GridColumns(FlexTagBuilder tagBuilder, GridStyle style, int columns);
        //void GridColumnOffset(FlexTagBuilder tagBuilder);
    }
}
