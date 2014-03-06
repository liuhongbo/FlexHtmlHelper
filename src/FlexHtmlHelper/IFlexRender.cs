using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if FLEXHTMLHELPER_MVC
using System.Web.Mvc;
#else
using System.Web.WebPages;
#endif

namespace FlexHtmlHelper
{
    public enum GridStyle{
        ExtraSmall,
        Small,
        Medium,
        Large,
        Print
    }


    public enum FormLayoutStyle{
        Default,
        Inline,
        Horizontal
    }


    public enum ValidationState
    {
        Warning,
        Error,
        Succuss
    }

    public enum InputHeightStyle
    {
        Small,
        Normal,
        Large
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
        FlexTagBuilder SelectHelper(FlexTagBuilder tagBuilder, string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple, IDictionary<string, object> htmlAttributes);
        FlexTagBuilder TextAreaHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> rowsAndColumns, IDictionary<string, object> htmlAttributes, string innerHtmlPrefix = null);
        FlexTagBuilder StaticHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> htmlAttributes);

        #endregion

        #region Grid System

        FlexTagBuilder GridRow(FlexTagBuilder tagBuilder);
        FlexTagBuilder GridColumns(FlexTagBuilder tagBuilder, GridStyle style, int columns);
        FlexTagBuilder GridColumnOffset(FlexTagBuilder tagBuilder, GridStyle style, int columns);
        FlexTagBuilder GridColumnPush(FlexTagBuilder tagBuilder, GridStyle style, int columns);
        FlexTagBuilder GridColumnPull(FlexTagBuilder tagBuilder, GridStyle style, int columns);
        FlexTagBuilder GridColumnVisible(FlexTagBuilder tagBuilder, GridStyle style, bool visible);

        #endregion

        #region Form

        FlexTagBuilder FormLayout(FlexTagBuilder tagBuilder, FormLayoutStyle layout);        
        FlexTagBuilder FormGroupHelpText(FlexTagBuilder tagBuilder, string text);
        FlexTagBuilder FormGroupLabelText(FlexTagBuilder tagBuilder, string text);
        FlexTagBuilder FormGroupValidationState(FlexTagBuilder tagBuilder, ValidationState state);
        FlexTagBuilder FormGroupAddInput(FlexFormContext formContext, FlexTagBuilder formGroupTag, FlexTagBuilder labelTag, FlexTagBuilder inputTag);
        FlexTagBuilder FormGroupInputGridColumns(FlexFormContext formContext, FlexTagBuilder formGroupTag, GridStyle style, int columns);

        #endregion

        #region Input

        FlexTagBuilder InputPlaceholder(FlexTagBuilder tagBuilder, string text);        
        FlexTagBuilder InputFocus(FlexTagBuilder tagBuilder);
        FlexTagBuilder InputHeight(FlexTagBuilder tagBuilder, InputHeightStyle size);               
        
        #endregion

        #region Element

        FlexTagBuilder Disabled(FlexTagBuilder tagBuilder);

        #endregion

    }
}
