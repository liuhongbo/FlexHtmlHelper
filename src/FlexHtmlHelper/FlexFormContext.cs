using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper
{
    public class FlexFormContext
    {
        public FlexFormContext()
        {
            LayoutStyle = FormLayoutStyle.Default;
            LabelColumns = new Dictionary<GridStyle, int>();
            LabelColumns.Add(GridStyle.Medium, 2);
            InputColumns = new Dictionary<GridStyle, int>();
            InputColumns.Add(GridStyle.Medium, 10);
        }

        public FormLayoutStyle LayoutStyle;
        public Dictionary<GridStyle, int> LabelColumns;
        public Dictionary<GridStyle, int> InputColumns;
    };

}