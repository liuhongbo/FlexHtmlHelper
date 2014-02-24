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
                }
                return tag;
            }
        }        

        public static FlexFormGroup Empty = new FlexFormGroup();

    }

    public static class FlexFormGroupExtensions
    {

        public static T LabelText<T>(this T formGroup, string text) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupLabelText(formGroup.TagBuilder, text);
            return formGroup;
        }

        public static T HelpText<T>(this T formGroup, string text) where T: FlexFormGroup
        {
            formGroup.Render.FormGroupHelpText(formGroup.TagBuilder, text);
            return formGroup;
        }        

        public static T Placeholder<T>(this T formGroup, string text) where T: FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.Placeholder(input, text);
            }
            return formGroup;
        }

        public static T Disabled<T>(this T formGroup) where T : FlexFormGroup
        {
             var input = formGroup.InputTag;
             if (input != null)
             {
                 formGroup.Render.Disabled(input);
             }
            return formGroup;
        }

        public static T Focus<T>(this T formGroup) where T : FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.Focus(input);
            }
            return formGroup;
        }

        public static T HasWarning<T>(this T formGroup) where T: FlexFormGroup
        {
            formGroup.Render.FormGroupValidationState(formGroup.TagBuilder,ValidationState.Warning);
            return formGroup;
        }

        public static T HasError<T>(this T formGroup) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupValidationState(formGroup.TagBuilder, ValidationState.Error);
            return formGroup;
        }

        public static T HasSuccess<T>(this T formGroup) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupValidationState(formGroup.TagBuilder, ValidationState.Succuss);
            return formGroup;
        }

        public static T Large<T>(this T formGroup) where T : FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.InputHeight(input, InputHeightStyle.Large);
            }
            return formGroup;
        }

        public static T Small<T>(this T formGroup) where T : FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.InputHeight(input, InputHeightStyle.Small);
            }
            return formGroup;
        }


        #region Inline Checkbox
        // CheckBox

        public static T CheckBox<T>(this T formGroup, string name) where T:FlexFormGroup
        {
            return CheckBox(formGroup, name, htmlAttributes: (object)null);
        }

        public static T CheckBox<T>(this T formGroup, string name, bool isChecked) where T : FlexFormGroup
        {
            return CheckBox(formGroup, name, isChecked, htmlAttributes: (object)null);
        }

        public static T CheckBox<T>(this T formGroup, string name, bool isChecked, object htmlAttributes) where T : FlexFormGroup
        {
            return CheckBox(formGroup, name, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static T CheckBox<T>(this T formGroup, string name, object htmlAttributes) where T : FlexFormGroup
        {
            return CheckBox(formGroup, name, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static T CheckBox<T>(this T formGroup, string name, IDictionary<string, object> htmlAttributes) where T : FlexFormGroup
        {
            return CheckBoxHelper(formGroup, metadata: null, name: name, isChecked: null, htmlAttributes: htmlAttributes);
        }

        public static T CheckBox<T>(this T formGroup, string name, bool isChecked, IDictionary<string, object> htmlAttributes) where T : FlexFormGroup
        {
            return CheckBoxHelper(formGroup, metadata: null, name: name, isChecked: isChecked, htmlAttributes: htmlAttributes);
        }


        private static T CheckBoxHelper<T>(this T formGroup, ModelMetadata metadata, string name, bool? isChecked, IDictionary<string, object> htmlAttributes) where T : FlexFormGroup
        {
            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);

            bool explicitValue = isChecked.HasValue;
            if (explicitValue)
            {
                attributes.Remove("checked"); // Explicit value must override dictionary
            }


            return (T)InputHelper((FlexFormGroup)formGroup,
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

        public static T RadioButton<T>(this T formGroup, string name, object value) where T : FlexFormGroup
        {
            return RadioButton(formGroup, name, value, htmlAttributes: (object)null);
        }

        public static T RadioButton<T>(this T formGroup, string name, object value, object htmlAttributes) where T : FlexFormGroup
        {
            return RadioButton(formGroup, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static T RadioButton<T>(this T formGroup, string name, object value, IDictionary<string, object> htmlAttributes) where T : FlexFormGroup
        {
            // Determine whether or not to render the checked attribute based on the contents of ViewData.
            string valueString = Convert.ToString(value, CultureInfo.CurrentCulture);
            bool isChecked = (!String.IsNullOrEmpty(name)) && (String.Equals(formGroup.FHtmlHelper.EvalString(name), valueString, StringComparison.OrdinalIgnoreCase));
            // checked attributes is implicit, so we need to ensure that the dictionary takes precedence.
            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);


            if (attributes.ContainsKey("checked"))
            {
                return (T)InputHelper(formGroup,
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

        public static T RadioButton<T>(this T formGroup, string name, object value, bool isChecked) where T : FlexFormGroup
        {
            return RadioButton(formGroup, name, value, isChecked, htmlAttributes: (object)null);
        }

        public static T RadioButton<T>(this T formGroup, string name, object value, bool isChecked, object htmlAttributes) where T : FlexFormGroup
        {
            return RadioButton(formGroup, name, value, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static T RadioButton<T>(this T formGroup, string name, object value, bool isChecked, IDictionary<string, object> htmlAttributes) where T : FlexFormGroup
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            // checked attribute is an explicit parameter so it takes precedence.
            RouteValueDictionary attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            attributes.Remove("checked");

            return (T)InputHelper(formGroup,
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


            FlexLabel label = htmlHelper.LabelHelper(metadata, name);
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

            formGroup.Render.FormGroupAddInput(formGroup.FormContext, formGroup.TagBuilder, label.TagBuilder, input.TagBuilder);

            return formGroup.TagBuilder;
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

    }
}
