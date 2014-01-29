using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexFormGroup: FlexElement
    {
        public FlexFormGroup(FHtmlHelper flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {

        }

        public FlexFormGroup(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        public FlexFormGroup()
        {

        }

        public static FlexFormGroup Empty = new FlexFormGroup();

    }

    public class FlexFormGroup<TModel> : FlexFormGroup
    {
        public FlexFormGroup(FHtmlHelper<TModel> flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {

        }

        public FlexFormGroup(FHtmlHelper<TModel> flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        public FlexFormGroup()
        {

        }

        public static new FlexFormGroup<TModel> Empty = new FlexFormGroup<TModel>();
    }
}
