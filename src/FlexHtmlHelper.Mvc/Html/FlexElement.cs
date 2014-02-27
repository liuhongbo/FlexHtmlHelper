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
    public abstract class FlexElement: IHtmlString
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
        public static  T addClass<T>(this T flexElement, string className) where T: FlexElement
        {
            flexElement.TagBuilder.AddCssClass(className);
            return (T)flexElement;
        }
       
        #region Grid System

        public static T row<T>(this T flexElement) where T:FlexElement
        {
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

        #endregion        
    }
}
