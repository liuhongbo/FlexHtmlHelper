using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexMvcForm : MvcForm
    {
        public FlexMvcForm(FHtmlHelper htmlHelper, FlexFormContext formContext)
            : base(htmlHelper.HtmlHelper.ViewContext)
        {
            FHtmlHelper = htmlHelper;
            FormContext = formContext;
        }

        public FHtmlHelper FHtmlHelper { get; set; }

        public FlexFormContext FormContext { get; set; }

    }

    public class FlexMvcForm<TModel> : FlexMvcForm
    {

        public FlexMvcForm(FHtmlHelper<TModel> htmlHelper, FlexFormContext formContext)
            : base(htmlHelper, formContext)
        {
            FHtmlHelper = htmlHelper;
        }

        public new FHtmlHelper<TModel> FHtmlHelper { get; set; }
    }

    public static class FlexMvcFormExtension
    {
        // CheckBox

        public static FlexFormGroup CheckBox(this FlexMvcForm form, string name)
        {
            return CheckBox(form,name, htmlAttributes: (object)null);
        }

        public static FlexFormGroup CheckBox(this FlexMvcForm form, string name, bool isChecked)
        {
            return CheckBox(form,name, isChecked, htmlAttributes: (object)null);
        }

        public static FlexFormGroup CheckBox(this FlexMvcForm form, string name, bool isChecked, object htmlAttributes)
        {
            return CheckBox(form,name, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup CheckBox(this FlexMvcForm form, string name, object htmlAttributes)
        {
            return CheckBox(form, name, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup CheckBox(this FlexMvcForm form, string name, IDictionary<string, object> htmlAttributes)
        {
            return CheckBoxHelper(form, metadata: null, name: name, isChecked: null, htmlAttributes: htmlAttributes);
        }

        public static FlexFormGroup CheckBox(this FlexMvcForm form, string name, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            return CheckBoxHelper(form, metadata: null, name: name, isChecked: isChecked, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> CheckBoxFor<TModel>(this FlexMvcForm<TModel> form, Expression<Func<TModel, bool>> expression)
        {
            return CheckBoxFor(form, expression, htmlAttributes: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> CheckBoxFor<TModel>(this FlexMvcForm<TModel> form, Expression<Func<TModel, bool>> expression, object htmlAttributes)
        {
            return CheckBoxFor(form, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> CheckBoxFor<TModel>(this FlexMvcForm<TModel> form, Expression<Func<TModel, bool>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData);
            bool? isChecked = null;
            if (metadata.Model != null)
            {
                bool modelChecked;
                if (Boolean.TryParse(metadata.Model.ToString(), out modelChecked))
                {
                    isChecked = modelChecked;
                }
            }

            return CheckBoxHelper(form, metadata, ExpressionHelper.GetExpressionText(expression), isChecked, htmlAttributes);
        }

        private static FlexFormGroup CheckBoxHelper(this FlexMvcForm form, ModelMetadata metadata, string name, bool? isChecked, IDictionary<string, object> htmlAttributes)
        {
            RouteValueDictionary attributes = ToRouteValueDictionary(htmlAttributes);

            bool explicitValue = isChecked.HasValue;
            if (explicitValue)
            {
                attributes.Remove("checked"); // Explicit value must override dictionary
            }

            return InputHelper(form,
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

        private static FlexFormGroup<TModel> CheckBoxHelper<TModel>(this FlexMvcForm<TModel> form, ModelMetadata metadata, string name, bool? isChecked, IDictionary<string, object> htmlAttributes)
        {
            RouteValueDictionary attributes = ToRouteValueDictionary(htmlAttributes);

            bool explicitValue = isChecked.HasValue;
            if (explicitValue)
            {
                attributes.Remove("checked"); // Explicit value must override dictionary
            }

            return InputHelper(form,
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

        // Hidden

        public static FlexFormGroup Hidden(this FlexMvcForm form, string name)
        {
            return Hidden(form, name, value: null, htmlAttributes: null);
        }

        public static FlexFormGroup Hidden(this FlexMvcForm form, string name, object value)
        {
            return Hidden(form, name, value, htmlAttributes: null);
        }

        public static FlexFormGroup Hidden(this FlexMvcForm form, string name, object value, object htmlAttributes)
        {
            return Hidden(form, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup Hidden(this FlexMvcForm form, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return HiddenHelper(form,
                                metadata: null,
                                value: value,
                                useViewData: value == null,
                                expression: name,
                                htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup HiddenFor<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return HiddenFor(expression, (IDictionary<string, object>)null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup HiddenFor<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return HiddenFor(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> HiddenFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData);
            return HiddenHelper(form,
                                metadata,
                                metadata.Model,
                                false,
                                ExpressionHelper.GetExpressionText(expression),
                                htmlAttributes);
        }

        private static FlexFormGroup HiddenHelper(this FlexMvcForm form, ModelMetadata metadata, object value, bool useViewData, string expression, IDictionary<string, object> htmlAttributes)
        {
            Binary binaryValue = value as Binary;
            if (binaryValue != null)
            {
                value = binaryValue.ToArray();
            }

            byte[] byteArrayValue = value as byte[];
            if (byteArrayValue != null)
            {
                value = Convert.ToBase64String(byteArrayValue);
            }

            return InputHelper(form,
                               InputType.Hidden,
                               metadata,
                               expression,
                               value,
                               useViewData,
                               isChecked: false,
                               setId: true,
                               isExplicitValue: true,
                               format: null,
                               htmlAttributes: htmlAttributes) as FlexFormGroup;
        }

        private static FlexFormGroup<TModel> HiddenHelper<TModel>(this FlexMvcForm<TModel> form, ModelMetadata metadata, object value, bool useViewData, string expression, IDictionary<string, object> htmlAttributes)
        {
            Binary binaryValue = value as Binary;
            if (binaryValue != null)
            {
                value = binaryValue.ToArray();
            }

            byte[] byteArrayValue = value as byte[];
            if (byteArrayValue != null)
            {
                value = Convert.ToBase64String(byteArrayValue);
            }

            return InputHelper(form,
                               InputType.Hidden,
                               metadata,
                               expression,
                               value,
                               useViewData,
                               isChecked: false,
                               setId: true,
                               isExplicitValue: true,
                               format: null,
                               htmlAttributes: htmlAttributes);
        }

        // Password

        public static FlexFormGroup Password(this FlexMvcForm form, string name)
        {
            return Password(form, name, value: null);
        }

        public static FlexFormGroup Password(this FlexMvcForm form, string name, object value)
        {
            return Password(form, name, value, htmlAttributes: null);
        }

        public static FlexFormGroup Password(this FlexMvcForm form, string name, object value, object htmlAttributes)
        {
            return Password(form, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup Password(this FlexMvcForm form, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return PasswordHelper(form, metadata: null, name: name, value: value, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> PasswordFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression)
        {
            return PasswordFor(form, expression, htmlAttributes: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> PasswordFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return PasswordFor(form, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> PasswordFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return PasswordHelper(form,
                                  ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData),
                                  ExpressionHelper.GetExpressionText(expression),
                                  value: null,
                                  htmlAttributes: htmlAttributes);
        }

        private static FlexFormGroup PasswordHelper(this FlexMvcForm form, ModelMetadata metadata, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper(form,
                               InputType.Password,
                               metadata,
                               name,
                               value,
                               useViewData: false,
                               isChecked: false,
                               setId: true,
                               isExplicitValue: true,
                               format: null,
                               htmlAttributes: htmlAttributes);
        }

        private static FlexFormGroup<TModel> PasswordHelper<TModel>(this FlexMvcForm<TModel> form, ModelMetadata metadata, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper(form,
                               InputType.Password,
                               metadata,
                               name,
                               value,
                               useViewData: false,
                               isChecked: false,
                               setId: true,
                               isExplicitValue: true,
                               format: null,
                               htmlAttributes: htmlAttributes);
        }

        // RadioButton

        public static FlexFormGroup RadioButton(this FlexMvcForm form, string name, object value)
        {
            return RadioButton(form, name, value, htmlAttributes: (object)null);
        }

        public static FlexFormGroup RadioButton(this FlexMvcForm form, string name, object value, object htmlAttributes)
        {
            return RadioButton(form, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup RadioButton(this FlexMvcForm form, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            // Determine whether or not to render the checked attribute based on the contents of ViewData.
            string valueString = Convert.ToString(value, CultureInfo.CurrentCulture);
            bool isChecked = (!String.IsNullOrEmpty(name)) && (String.Equals(form.FHtmlHelper.EvalString(name), valueString, StringComparison.OrdinalIgnoreCase));
            // checked attributes is implicit, so we need to ensure that the dictionary takes precedence.
            RouteValueDictionary attributes = ToRouteValueDictionary(htmlAttributes);
            if (attributes.ContainsKey("checked"))
            {
                return InputHelper(form,
                                   InputType.Radio,
                                   metadata: null,
                                   name: name,
                                   value: value,
                                   useViewData: false,
                                   isChecked: false,
                                   setId: true,
                                   isExplicitValue: true,
                                   format: null,
                                   htmlAttributes: attributes) as FlexFormGroup;
            }

            return RadioButton(form, name, value, isChecked, htmlAttributes);
        }

        public static FlexFormGroup RadioButton(this FlexMvcForm form, string name, object value, bool isChecked)
        {
            return RadioButton(form, name, value, isChecked, htmlAttributes: (object)null);
        }

        public static FlexFormGroup RadioButton(this FlexMvcForm form, string name, object value, bool isChecked, object htmlAttributes)
        {
            return RadioButton(form, name, value, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup RadioButton(this FlexMvcForm form, string name, object value, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            // checked attribute is an explicit parameter so it takes precedence.
            RouteValueDictionary attributes = ToRouteValueDictionary(htmlAttributes);
            attributes.Remove("checked");
            return InputHelper(form,
                               InputType.Radio,
                               metadata: null,
                               name: name,
                               value: value,
                               useViewData: false,
                               isChecked: isChecked,
                               setId: true,
                               isExplicitValue: true,
                               format: null,
                               htmlAttributes: attributes) as FlexFormGroup;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> RadioButtonFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, object value)
        {
            return RadioButtonFor(form, expression, value, htmlAttributes: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> RadioButtonFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, object value, object htmlAttributes)
        {
            return RadioButtonFor(form, expression, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> RadioButtonFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData);
            return RadioButtonHelper(form,
                                     metadata,
                                     metadata.Model,
                                     ExpressionHelper.GetExpressionText(expression),
                                     value,
                                     null /* isChecked */,
                                     htmlAttributes);
        }

        private static FlexFormGroup RadioButtonHelper(this FlexMvcForm form, ModelMetadata metadata, object model, string name, object value, bool? isChecked, IDictionary<string, object> htmlAttributes)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            RouteValueDictionary attributes = ToRouteValueDictionary(htmlAttributes);

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

            return InputHelper(form,
                               InputType.Radio,
                               metadata,
                               name,
                               value,
                               useViewData: false,
                               isChecked: isChecked ?? false,
                               setId: true,
                               isExplicitValue: true,
                               format: null,
                               htmlAttributes: attributes) as FlexFormGroup;
        }

        private static FlexFormGroup<TModel> RadioButtonHelper<TModel>(this FlexMvcForm<TModel> form, ModelMetadata metadata, object model, string name, object value, bool? isChecked, IDictionary<string, object> htmlAttributes)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            RouteValueDictionary attributes = ToRouteValueDictionary(htmlAttributes);

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

            return InputHelper(form,
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

        // TextBox

        public static FlexFormGroup TextBox(this FlexMvcForm form, string name)
        {
            return TextBox(form, name, value: null);
        }

        public static FlexFormGroup TextBox(this FlexMvcForm form, string name, object value)
        {
            return TextBox(form, name, value, format: null);
        }

        public static FlexFormGroup TextBox(this FlexMvcForm form, string name, object value, string format)
        {
            return TextBox(form, name, value, format, htmlAttributes: (object)null);
        }

        public static FlexFormGroup TextBox(this FlexMvcForm form, string name, object value, object htmlAttributes)
        {
            return TextBox(form, name, value, format: null, htmlAttributes: htmlAttributes);
        }

        public static FlexFormGroup TextBox(this FlexMvcForm form, string name, object value, string format, object htmlAttributes)
        {
            return TextBox(form, name, value, format, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup TextBox(this FlexMvcForm form, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return TextBox(form, name, value, format: null, htmlAttributes: htmlAttributes);
        }

        public static FlexFormGroup TextBox(this FlexMvcForm form, string name, object value, string format, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper(form,
                               InputType.Text,
                               metadata: null,
                               name: name,
                               value: value,
                               useViewData: (value == null),
                               isChecked: false,
                               setId: true,
                               isExplicitValue: true,
                               format: format,
                               htmlAttributes: htmlAttributes) as FlexFormGroup;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextBoxFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression)
        {
            return TextBoxFor(form, expression, format: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextBoxFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, string format)
        {
            return TextBoxFor(form, expression, format, (IDictionary<string, object>)null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextBoxFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return TextBoxFor(form, expression, format: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextBoxFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, string format, object htmlAttributes)
        {
            return TextBoxFor(form, expression, format: format, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextBoxFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            return TextBoxFor(form, expression, format: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextBoxFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData);
            return TextBoxHelper(form,
                                 metadata,
                                 metadata.Model,
                                 ExpressionHelper.GetExpressionText(expression),
                                 format,
                                 htmlAttributes);
        }

        private static FlexFormGroup TextBoxHelper(FlexMvcForm form, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper(form,
                               InputType.Text,
                               metadata,
                               expression,
                               model,
                               useViewData: false,
                               isChecked: false,
                               setId: true,
                               isExplicitValue: true,
                               format: format,
                               htmlAttributes: htmlAttributes) as FlexFormGroup;
        }

        private static FlexFormGroup<TModel> TextBoxHelper<TModel>(FlexMvcForm<TModel> form, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper(form,
                               InputType.Text,
                               metadata,
                               expression,
                               model,
                               useViewData: false,
                               isChecked: false,
                               setId: true,
                               isExplicitValue: true,
                               format: format,
                               htmlAttributes: htmlAttributes);
        }

        // Helper methods

        private static FlexFormGroup InputHelper(FlexMvcForm form, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder formGroup = InputTagBuilderHelper(form, inputType, metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);

            return new FlexFormGroup(form.FHtmlHelper, formGroup);
        }

        private static FlexFormGroup<TModel> InputHelper<TModel>(FlexMvcForm<TModel> form, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder formGroup = InputTagBuilderHelper(form, inputType, metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);

            return new FlexFormGroup<TModel>(form.FHtmlHelper, formGroup);
        }

        private static FlexTagBuilder InputTagBuilderHelper(FlexMvcForm form, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FHtmlHelper htmlHelper = form.FHtmlHelper;

            string fullName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            
            FlexLabel label = htmlHelper.LabelHelper(metadata, name);
            FlexInput input = htmlHelper.InputHelper<FlexInput>(inputType, metadata, name, value,useViewData, isChecked, setId, isExplicitValue, format,htmlAttributes);
            FlexValidationMessage validateMessage = htmlHelper.ValidationMessageHelper(metadata, name, null, null);

            if (inputType == InputType.Radio)
            {
                var originalId = input.TagBuilder.Attributes["id"];
                if ((originalId != null) && (value!= null))
                {
                    var newId = originalId + "_" + value.ToString();
                    input.TagBuilder.Attributes["id"] = newId;
                    label.TagBuilder.Attributes["for"] = newId;
                }
            }

            FlexTagBuilder formGroup = htmlHelper.Render.FormGroupHelper(new FlexTagBuilder(), form.FormContext, label.TagBuilder,input.TagBuilder,validateMessage.TagBuilder);

            return formGroup;
        }

        private static RouteValueDictionary ToRouteValueDictionary(IDictionary<string, object> dictionary)
        {
            return dictionary == null ? new RouteValueDictionary() : new RouteValueDictionary(dictionary);
        }

    }
}
