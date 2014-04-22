using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexModal : FlexElement
    {
        public FlexModal(FHtmlHelper flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {

        }

        public FlexModal(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        public FlexModal()
        {

        }

        public static FlexModal Empty = new FlexModal();

        public FlexModal Header(FlexTagBuilder header)
        {
            this.Render.ModalHeaderHelper(this.TagBuilder, header);
            return this;
        }

        public FlexModal Header(FlexElement header)
        {
            return Header(header.TagBuilder);
        }

        public FlexModal Body(FlexTagBuilder body)
        {
            this.Render.ModalBodyHelper(this.TagBuilder, body);
            return this;
        }

        public FlexModal Body(FlexElement body)
        {
            return Body(body.TagBuilder);
        }

        public FlexModal Footer(FlexTagBuilder footer)
        {
            this.Render.ModalFooterHelper(this.TagBuilder, footer);
            return this;
        }

        public FlexModal Footer(FlexElement footer)
        {
            return Footer(footer.TagBuilder);
        }       

    }
}
