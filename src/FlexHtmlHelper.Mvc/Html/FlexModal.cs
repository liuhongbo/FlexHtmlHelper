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

    public static class FlexModalExtensions
    {
        public static T modal_lg<T>(this T flexModal) where T : FlexModal
        {
            flexModal.Render.ModalSize(flexModal.TagBuilder, ModalSizeStyle.Large);
            return flexModal;
        } 

        public static T modal_sm<T>(this T flexModal) where T: FlexModal
        {
            flexModal.Render.ModalSize(flexModal.TagBuilder, ModalSizeStyle.Small);
            return flexModal;
        }

        public static T modal_backdrop<T>(this T flexModal, string backdrop) where T:FlexModal
        {
            flexModal.Render.ModalOption(flexModal.TagBuilder, "backdrop", backdrop);
            return flexModal;
        }

        public static T modal_keyboard<T>(this T flexModal, bool keyboard) where T:FlexModal
        {
            flexModal.Render.ModalOption(flexModal.TagBuilder,"keyboard",keyboard?"true":"false");
            return flexModal;
        }

        public static T modal_show<T>(this T flexModal, bool show) where T : FlexModal
        {
            flexModal.Render.ModalOption(flexModal.TagBuilder, "show", show ? "true" : "false");
            return flexModal;
        }

        public static T modal_remote<T>(this T flexModal, bool remote) where T: FlexModal
        {
            flexModal.Render.ModalOption(flexModal.TagBuilder, "remote", remote ? "true" : "false");
            return flexModal;
        }
    }
}
