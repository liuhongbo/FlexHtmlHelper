using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexFormGroup: FlexElement
    {
        public FlexFormGroup(FlexFormContext formContext, FHtmlHelper flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {
            FormContext = formContext;
        }

        public FlexFormGroup(FlexFormContext formContext, FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {
            FormContext = formContext;
        }

        public FlexFormGroup()
        {

        }

        public FlexFormContext FormContext { get; set; }

        public FlexTagBuilder InputTag
        {
            get
            {
                var tag = TagBuilder.Tag("input");
                if (tag == null)
                {
                    tag = TagBuilder.Tag("select");
                    if (tag == null)
                    {
                        tag = TagBuilder.Tag("textarea");                        
                    }
                }
                
                return tag;
            }
        }        

        public static FlexFormGroup Empty = new FlexFormGroup();

    }

    public static class FlexFormGroupExtensions
    {

        public static T label_text<T>(this T formGroup, string text) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupLabelText(formGroup.TagBuilder, text);
            return formGroup;
        }

        public static T help_text<T>(this T formGroup, string text) where T: FlexFormGroup
        {
            formGroup.Render.FormGroupHelpText(formGroup.TagBuilder, text);
            return formGroup;
        }        

        public static T input_placeholder<T>(this T formGroup, string text) where T: FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.InputPlaceholder(input, text);
            }
            return formGroup;
        }

        public static T input_disabled<T>(this T formGroup) where T : FlexFormGroup
        {
             var input = formGroup.InputTag;
             if (input != null)
             {
                 formGroup.Render.Disabled(input);
             }
            return formGroup;
        }

        public static T input_focus<T>(this T formGroup) where T : FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.InputFocus(input);
            }
            return formGroup;
        }

        public static T has_warning<T>(this T formGroup) where T: FlexFormGroup
        {
            formGroup.Render.FormGroupValidationState(formGroup.TagBuilder,ValidationState.Warning);
            return formGroup;
        }

        public static T has_error<T>(this T formGroup) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupValidationState(formGroup.TagBuilder, ValidationState.Error);
            return formGroup;
        }

        public static T has_success<T>(this T formGroup) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupValidationState(formGroup.TagBuilder, ValidationState.Succuss);
            return formGroup;
        }

        public static T input_lg<T>(this T formGroup) where T : FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.InputHeight(input, InputHeightStyle.Large);
            }
            return formGroup;
        }

        public static T input_sm<T>(this T formGroup) where T : FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.InputHeight(input, InputHeightStyle.Small);
            }
            return formGroup;
        }

        public static T input_col_xs<T>(this T formGroup, int columns) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupInputGridColumns(formGroup.TagBuilder, formGroup.FormContext, GridStyle.ExtraSmall, columns);
            return formGroup;
        }

        public static T input_col_sm<T>(this T formGroup, int columns) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupInputGridColumns(formGroup.TagBuilder, formGroup.FormContext, GridStyle.Small, columns);
            return formGroup;
        }

        public static T input_col_md<T>(this T formGroup, int columns) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupInputGridColumns(formGroup.TagBuilder, formGroup.FormContext, GridStyle.Medium, columns);
            return formGroup;
        }

        public static T input_col_lg<T>(this T formGroup, int columns) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupInputGridColumns(formGroup.TagBuilder, formGroup.FormContext, GridStyle.Large, columns);
            return formGroup;
        }


        #region Inline Checkbox
        // CheckBox

        public static FlexFormGroup CheckBox(this FlexFormGroup formGroup, string name)
        {
            return CheckBox(formGroup, name, htmlAttributes: (object)null);
        }

        public static FlexFormGroup CheckBox(this FlexFormGroup formGroup, string name, bool isChecked)
        {
            return CheckBox(formGroup, name, isChecked, htmlAttributes: (object)null);
        }

        public static FlexFormGroup CheckBox(this FlexFormGroup formGroup, string name, bool isChecked, object htmlAttributes)
        {
            return CheckBox(formGroup, name, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup CheckBox(this FlexFormGroup formGroup, string name, object htmlAttributes)
        {
            return CheckBox(formGroup, name, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup CheckBox(this FlexFormGroup formGroup, string name, IDictionary<string, object> htmlAttributes)
        {
            return CheckBoxHelper(formGroup, metadata: null, name: name, isChecked: null, htmlAttributes: htmlAttributes);
        }

        public static FlexFormGroup CheckBox(this FlexFormGroup formGroup, string name, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            return CheckBoxHelper(formGroup, metadata: null, name: name, isChecked: isChecked, htmlAttributes: htmlAttributes);
        }


        private static FlexFormGroup CheckBoxHelper(FlexFormGroup formGroup, ModelMetadata metadata, string name, bool? isChecked, IDictionary<string, object> htmlAttributes)
        {
            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);

            bool explicitValue = isChecked.HasValue;
            if (explicitValue)
            {
                attributes.Remove("checked"); // Explicit value must override dictionary
            }


            return InputHelper((FlexFormGroup)formGroup,
                               InputType.CheckBox,
                               metadata,
                               name,
                               value: "true",
                               useViewData: !explicitValue,
                               isChecked: isChecked ?? false,
                               setId: true,
                               isExplicitValue: false,
                               format: null,
                               htmlAttributes: attributes);            
        }       

        #endregion

        #region Inline Radio

        public static FlexFormGroup RadioButton(this FlexFormGroup formGroup, string name, object value)
        {
            return RadioButton(formGroup, name, value, htmlAttributes: (object)null);
        }

        public static FlexFormGroup RadioButton(this FlexFormGroup formGroup, string name, object value, object htmlAttributes)
        {
            return RadioButton(formGroup, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup RadioButton(this FlexFormGroup formGroup, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            // Determine whether or not to render the checked attribute based on the contents of ViewData.
            string valueString = Convert.ToString(value, CultureInfo.CurrentCulture);
            bool isChecked = (!String.IsNullOrEmpty(name)) && (String.Equals(formGroup.FHtmlHelper.EvalString(name), valueString, StringComparison.OrdinalIgnoreCase));
            // checked attributes is implicit, so we need to ensure that the dictionary takes precedence.
            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);


            if (attributes.ContainsKey("checked"))
            {
                return (FlexFormGroup)InputHelper(formGroup,
                                   InputType.Radio,
                                   metadata: null,
                                   name: name,
                                   value: value,
                                   useViewData: false,
                                   isChecked: false,
                                   setId: true,
                                   isExplicitValue: true,
                                   format: null,
                                   htmlAttributes: attributes);
            }

            return RadioButton(formGroup, name, value, isChecked, htmlAttributes);
        }

        public static FlexFormGroup RadioButton(this FlexFormGroup formGroup, string name, object value, bool isChecked)
        {
            return RadioButton(formGroup, name, value, isChecked, htmlAttributes: (object)null);
        }

        public static FlexFormGroup RadioButton(this FlexFormGroup formGroup, string name, object value, bool isChecked, object htmlAttributes)
        {
            return RadioButton(formGroup, name, value, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup RadioButton(this FlexFormGroup formGroup, string name, object value, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            // checked attribute is an explicit parameter so it takes precedence.
            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            attributes.Remove("checked");

            return (FlexFormGroup)InputHelper(formGroup,
                               InputType.Radio,
                               metadata: null,
                               name: name,
                               value: value,
                               useViewData: false,
                               isChecked: isChecked,
                               setId: true,
                               isExplicitValue: true,
                               format: null,
                               htmlAttributes: attributes);
            
        }

        #endregion

        #region Helper

        private static FlexFormGroup InputHelper(FlexFormGroup formGroup, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder formGroupTag = InputTagBuilderHelper(formGroup, inputType, metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);

            return formGroup;
        }

        internal static FlexTagBuilder InputTagBuilderHelper(FlexFormGroup formGroup, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FHtmlHelper htmlHelper = formGroup.FHtmlHelper;

            string fullName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);


            FlexLabel label = (metadata != null) ? htmlHelper.LabelHelper(metadata, name) : htmlHelper.Label(name);
            FlexInput input = htmlHelper.InputHelper<FlexInput>(inputType, metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);
            //FlexValidationMessage validateMessage = htmlHelper.ValidationMessageHelper(metadata, name, null, null);

            if (inputType == InputType.Radio)
            {
                var originalId = input.TagBuilder.TagAttributes["id"];
                if ((originalId != null) && (value != null))
                {
                    var newId = originalId + "_" + value.ToString();
                    input.TagBuilder.TagAttributes["id"] = newId;
                    label.TagBuilder.TagAttributes["for"] = newId;
                }
            }

            formGroup.Render.FormGroupAddInput(formGroup.TagBuilder, formGroup.FormContext, label.TagBuilder, input.TagBuilder);

            return formGroup.TagBuilder;
        }

        #endregion

        #region Button

        public static FlexFormGroup Button(this FlexFormGroup formGroup, FlexButton button)
        {
            formGroup.Render.FormGroupAddButton(formGroup.TagBuilder, formGroup.FormContext, button.TagBuilder);

            return formGroup;

        }

        public static FlexFormGroup Button(this FlexFormGroup formGroup, FlexTagBuilder buttonTag)
        {
            formGroup.Render.FormGroupAddButton(formGroup.TagBuilder, formGroup.FormContext, buttonTag);

            return formGroup;

        }

        #endregion   
    }

    public class FlexFormGroup<TModel> : FlexFormGroup
    {

        private FHtmlHelper<TModel> _flexHtmlHelper;

        public FlexFormGroup(FlexFormContext formContext, FHtmlHelper<TModel> flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(formContext,flexHtmlHelper, tagBuilder)
        {
            _flexHtmlHelper = flexHtmlHelper;
        }

        public FlexFormGroup(FlexFormContext formContext, FHtmlHelper<TModel> flexHtmlHelper, string tagName)
            : base(formContext,flexHtmlHelper, tagName)
        {
            _flexHtmlHelper = flexHtmlHelper;
        }

        public FlexFormGroup()
        {

        }

        internal new FHtmlHelper<TModel> FHtmlHelper 
        {
            get { return _flexHtmlHelper; }
        } 

        public static new FlexFormGroup<TModel> Empty = new FlexFormGroup<TModel>();
    }

    public static class FlexFormGroupForModelExtensions
    {

        #region Inline CheckBox

        public static FlexFormGroup<TModel> CheckBox<TModel>(this FlexFormGroup<TModel> formGroup, string name)
        {
            return CheckBox(formGroup, name, htmlAttributes: (object)null);
        }

        public static FlexFormGroup<TModel> CheckBox<TModel>(this FlexFormGroup<TModel> formGroup, string name, bool isChecked)
        {
            return CheckBox(formGroup, name, isChecked, htmlAttributes: (object)null);
        }

        public static FlexFormGroup<TModel> CheckBox<TModel>(this FlexFormGroup<TModel> formGroup, string name, bool isChecked, object htmlAttributes)
        {
            return CheckBox(formGroup, name, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup<TModel> CheckBox<TModel>(this FlexFormGroup<TModel> formGroup, string name, object htmlAttributes)
        {
            return CheckBox(formGroup, name, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup<TModel> CheckBox<TModel>(this FlexFormGroup<TModel> formGroup, string name, IDictionary<string, object> htmlAttributes)
        {
            return CheckBoxHelper(formGroup, metadata: null, name: name, isChecked: null, htmlAttributes: htmlAttributes);
        }

        public static FlexFormGroup<TModel> CheckBox<TModel>(this FlexFormGroup<TModel> formGroup, string name, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            return CheckBoxHelper(formGroup, metadata: null, name: name, isChecked: isChecked, htmlAttributes: htmlAttributes);
        }        

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> CheckBoxFor<TModel>(this FlexFormGroup<TModel> formGroup, Expression<Func<TModel, bool>> expression)
        {
            return CheckBoxFor(formGroup, expression, htmlAttributes: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> CheckBoxFor<TModel>(this FlexFormGroup<TModel> formGroup, Expression<Func<TModel, bool>> expression, object htmlAttributes)
        {
            return CheckBoxFor(formGroup, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> CheckBoxFor<TModel>(this FlexFormGroup<TModel> formGroup, Expression<Func<TModel, bool>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, formGroup.FHtmlHelper.HtmlHelper.ViewData);
            bool? isChecked = null;
            if (metadata.Model != null)
            {
                bool modelChecked;
                if (Boolean.TryParse(metadata.Model.ToString(), out modelChecked))
                {
                    isChecked = modelChecked;
                }
            }

            return CheckBoxHelper(formGroup, metadata, ExpressionHelper.GetExpressionText(expression), isChecked, htmlAttributes);
        }

        private static FlexFormGroup<TModel> CheckBoxHelper<TModel>(this FlexFormGroup<TModel> formGroup, ModelMetadata metadata, string name, bool? isChecked, IDictionary<string, object> htmlAttributes)
        {
            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);

            bool explicitValue = isChecked.HasValue;
            if (explicitValue)
            {
                attributes.Remove("checked"); // Explicit value must override dictionary
            }


            return InputHelper(formGroup,
                               InputType.CheckBox,
                               metadata,
                               name,
                               value: "true",
                               useViewData: !explicitValue,
                               isChecked: isChecked ?? false,
                               setId: true,
                               isExplicitValue: false,
                               format: null,
                               htmlAttributes: attributes);
        }

        #endregion

        #region Inline Radio

        public static FlexFormGroup<TModel> RadioButton<TModel>(this FlexFormGroup<TModel> formGroup, string name, object value)
        {
            return RadioButton(formGroup, name, value, htmlAttributes: (object)null);
        }

        public static FlexFormGroup<TModel> RadioButton<TModel>(this FlexFormGroup<TModel> formGroup, string name, object value, object htmlAttributes)
        {
            return RadioButton(formGroup, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup<TModel> RadioButton<TModel>(this FlexFormGroup<TModel> formGroup, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            // Determine whether or not to render the checked attribute based on the contents of ViewData.
            string valueString = Convert.ToString(value, CultureInfo.CurrentCulture);
            bool isChecked = (!String.IsNullOrEmpty(name)) && (String.Equals(formGroup.FHtmlHelper.EvalString(name), valueString, StringComparison.OrdinalIgnoreCase));
            // checked attributes is implicit, so we need to ensure that the dictionary takes precedence.
            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);


            if (attributes.ContainsKey("checked"))
            {
                return InputHelper(formGroup,
                                   InputType.Radio,
                                   metadata: null,
                                   name: name,
                                   value: value,
                                   useViewData: false,
                                   isChecked: false,
                                   setId: true,
                                   isExplicitValue: true,
                                   format: null,
                                   htmlAttributes: attributes);
            }

            return RadioButton(formGroup, name, value, isChecked, htmlAttributes);
        }

        public static FlexFormGroup<TModel> RadioButton<TModel>(this FlexFormGroup<TModel> formGroup, string name, object value, bool isChecked)
        {
            return RadioButton(formGroup, name, value, isChecked, htmlAttributes: (object)null);
        }

        public static FlexFormGroup<TModel> RadioButton<TModel>(this FlexFormGroup<TModel> formGroup, string name, object value, bool isChecked, object htmlAttributes)
        {
            return RadioButton(formGroup, name, value, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup<TModel> RadioButton<TModel>(this FlexFormGroup<TModel> formGroup, string name, object value, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            // checked attribute is an explicit parameter so it takes precedence.
            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            attributes.Remove("checked");

            return InputHelper(formGroup,
                               InputType.Radio,
                               metadata: null,
                               name: name,
                               value: value,
                               useViewData: false,
                               isChecked: isChecked,
                               setId: true,
                               isExplicitValue: true,
                               format: null,
                               htmlAttributes: attributes);

        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> RadioButtonFor<TModel, TProperty>(this FlexFormGroup<TModel> formGroup, Expression<Func<TModel, TProperty>> expression, object value)
        {
            return RadioButtonFor(formGroup, expression, value, htmlAttributes: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> RadioButtonFor<TModel, TProperty>(this FlexFormGroup<TModel> formGroup, Expression<Func<TModel, TProperty>> expression, object value, object htmlAttributes)
        {
            return RadioButtonFor(formGroup, expression, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> RadioButtonFor<TModel, TProperty>(this FlexFormGroup<TModel> formGroup, Expression<Func<TModel, TProperty>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, formGroup.FHtmlHelper.HtmlHelper.ViewData);
            return RadioButtonHelper(formGroup,
                                     metadata,
                                     metadata.Model,
                                     ExpressionHelper.GetExpressionText(expression),
                                     value,
                                     null /* isChecked */,
                                     htmlAttributes);
        }       

        private static FlexFormGroup<TModel> RadioButtonHelper<TModel>(this FlexFormGroup<TModel> formGroup, ModelMetadata metadata, object model, string name, object value, bool? isChecked, IDictionary<string, object> htmlAttributes)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);

            bool explicitValue = isChecked.HasValue;
            if (explicitValue)
            {
                attributes.Remove("checked"); // Explicit value must override dictionary
            }
            else
            {
                string valueString = Convert.ToString(value, CultureInfo.CurrentCulture);
                isChecked = model != null &&
                            !String.IsNullOrEmpty(name) &&
                            String.Equals(model.ToString(), valueString, StringComparison.OrdinalIgnoreCase);
            }

            return InputHelper(formGroup,
                               InputType.Radio,
                               metadata,
                               name,
                               value,
                               useViewData: false,
                               isChecked: isChecked ?? false,
                               setId: true,
                               isExplicitValue: true,
                               format: null,
                               htmlAttributes: attributes);
        }


        #endregion

        #region Helper        

        private static FlexFormGroup<TModel> InputHelper<TModel>(FlexFormGroup<TModel> formGroup, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder formGroupTag = FlexFormGroupExtensions.InputTagBuilderHelper(formGroup, inputType, metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);

            return formGroup;
        }       

        #endregion

        #region Button

        public static FlexFormGroup<TModel> Button<TModel>(this FlexFormGroup<TModel> formGroup, FlexButton button)
        {
            formGroup.Render.FormGroupAddButton(formGroup.TagBuilder, formGroup.FormContext, button.TagBuilder);

            return formGroup;

        }

        public static FlexFormGroup<TModel> Button<TModel>(this FlexFormGroup<TModel> formGroup, FlexTagBuilder buttonTag)
        {
            formGroup.Render.FormGroupAddButton(formGroup.TagBuilder, formGroup.FormContext, buttonTag);

            return formGroup;

        }

        #endregion   
    }
}
