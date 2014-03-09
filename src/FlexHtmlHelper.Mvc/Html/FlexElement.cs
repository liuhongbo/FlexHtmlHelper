using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
        }

        public FlexElement(FHtmlHelper flexHtmlHelper, string tagName)
            : this(flexHtmlHelper, new FlexTagBuilder(tagName))
        {
        }

        protected FlexElement(): this(null,(FlexTagBuilder)null)
        {

        }

        internal FlexTagBuilder TagBuilder
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
        
    }


    public static class FlexElementExtensions
    {

        public static FlexElement Rent(this FlexTagBuilder tagBuilder)
        {
            return new FlexElement((FHtmlHelper)tagBuilder.BuildContext, tagBuilder);
        }

        public static  T addClass<T>(this T flexElement, string className) where T: FlexElement
        {
            flexElement.TagBuilder.AddCssClass(className);
            return (T)flexElement;
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

        public static T disabled<T>(this T flexInput) where T : FlexInput
        {
            flexInput.Render.Disabled(flexInput.TagBuilder);
            return flexInput;
        }

        public static T active<T>(this T flexInput) where T : FlexInput
        {
            flexInput.Render.Active(flexInput.TagBuilder);
            return flexInput;
        }

        #endregion
    }
}
