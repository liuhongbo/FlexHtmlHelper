using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using FlexHtmlHelper.Mvc;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexElement: IHtmlString
    {       
        private FlexTagBuilder _tagBuilder;
        private FHtmlHelper _flexHtmlHelper;

        public FlexElement( FHtmlHelper flexHtmlHelper,FlexTagBuilder tagBuilder)
        {
            _tagBuilder = tagBuilder;
            _flexHtmlHelper = flexHtmlHelper;
            if (tagBuilder != null)
            {
                tagBuilder.BuildContext = flexHtmlHelper;
            }
        }

        public FlexElement(FHtmlHelper flexHtmlHelper, string tagName)
            : this(flexHtmlHelper, new FlexTagBuilder(tagName))
        {
        }

        protected FlexElement(): this(null,(FlexTagBuilder)null)
        {

        }

        public FlexTagBuilder TagBuilder
        {
            get { return _tagBuilder; }
        }

        internal IFlexRender Render
        {
            get { return _flexHtmlHelper.Render; }
        }

        internal FHtmlHelper FHtmlHelper
        {
            get { return _flexHtmlHelper; }
        }

        internal HtmlHelper HtmlHelper
        {
            get { return _flexHtmlHelper.HtmlHelper; }
        }       

        public string ToHtmlString()
        {
            return (_tagBuilder == null) ? string.Empty : _tagBuilder.Root.ToString();
        }

        public override string ToString()
        {
            return (_tagBuilder == null) ? string.Empty : _tagBuilder.ToString();
        }

        public void Write()
        {
            HtmlHelper.ViewContext.Writer.Write(this);
        }        

        protected static string GenerateAjaxScript(AjaxOptions ajaxOptions, string scriptFormat)
        {
            string optionsString = ToJavascriptString(ajaxOptions);
            return String.Format(CultureInfo.InvariantCulture, scriptFormat, optionsString);
        }

        protected static string ToJavascriptString(AjaxOptions ajaxOptions)
        {
            // creates a string of the form { key1: value1, key2 : value2, ... }
            StringBuilder optionsBuilder = new StringBuilder("{");
            optionsBuilder.Append(String.Format(CultureInfo.InvariantCulture, " insertionMode: {0},", InsertionModeString(ajaxOptions)));
            optionsBuilder.Append(PropertyStringIfSpecified("confirm", ajaxOptions.Confirm));
            optionsBuilder.Append(PropertyStringIfSpecified("httpMethod", ajaxOptions.HttpMethod));
            optionsBuilder.Append(PropertyStringIfSpecified("loadingElementId", ajaxOptions.LoadingElementId));
            optionsBuilder.Append(PropertyStringIfSpecified("updateTargetId", ajaxOptions.UpdateTargetId));
            optionsBuilder.Append(PropertyStringIfSpecified("url", ajaxOptions.Url));
            optionsBuilder.Append(EventStringIfSpecified("onBegin", ajaxOptions.OnBegin));
            optionsBuilder.Append(EventStringIfSpecified("onComplete", ajaxOptions.OnComplete));
            optionsBuilder.Append(EventStringIfSpecified("onFailure", ajaxOptions.OnFailure));
            optionsBuilder.Append(EventStringIfSpecified("onSuccess", ajaxOptions.OnSuccess));
            optionsBuilder.Length--;
            optionsBuilder.Append(" }");
            return optionsBuilder.ToString();
        }

        protected static string InsertionModeString(AjaxOptions ajaxOptions)
        {

            switch (ajaxOptions.InsertionMode)
            {
                case InsertionMode.Replace:
                    return "Sys.Mvc.InsertionMode.replace";
                case InsertionMode.InsertBefore:
                    return "Sys.Mvc.InsertionMode.insertBefore";
                case InsertionMode.InsertAfter:
                    return "Sys.Mvc.InsertionMode.insertAfter";
                default:
                    return ((int)ajaxOptions.InsertionMode).ToString(CultureInfo.InvariantCulture);
            }

        }

        protected static string EventStringIfSpecified(string propertyName, string handler)
        {
            if (!String.IsNullOrEmpty(handler))
            {
                return String.Format(CultureInfo.InvariantCulture, " {0}: Function.createDelegate(this, {1}),", propertyName, handler.ToString());
            }
            return String.Empty;
        }

        protected static string PropertyStringIfSpecified(string propertyName, string propertyValue)
        {
            if (!String.IsNullOrEmpty(propertyValue))
            {
                string escapedPropertyValue = propertyValue.Replace("'", @"\'");
                return String.Format(CultureInfo.InvariantCulture, " {0}: '{1}',", propertyName, escapedPropertyValue);
            }
            return String.Empty;
        }        
    }


    public static class FlexElementExtensions
    {
        public static T Add<T>(this T flexElement, FlexElement element) where T : FlexElement
        {
            flexElement.TagBuilder.Tag().AddTag(element.TagBuilder);
            return flexElement;
        }

        public static T Add<T>(this T flexElement, FlexTagBuilder tagBuilder) where T : FlexElement
        {
            flexElement.TagBuilder.Tag().AddTag(tagBuilder);
            return flexElement;
        }

        public static T Insert<T>(this T flexElement, int index, FlexElement element) where T : FlexElement
        {
            flexElement.TagBuilder.Tag().InsertTag(index, element.TagBuilder);
            return flexElement;
        }

        public static T Insert<T>(this T flexElement, int index, FlexTagBuilder tagBuilder) where T : FlexElement
        {
            flexElement.TagBuilder.Tag().InsertTag(index, tagBuilder);
            return flexElement;
        }

        public static T AddFirst<T>(this T flexElement, FlexElement element) where T : FlexElement
        {
            return flexElement.Insert(0, element);
        }

        public static T AddFirst<T>(this T flexElement, FlexTagBuilder tagBuilder) where T : FlexElement
        {
            return flexElement.Insert(0, tagBuilder);
        }

        public static  T css<T>(this T flexElement, string className) where T: FlexElement
        {
            flexElement.TagBuilder.Tag().AddCssClass(className);
            return flexElement;
        }

        public static T attr<T>(this T flexElement, string name, string value) where T : FlexElement
        {
            flexElement.TagBuilder.Tag().Attributes[name] = value;
            return flexElement;
        }        

        #region Grid System

        public static T row<T>(this T flexElement) where T:FlexElement
        {
            flexElement.Render.GridRow(flexElement.TagBuilder);
            return (T)flexElement;
        }

        public static T col_xs<T>(this T flexElement, int columns) where T: FlexElement
        {
            flexElement.Render.GridColumns(flexElement.TagBuilder, GridStyle.ExtraSmall, columns);
            return (T)flexElement;
        }

        public static T col_sm<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumns(flexElement.TagBuilder, GridStyle.Small, columns);
            return (T)flexElement;
        }

        public static T col_md<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumns(flexElement.TagBuilder, GridStyle.Medium, columns);
            return (T)flexElement;
        }

        public static T col_lg<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumns(flexElement.TagBuilder, GridStyle.Large, columns);
            return (T)flexElement;
        }

        public static T col_xs_offset<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnOffset(flexElement.TagBuilder, GridStyle.ExtraSmall, columns);
            return (T)flexElement;
        }

        public static T col_sm_offset<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnOffset(flexElement.TagBuilder, GridStyle.Small, columns);
            return (T)flexElement;
        }

        public static T col_md_offset<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnOffset(flexElement.TagBuilder, GridStyle.Medium, columns);
            return (T)flexElement;
        }

        public static T col_lg_offset<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnOffset(flexElement.TagBuilder, GridStyle.Large, columns);
            return (T)flexElement;
        }

        public static T col_xs_push<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnPush(flexElement.TagBuilder, GridStyle.ExtraSmall, columns);
            return (T)flexElement;
        }

        public static T col_sm_push<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnPush(flexElement.TagBuilder, GridStyle.Small, columns);
            return (T)flexElement;
        }

        public static T col_md_push<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnPush(flexElement.TagBuilder, GridStyle.Medium, columns);
            return (T)flexElement;
        }

        public static T col_lg_push<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnPush(flexElement.TagBuilder, GridStyle.Large, columns);
            return (T)flexElement;
        }

        public static T col_xs_pull<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnPull(flexElement.TagBuilder, GridStyle.ExtraSmall, columns);
            return (T)flexElement;
        }

        public static T col_sm_pull<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnPull(flexElement.TagBuilder, GridStyle.Small, columns);
            return (T)flexElement;
        }

        public static T col_md_pull<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnPull(flexElement.TagBuilder, GridStyle.Medium, columns);
            return (T)flexElement;
        }

        public static T col_lg_pull<T>(this T flexElement, int columns) where T : FlexElement
        {
            flexElement.Render.GridColumnPull(flexElement.TagBuilder, GridStyle.Large, columns);
            return (T)flexElement;
        }

        public static T visible_xs<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.GridColumnVisible(flexElement.TagBuilder, GridStyle.ExtraSmall, true);
            return flexElement;
        }

        public static T visible_sm<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.GridColumnVisible(flexElement.TagBuilder, GridStyle.Small, true);
            return flexElement;
        }

        public static T visible_md<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.GridColumnVisible(flexElement.TagBuilder, GridStyle.Medium, true);
            return flexElement;
        }

        public static T visible_lg<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.GridColumnVisible(flexElement.TagBuilder, GridStyle.Large, true);
            return flexElement;
        }

        public static T visible_print<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.GridColumnVisible(flexElement.TagBuilder, GridStyle.Print, true);
            return flexElement;
        }

        public static T hidden_xs<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.GridColumnVisible(flexElement.TagBuilder, GridStyle.ExtraSmall, false);
            return flexElement;
        }

        public static T hidden_sm<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.GridColumnVisible(flexElement.TagBuilder, GridStyle.Small, false);
            return flexElement;
        }

        public static T hidden_md<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.GridColumnVisible(flexElement.TagBuilder, GridStyle.Medium, false);
            return flexElement;
        }

        public static T hidden_lg<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.GridColumnVisible(flexElement.TagBuilder, GridStyle.Large, false);
            return flexElement;
        }

        public static T hidden_print<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.GridColumnVisible(flexElement.TagBuilder, GridStyle.Print, false);
            return flexElement;
        }

        #endregion        


        #region Element

        public static T disabled<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.Disabled(flexElement.TagBuilder);
            return flexElement;
        }

        public static T active<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.Active(flexElement.TagBuilder);
            return flexElement;
        }

        public static T id<T>(this T flexElement, string id) where T: FlexElement
        {
            flexElement.Render.Id(flexElement.TagBuilder, id);
            return flexElement;
        }

        #endregion


        #region Collapse

        public static T collapse<T>(this T flexElement, string target) where T : FlexElement
        {
            flexElement.Render.Collapse(flexElement.TagBuilder,target);
            return flexElement;
        }

        public static T collapsible<T>(this T flexElement) where T : FlexElement
        {
            flexElement.Render.Collapsible(flexElement.TagBuilder);
            return flexElement;
        }

        public static T collapsible<T>(this T flexElement, bool show = false) where T : FlexElement
        {
            flexElement.Render.Collapsible(flexElement.TagBuilder, show);
            return flexElement;
        }

        #endregion
    }
}
