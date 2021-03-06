using System.Collections.Generic;
using System.Linq;
#if FLEXHTMLHELPER_MVC
using System.Web.Mvc;
using System;
#else
using System.Web.WebPages;
#endif

namespace FlexHtmlHelper
{
    public class DefaultRender : IFlexRender
    {
        public virtual string Name
        {
            get
            {
                return "Default";
            }
        }

        #region Helper

        public virtual FlexTagBuilder LabelHelper(FlexTagBuilder tagBuilder, string @for, string text, IDictionary<string, object> htmlAttributes = null)
        {
            FlexTagBuilder tag = new FlexTagBuilder("label");
            tag.TagAttributes.Add("for", @for);
            tag.AddText(text);
            tag.MergeAttributes(htmlAttributes, replaceExisting: true);

            tagBuilder.AddTag(tag);
            return tag;
        }

        public virtual FlexTagBuilder FormHelper(FlexTagBuilder tagBuilder, string tagName, string formAction, string formMethod, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("form");
            tag.MergeAttributes(htmlAttributes);
            // action is implicitly generated, so htmlAttributes take precedence.
            tag.MergeAttribute("action", formAction);
            // method is an explicit parameter, so it takes precedence over the htmlAttributes.
            tag.MergeAttribute("method", formMethod, true);

            tagBuilder.AddTag(tag);

            return tag;
        }

        public virtual FlexTagBuilder CheckBoxHelper(FlexTagBuilder tagBuilder, string name, string @checked, string value, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("input");
            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("type", "checkbox");
            tag.MergeAttribute("name", name, true);
            if (!string.IsNullOrEmpty(@checked))
            {
                tag.MergeAttribute(@checked, null);
            }
            tag.MergeAttribute("value", value, false);
            tag.GenerateId(name);
            tagBuilder.AddTag(tag);

            FlexTagBuilder hiddenInput = new FlexTagBuilder("input");
            hiddenInput.MergeAttribute("type", "hidden");
            hiddenInput.MergeAttribute("name", name);
            hiddenInput.MergeAttribute("value", "false");

            tagBuilder.AddTag(hiddenInput);

            return tagBuilder;
        }

        public virtual FlexTagBuilder HiddenHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("input");
            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("type", "hidden");
            tag.MergeAttribute("name", name, true);            
            tag.MergeAttribute("value", value, true);
            tag.GenerateId(name);
            tagBuilder.AddTag(tag);

            return tag;
        }

        public virtual FlexTagBuilder PasswordHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("input");
            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("type", "password");
            tag.MergeAttribute("name", name, true);
            if (value != null)
            {
                tag.MergeAttribute("value", value, true);
            }
            tag.GenerateId(name);
            tagBuilder.AddTag(tag);

