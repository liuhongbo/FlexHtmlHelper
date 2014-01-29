using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexForm: FlexElement
    {

        public FlexForm(FHtmlHelper flexHtmlHelper,FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper,tagBuilder)
        {

        }

        public FlexForm(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper,tagName)
        {

        }

        protected FlexForm()
        {

        }
        public static FlexForm Empty = new FlexForm();

        internal FlexFormContext FormContext { get; set; }        
       
        public FlexMvcForm BeginForm()
        {
            bool traditionalJavascriptEnabled = HtmlHelper.ViewContext.ClientValidationEnabled
                                               && !HtmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled;

            if (traditionalJavascriptEnabled)
            {
                // forms must have an ID for client validation                
                TagBuilder.GenerateId(FHtmlHelper.FormIdGenerator());
            }

            HtmlHelper.ViewContext.Writer.Write(TagBuilder.ToString(FlexTagRenderMode.StartTag));
            FlexMvcForm theForm = new FlexMvcForm(this.FHtmlHelper, FormContext);
           
            if (traditionalJavascriptEnabled)
            {
                HtmlHelper.ViewContext.FormContext.FormId = TagBuilder.Attributes["id"];
            }

            return theForm;
        }

        public void EndForm()
        {
            ViewContext viewContext = HtmlHelper.ViewContext;

            viewContext.Writer.Write("</form>");
            viewContext.OutputClientValidation();
            viewContext.FormContext = null;
        }

        
    }

    public static class FlexFormExtensions
    {
        public static T Inline<T>(this T flexForm) where T:FlexForm
        {
            flexForm.Render.FormLayout(flexForm.TagBuilder, FormLayoutStyle.Inline);
            flexForm.FormContext.LayoutStyle = FormLayoutStyle.Inline;
            return flexForm;
        }

        public static T Horizontal<T>(this T flexForm) where T : FlexForm
        {
            flexForm.Render.FormLayout(flexForm.TagBuilder, FormLayoutStyle.Horizontal);
            flexForm.FormContext.LayoutStyle = FormLayoutStyle.Horizontal;
            return flexForm;
        }
    }

    public class FlexForm<TModel> : FlexForm
    {

        private FHtmlHelper<TModel> _flexHtmlHelper;

        public FlexForm(FHtmlHelper<TModel> flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {
            _flexHtmlHelper = flexHtmlHelper;
        }

        public FlexForm(FHtmlHelper<TModel> flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        protected FlexForm()
        {

        }

        public static new FlexForm<TModel> Empty = new FlexForm<TModel>();

        internal new FHtmlHelper<TModel> FHtmlHelper
        {
            get { return _flexHtmlHelper; }
        }

        public new FlexMvcForm<TModel> BeginForm()
        {
            bool traditionalJavascriptEnabled = HtmlHelper.ViewContext.ClientValidationEnabled
                                               && !HtmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled;

            if (traditionalJavascriptEnabled)
            {
                // forms must have an ID for client validation                
                TagBuilder.GenerateId(FHtmlHelper.FormIdGenerator());
            }

            HtmlHelper.ViewContext.Writer.Write(TagBuilder.ToString(FlexTagRenderMode.StartTag));
            FlexMvcForm<TModel> theForm = new FlexMvcForm<TModel>(this.FHtmlHelper, FormContext);
        
            if (traditionalJavascriptEnabled)
            {
                HtmlHelper.ViewContext.FormContext.FormId = TagBuilder.Attributes["id"];
            }

            return theForm;
        }
    }
  
}
