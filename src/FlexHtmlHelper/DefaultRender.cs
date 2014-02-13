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
            tag.Attributes.Add("for", @for);
            tag.AddText(text);
            tag.MergeAttributes(htmlAttributes, replaceExisting: true);

            tagBuilder.AddTag(tag);
            return tag;
        }

        public virtual FlexTagBuilder FormHelper(FlexTagBuilder tagBuilder, string formAction, string formMethod, IDictionary<string, object> htmlAttributes)
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

        public virtual FlexTagBuilder CheckBoxHelper(FlexTagBuilder tagBuilder, string name, bool isChecked, string value, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("input");
            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("type", "checkbox");
            tag.MergeAttribute("name", name, true);
            if (isChecked)
            {
                tag.MergeAttribute("checked", "checked");
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

        public virtual FlexTagBuilder RadioHelper(FlexTagBuilder tagBuilder, string name, bool isChecked, string value, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("input");
            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("type", "radio");
            tag.MergeAttribute("name", name, true);
            if (isChecked)
            {
                tag.MergeAttribute("checked", "checked");
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


        protected FlexTagBuilder ListItemToOption(SelectListItem item)
        {
            FlexTagBuilder tag = new FlexTagBuilder("option");
            tag.AddText(item.Text);
            if (item.Value != null)
            {
                tag.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                tag.Attributes["selected"] = "selected";
            }
            return tag;
        }

        public virtual FlexTagBuilder SelectHelper(FlexTagBuilder tagBuilder, string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("select");

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
            {
                tag.AddTag(ListItemToOption(new SelectListItem() { Text = optionLabel, Value = String.Empty, Selected = false }));
            }

            foreach (SelectListItem item in selectList)
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

        #endregion

        #region Grid System

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

        #endregion


        #region Form

        public virtual FlexTagBuilder FormLayout(FlexTagBuilder tagBuilder, FormLayoutStyle layout)
        {
            return tagBuilder;
        }

        public virtual FlexTagBuilder FormControl(FlexTagBuilder tagBuilder)
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

        #endregion


        #region Html

        public virtual FlexTagBuilder Placeholder(FlexTagBuilder tagBuilder, string text)
        {
            return tagBuilder;
        }

        #endregion
    }

}