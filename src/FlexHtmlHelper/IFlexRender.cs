using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper
{
    public enum GridStyle{
        ExtraSmall,
        Small,
        Medium,
        Large
    }


    public enum FormLayoutStyle{
        Default,
        Inline,
        Horizontal
    }

    public interface IFlexRender
    {
        string Name { get; }


        #region Helper

        FlexTagBuilder LabelHelper(FlexTagBuilder tagBuilder, string @for, string text, IDictionary<string, object> htmlAttributes = null);
        FlexTagBuilder FormHelper(FlexTagBuilder tagBuilder, string formAction, string formMethod, IDictionary<string, object> htmlAttributes);
        FlexTagBuilder CheckBoxHelper(FlexTagBuilder tagBuilder, string name, bool isChecked, string value, IDictionary<string, object> htmlAttributes);
        FlexTagBuilder HiddenHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> htmlAttributes);
        FlexTagBuilder PasswordHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> htmlAttributes);
        FlexTagBuilder RadioHelper(FlexTagBuilder tagBuilder, string name, bool isChecked, string value, IDictionary<string, object> htmlAttributes);
        FlexTagBuilder TextBoxHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> htmlAttributes);
        FlexTagBuilder ValidationMessageHelper(FlexTagBuilder tagBuilder, string validationMessage, bool isValid, IDictionary<string, object> htmlAttributes);
        FlexTagBuilder ValidationSummaryHelper(FlexTagBuilder tagBuilder, string validationMessage, IEnumerable<string> errorMessages, IDictionary<string, object> htmlAttributes);
        FlexTagBuilder FormGroupHelper(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder labelTag, FlexTagBuilder inputTag, FlexTagBuilder validationMessageTag);

        #endregion

        #region Grid System

        FlexTagBuilder GridColumns(FlexTagBuilder tagBuilder, GridStyle style, int columns);
        FlexTagBuilder GridColumnOffset(FlexTagBuilder tagBuilder, GridStyle style, int columns);
        FlexTagBuilder GridColumnPush(FlexTagBuilder tagBuilder, GridStyle style, int columns);
        FlexTagBuilder GridColumnPull(FlexTagBuilder tagBuilder, GridStyle style, int columns);

        #endregion

        #region Form

        FlexTagBuilder FormLayout(FlexTagBuilder tagBuilder, FormLayoutStyle layout);
        FlexTagBuilder FormControl(FlexTagBuilder tagBuilder);
        FlexTagBuilder FormGroupHelpText(FlexTagBuilder tagBuilder, string text);

        #endregion

        #region Html

        FlexTagBuilder Placeholder(FlexTagBuilder tagBuilder, string text);

        #endregion

    }
}