            return tag;
        }

        public virtual FlexTagBuilder RadioHelper(FlexTagBuilder tagBuilder, string name, string @checked, string value, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("input");
            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("type", "radio");
            tag.MergeAttribute("name", name, true);
            if (!string.IsNullOrEmpty(@checked))
            {
                tag.MergeAttribute(@checked, null);
            }
            tag.MergeAttribute("value", value, true);
            tag.GenerateId(name);
            tagBuilder.AddTag(tag);

            return tag;
        }

        public virtual FlexTagBuilder TextBoxHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("input");
            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("type", "text");
            tag.MergeAttribute("name", name, true);
            tag.MergeAttribute("value", value, true);
            tag.GenerateId(name);
            tagBuilder.AddTag(tag);

            return tag;
        }

        public virtual FlexTagBuilder StaticHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> htmlAttributes)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder ValidationMessageHelper(FlexTagBuilder tagBuilder, string validationMessage, bool isValid, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("span");
            tag.MergeAttributes(htmlAttributes);
            if (validationMessage != null)
            {
                tag.AddText(validationMessage);
            }
            tagBuilder.AddTag(tag);

            return tag;
        }

        public virtual FlexTagBuilder ValidationSummaryHelper(FlexTagBuilder tagBuilder, string validationMessage, IEnumerable<string> errorMessages, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("div");
            tag.MergeAttributes(htmlAttributes);
            if (string.IsNullOrEmpty(validationMessage))
            {
                FlexTagBuilder messageSpan = tag.AddTag("span");
                messageSpan.AddText(validationMessage);
            }
            FlexTagBuilder errorList = tag.AddTag("ul");
            if (errorMessages.Count() > 0)
            {
                foreach (var m in errorMessages)
                {
                    errorList.AddTag("li").AddText(m);
                }
            }
            else
            {
                errorList.AddTag("li").MergeAttribute("display", "none;");
            }

            tagBuilder.AddTag(tag);

            return tag;
        }

        public virtual FlexTagBuilder FormGroupHelper(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder labelTag, FlexTagBuilder inputTag, FlexTagBuilder validationMessageTag)
        {

            return tagBuilder;
        }


        protected FlexTagBuilder ListItemToOption(SelectListItemEx item)
        {
            FlexTagBuilder tag = new FlexTagBuilder("option");
            tag.AddText(item.Text);
            if (item.Value != null)
            {                
                tag.MergeAttribute("value", item.Value);
            }
            if (!string.IsNullOrEmpty(item.Selected))
            {
                tag.MergeAttribute(item.Selected, null);
            }
            return tag;
        }

        public virtual FlexTagBuilder SelectHelper(FlexTagBuilder tagBuilder, string optionLabel, string name, IEnumerable<SelectListItemEx> selectList, bool allowMultiple, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("select");

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
            {
                tag.AddTag(ListItemToOption(new SelectListItemEx() { Text = optionLabel, Value = String.Empty, Selected = null }));
            }

            foreach (var item in selectList)
            {
                tag.AddTag(ListItemToOption(item));
            }

            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("name", name, true /* replaceExisting */);
            tag.GenerateId(name);
            if (allowMultiple)
            {
                tag.MergeAttribute("multiple", "multiple");
            }

            tagBuilder.AddTag(tag);

            return tagBuilder;
        }


        public virtual FlexTagBuilder TextAreaHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> rowsAndColumns, IDictionary<string, object> htmlAttributes, string innerHtmlPrefix = null)
        {
            FlexTagBuilder tag = new FlexTagBuilder("textarea");
            
            tag.GenerateId(name);
            tag.MergeAttributes(htmlAttributes, true);
            tag.MergeAttributes(rowsAndColumns, true);
            tag.MergeAttribute("name", name, true);
            tag.AddText( (innerHtmlPrefix ?? Environment.NewLine) + value);

            tagBuilder.AddTag(tag);
            return tagBuilder;
        }

        public virtual FlexTagBuilder LinkHelper(FlexTagBuilder tagBuilder, string linkText, string url, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("a");
            tag.AddText(linkText);
            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("href", url);
            tagBuilder.AddTag(tag);
            return tagBuilder;
        }

        public virtual FlexTagBuilder ButtonHelper(FlexTagBuilder tagBuilder, string type, string text, string value, string name, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("button");
            tag.AddText(text);
            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("type", type);
            tag.MergeAttribute("value", value);
            tag.MergeAttribute("name", name);
            tagBuilder.AddTag(tag);
            return tagBuilder;
        }

        public virtual FlexTagBuilder LinkButtonHelper(FlexTagBuilder tagBuilder)
        {            
            return tagBuilder;
        }

        public virtual FlexTagBuilder PagingLinkHelper(FlexTagBuilder tagBuilder, int totalItemCount, int pageNumber, int pageSize, int maxPagingLinks, Func<int, string> pagingUrlResolver, IDictionary<string, object> htmlAttributes)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder IconHelper(FlexTagBuilder tagBuilder, string name, IDictionary<string, object> htmlAttributes)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder ModalHelper(FlexTagBuilder tagBuilder, string title, IDictionary<string, object> htmlAttributes)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder ModalHeaderHelper(FlexTagBuilder tagBuilder, FlexTagBuilder header)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder ModalBodyHelper(FlexTagBuilder tagBuilder, FlexTagBuilder body)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder ModalFooterHelper(FlexTagBuilder tagBuilder, FlexTagBuilder footer)
        {
            return tagBuilder;
        }

        #endregion

        #region Grid System

        public virtual FlexTagBuilder GridRow(FlexTagBuilder tagBuilder)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder GridColumns(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder GridColumnOffset(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder GridColumnPush(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder GridColumnPull(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder GridColumnVisible(FlexTagBuilder tagBuilder, GridStyle style, bool visible)
        {
            return tagBuilder;
        }

        #endregion


        #region Form

        public virtual FlexTagBuilder FormLayout(FlexTagBuilder tagBuilder, FormLayoutStyle layout)
        {
            return tagBuilder;
        }        

        public virtual FlexTagBuilder FormGroupHelpText(FlexTagBuilder tagBuilder, string text)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder FormGroupLabelText(FlexTagBuilder tagBuilder, string text)
        {
            return tagBuilder;
        }

        public virtual  FlexTagBuilder FormGroupValidationState(FlexTagBuilder tagBuilder, ValidationState state)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder FormGroupAddInput(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder labelTag, FlexTagBuilder inputTag)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder FormGroupInputGridColumns(FlexTagBuilder tagBuilder, FlexFormContext formContext, GridStyle style, int columns)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder FormGroupButton(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder buttonTag)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder FormGroupAddButton(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder buttonTag)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder FormGroupHeight(FlexTagBuilder tagBuilder, FormGroupHeightStyle size)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder FormGroupLink(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder linkTag)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder FormGroupAddLink(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder linkTag)
        {
            return tagBuilder;
        }

        #endregion


        #region Input

        public virtual FlexTagBuilder InputPlaceholder(FlexTagBuilder tagBuilder, string text)
        {
            tagBuilder.TagAttributes["placeholder"] = text;
            return tagBuilder;
        }        

        public virtual FlexTagBuilder InputFocus(FlexTagBuilder tagBuilder)
        {
            tagBuilder.TagAttributes["autofocus"] = "";
            return tagBuilder;
        }

        public virtual FlexTagBuilder InputHeight(FlexTagBuilder tagBuilder, InputHeightStyle size)
        {
            return tagBuilder;
        }       

        #endregion

        #region Element

        public virtual FlexTagBuilder Disabled(FlexTagBuilder tagBuilder)
        {
            tagBuilder.TagAttributes["disabled"] = "";
            return tagBuilder;
        }

        public virtual FlexTagBuilder Active(FlexTagBuilder tagBuilder)
        {
            tagBuilder.AddCssClass("active");
            return tagBuilder;
        }

        public virtual FlexTagBuilder Id(FlexTagBuilder tagBuilder, string id)
        {
            tagBuilder.TagAttributes["id"] = id;
            return tagBuilder;
        }

        public virtual FlexTagBuilder Collapse(FlexTagBuilder tagBuilder, string target)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder Collapsible(FlexTagBuilder tagBuilder, bool show = false)
        {
            return tagBuilder;
        }

        #endregion

        #region Text Helper

        public virtual FlexTagBuilder TextAlignment(FlexTagBuilder tagBuilder, TextAlignment textAlignment)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder TextTransformation(FlexTagBuilder tagBuilder, TextTransformation textTransformation)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder TextContextualColor(FlexTagBuilder tagBuilder, TextContextualColor textContextualColor)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder ContextualBackground(FlexTagBuilder tagBuilder, ContextualBackground contextualBackground)
        {
            return tagBuilder;
        }

        #endregion

        #region Button

        public virtual FlexTagBuilder ButtonSize(FlexTagBuilder tagBuilder, ButtonSizeStyle size)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder ButtonStyle(FlexTagBuilder tagBuilder, ButtonOptionStyle style)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder ButtonBlock(FlexTagBuilder tagBuilder)
        {
            return tagBuilder;
        }

        #endregion

        #region Link

        public virtual FlexTagBuilder PagingLinkSize(FlexTagBuilder tagBuilder, PagingLinkSizeStyle size)
        {
            return tagBuilder;
        }

        #endregion

        #region Modal

        public virtual FlexTagBuilder ModalSize(FlexTagBuilder tagBuilder, ModalSizeStyle size)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder ModalOption(FlexTagBuilder tagBuilder, string name, string value)
        {
            return tagBuilder;
        }

        #endregion
    }

}