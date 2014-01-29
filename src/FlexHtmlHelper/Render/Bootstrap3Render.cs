using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlexHtmlHelper.Render
{
    public class Bootstrap3Render :DefaultRender, IFlexRender
    {
        public override string Name
        {
            get
            {
                return "Bootstrap3";
            }
        }

        #region Helper

        public override FlexTagBuilder LabelHelper(FlexTagBuilder tagBuilder, string @for, string text, IDictionary<string, object> htmlAttributes = null)
        {
            FlexTagBuilder tag = new FlexTagBuilder("label");
            tag.Attributes.Add("for", @for);
            tag.AddText(text);
            tag.MergeAttributes(htmlAttributes, replaceExisting: true);

            tagBuilder.AddTag(tag);
            return tag;
        }

        public override FlexTagBuilder FormHelper(FlexTagBuilder tagBuilder, string formAction, string formMethod, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("form");
            tag.MergeAttributes(htmlAttributes);
            // action is implicitly generated, so htmlAttributes take precedence.
            tag.MergeAttribute("action", formAction);
            // method is an explicit parameter, so it takes precedence over the htmlAttributes.
            tag.MergeAttribute("method", formMethod, true);

            tag.MergeAttribute("role", "form");

            tagBuilder.AddTag(tag);

            return tag;
        }

        public override FlexTagBuilder FormGroupHelper(FlexTagBuilder tagBuilder)
        {
            FlexTagBuilder tag = new FlexTagBuilder("div");

            tag.AddCssClass("form-group");

            tagBuilder.AddTag(tag);

            return tag;
        }

        #endregion

        #region Grid System

        public override FlexTagBuilder GridColumns(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            string cssClass = "";
            switch (style)
            {
                case GridStyle.ExtraSmall:
                    cssClass = "col-xs-";
                    break;
                case GridStyle.Small:
                    cssClass = "col-sm-";
                    break;
                case GridStyle.Medium:
                    cssClass = "col-md-";
                    break;
                case GridStyle.Large:
                    cssClass = "col-lg-";
                    break;
            }
            tagBuilder.AddCssClass(cssClass + columns.ToString());
            return tagBuilder;
        }

        public override FlexTagBuilder GridColumnOffset(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            string cssClass = "";
            switch (style)
            {
                case GridStyle.ExtraSmall:
                    cssClass = "col-xs-offset";
                    break;
                case GridStyle.Small:
                    cssClass = "col-sm-offset";
                    break;
                case GridStyle.Medium:
                    cssClass = "col-md-offset";
                    break;
                case GridStyle.Large:
                    cssClass = "col-lg-offset";
                    break;
            }
            tagBuilder.AddCssClass(cssClass + columns.ToString());
            return tagBuilder;
        }

        public override FlexTagBuilder GridColumnPush(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            string cssClass = "";
            switch (style)
            {
                case GridStyle.ExtraSmall:
                    cssClass = "col-xs-push";
                    break;
                case GridStyle.Small:
                    cssClass = "col-sm-push";
                    break;
                case GridStyle.Medium:
                    cssClass = "col-md-push";
                    break;
                case GridStyle.Large:
                    cssClass = "col-lg-push";
                    break;
            }
            tagBuilder.AddCssClass(cssClass + columns.ToString());
            return tagBuilder;
        }

        public override FlexTagBuilder GridColumnPull(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            string cssClass = "";
            switch (style)
            {
                case GridStyle.ExtraSmall:
                    cssClass = "col-xs-pull";
                    break;
                case GridStyle.Small:
                    cssClass = "col-sm-pull";
                    break;
                case GridStyle.Medium:
                    cssClass = "col-md-pull";
                    break;
                case GridStyle.Large:
                    cssClass = "col-lg-pull";
                    break;
            }
            tagBuilder.AddCssClass(cssClass + columns.ToString());
            return tagBuilder;
        }

        #endregion


        #region Form

        public override FlexTagBuilder FormLayout(FlexTagBuilder tagBuilder, FormLayoutStyle layout)
        {
            switch (layout)
            {
                case FormLayoutStyle.Horizontal:
                    tagBuilder.AddCssClass("form-horizontal");
                    break;
                case FormLayoutStyle.Inline:
                    tagBuilder.AddCssClass("form-inline");
                    break;
            }
            return tagBuilder;
        }

        public override FlexTagBuilder FormControl(FlexTagBuilder tagBuilder)
        {
            tagBuilder.AddCssClass("form-control");
            return tagBuilder;
        }

        #endregion
    }
}