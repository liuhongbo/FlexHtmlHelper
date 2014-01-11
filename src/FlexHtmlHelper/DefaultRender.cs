using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper
{
    public class DefaultRender : IFlexRender
    {
        public string Name
        {
            get
            {
                return "Default";
            }
        }

        public void GridColumns(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {

        }

        public FlexTagBuilder LabelHelper(FlexTagBuilder tagBuilder, string @for, string text, IDictionary<string, object> htmlAttributes = null)
        {
            FlexTagBuilder tag = new FlexTagBuilder("label");
            tag.Attributes.Add("for", @for);
            tag.AddText(text);
            tag.MergeAttributes(htmlAttributes, replaceExisting: true);

            tagBuilder.AddTag(tag);
            return tag;
        }
    }

}