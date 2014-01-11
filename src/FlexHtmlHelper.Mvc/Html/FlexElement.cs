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
    public abstract class FlexElement<T>: IHtmlString where T : FlexElement<T>
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

        protected FlexTagBuilder TagBuilder
        {
            get { return _tagBuilder; }
        }

        protected IFlexRender Render
        {
            get { return _flexHtmlHelper.Render; }
        }

        protected HtmlHelper HtmlHelper
        {
            get { return _flexHtmlHelper.HtmlHelper; }
        }       

        public string ToHtmlString()
        {
            return (_tagBuilder == null) ? string.Empty : _tagBuilder.RootTag.ToString();
        }

        public override string ToString()
        {
            return (_tagBuilder == null) ? string.Empty : _tagBuilder.ToString();
        }

        public T addClass(string className)
        {
            _tagBuilder.AddCssClass(className);
            return (T)this;
        }

        #region Grid System

        public T row()
        {

            return (T)this;
        }

        public T col_xs(int columns)
        {
            Render.GridColumns(_tagBuilder, GridStyle.ExtraSmall, columns);
            return (T)this;
        }
        
        public T col_sm(int columns)
        {
            return (T)this;
        }

        public T col_md(int columns)
        {
            return (T)this;
        }

        public T col_lg(int columns)
        {
            return (T)this;
        }

        public T col_xs_offset(int columns)
        {
            return (T)this;
        }

        public T col_sm_offset(int columns)
        {
            return (T)this;
        }

        public T col_md_offset(int columns)
        {
            return (T)this;
        }

        public T col_lg_offset(int columns)
        {
            return (T)this;
        }

        #endregion

        
    }
}
