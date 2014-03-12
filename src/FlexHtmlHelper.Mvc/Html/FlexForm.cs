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

        private FlexFormContext _formContext = new FlexFormContext();

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

        internal FlexFormContext FormContext
        {
            get { return _formContext; }
            private set { _formContext = value; }
        }
      
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
                HtmlHelper.ViewContext.FormContext.FormId = TagBuilder.TagAttributes["id"];
            }
            FHtmlHelper.FormConext = FormContext;
            return theForm;
        }

        public void EndForm()
        {
            ViewContext viewContext = HtmlHelper.ViewContext;

            viewContext.Writer.Write("</form>");
            viewContext.OutputClientValidation();
            viewContext.FormContext = null;
        }

        public FlexMvcForm PartialForm()
        {
            FHtmlHelper.FormConext = FormContext;
            return new FlexMvcForm(this.FHtmlHelper, FormContext);
        }
    }

    public static class FlexFormExtensions
    {
        public static T inline<T>(this T flexForm) where T:FlexForm
        {
            flexForm.Render.FormLayout(flexForm.TagBuilder, FormLayoutStyle.Inline);
            flexForm.FormContext.LayoutStyle = FormLayoutStyle.Inline;
            return flexForm;
        }

        public static T horizontal<T>(this T flexForm) where T : FlexForm
        {
            flexForm.Render.FormLayout(flexForm.TagBuilder, FormLayoutStyle.Horizontal);
            flexForm.FormContext.LayoutStyle = FormLayoutStyle.Horizontal;
            return flexForm;
        }

        public static T control_label_col_xs<T>(this T flexForm, int columns) where T: FlexForm
        {
            if (flexForm.FormContext.LabelColumns.Keys.Contains(GridStyle.ExtraSmall))
            {
                flexForm.FormContext.LabelColumns[GridStyle.ExtraSmall] = columns;
            }
            else
            {
                flexForm.FormContext.LabelColumns.Add(GridStyle.ExtraSmall, columns);
            }
            return flexForm;
        }

        public static T control_label_col_sm<T>(this T flexForm, int columns) where T : FlexForm
        {
            if (flexForm.FormContext.LabelColumns.Keys.Contains(GridStyle.Small))
            {
                flexForm.FormContext.LabelColumns[GridStyle.Small] = columns;
            }
            else
            {
                flexForm.FormContext.LabelColumns.Add(GridStyle.Small, columns);
            }
            return flexForm;
        }

        public static T control_label_col_md<T>(this T flexForm, int columns) where T : FlexForm
        {
            if (flexForm.FormContext.LabelColumns.Keys.Contains(GridStyle.Medium))
            {
                flexForm.FormContext.LabelColumns[GridStyle.Medium] = columns;
            }
            else
            {
                flexForm.FormContext.LabelColumns.Add(GridStyle.Medium, columns);
            }
            return flexForm;
        }

        public static T control_label_col_lg<T>(this T flexForm, int columns) where T : FlexForm
        {
            if (flexForm.FormContext.LabelColumns.Keys.Contains(GridStyle.Large))
            {
                flexForm.FormContext.LabelColumns[GridStyle.Large] = columns;
            }
            else
            {
                flexForm.FormContext.LabelColumns.Add(GridStyle.Large, columns);
            }
            return flexForm;
        }

        public static T control_col_xs<T>(this T flexForm, int columns) where T : FlexForm
        {
            if (flexForm.FormContext.InputColumns.Keys.Contains(GridStyle.ExtraSmall))
            {
                flexForm.FormContext.InputColumns[GridStyle.ExtraSmall] = columns;
            }
            else
            {
                flexForm.FormContext.InputColumns.Add(GridStyle.ExtraSmall, columns);
            }
            return flexForm;
        }

        public static T control_col_sm<T>(this T flexForm, int columns) where T : FlexForm
        {
            if (flexForm.FormContext.InputColumns.Keys.Contains(GridStyle.Small))
            {
                flexForm.FormContext.InputColumns[GridStyle.Small] = columns;
            }
            else
            {
                flexForm.FormContext.InputColumns.Add(GridStyle.Small, columns);
            }
            return flexForm;
        }

        public static T control_col_md<T>(this T flexForm, int columns) where T : FlexForm
        {
            if (flexForm.FormContext.InputColumns.Keys.Contains(GridStyle.Medium))
            {
                flexForm.FormContext.InputColumns[GridStyle.Medium] = columns;
            }
            else
            {
                flexForm.FormContext.InputColumns.Add(GridStyle.Medium, columns);
            }
            return flexForm;
        }

        public static T control_col_lg<T>(this T flexForm, int columns) where T : FlexForm
        {
            if (flexForm.FormContext.InputColumns.Keys.Contains(GridStyle.Large))
            {
                flexForm.FormContext.InputColumns[GridStyle.Large] = columns;
            }
            else
            {
                flexForm.FormContext.InputColumns.Add(GridStyle.Large, columns);
            }
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
                HtmlHelper.ViewContext.FormContext.FormId = TagBuilder.TagAttributes["id"];
            }

            FHtmlHelper.FormConext = FormContext;

            return theForm;
        }

        public new FlexMvcForm<TModel> PartialForm()
        {
            FHtmlHelper.FormConext = FormContext;
            return new FlexMvcForm<TModel>(this.FHtmlHelper, FormContext);
        }
    }
  
}
