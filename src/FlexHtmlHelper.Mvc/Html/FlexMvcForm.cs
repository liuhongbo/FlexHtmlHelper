using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

        #region CHECKBOX
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

        #endregion


        #region HIDDEN
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
        public static FlexFormGroup HiddenFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression)
        {
            return HiddenFor(form, expression, (IDictionary<string, object>)null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup HiddenFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return HiddenFor(form, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
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

        #endregion


        #region PASSWORD
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

        #endregion


        #region RADIOBUTTON
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

        #endregion


        #region TEXTBOX
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

        #endregion

        // Helper methods

        private static FlexFormGroup InputHelper(FlexMvcForm form, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder formGroup = InputTagBuilderHelper(form, inputType, metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);

            return new FlexFormGroup(form.FormContext, form.FHtmlHelper, formGroup);
        }

        private static FlexFormGroup<TModel> InputHelper<TModel>(FlexMvcForm<TModel> form, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder formGroup = InputTagBuilderHelper(form, inputType, metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);

            return new FlexFormGroup<TModel>(form.FormContext, form.FHtmlHelper, formGroup);
        }

        private static FlexTagBuilder InputTagBuilderHelper(FlexMvcForm form, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FHtmlHelper htmlHelper = form.FHtmlHelper;

            string fullName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);


            FlexLabel label = (metadata != null) ? htmlHelper.LabelHelper(metadata, name) : htmlHelper.Label(name);
            FlexInput input = htmlHelper.InputHelper<FlexInput>(inputType, metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);
            FlexValidationMessage validateMessage = htmlHelper.ValidationMessageHelper(metadata, name, null, null);

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

            FlexTagBuilder formGroup = htmlHelper.Render.FormGroupHelper(new FlexTagBuilder(), form.FormContext, label.TagBuilder, input.TagBuilder, validateMessage.TagBuilder);
            return formGroup;
        }

        private static RouteValueDictionary ToRouteValueDictionary(IDictionary<string, object> dictionary)
        {
            return dictionary == null ? new RouteValueDictionary() : new RouteValueDictionary(dictionary);
        }


        #region SELECT

        public static FlexFormGroup DropDownList(this FlexMvcForm form, string name)
        {
            return DropDownList(form, name, null /* selectList */, null /* optionLabel */, null /* htmlAttributes */);
        }

        public static FlexFormGroup DropDownList(this FlexMvcForm form, string name, string optionLabel)
        {
            return DropDownList(form, name, null /* selectList */, optionLabel, null /* htmlAttributes */);
        }

        public static FlexFormGroup DropDownList(this FlexMvcForm form, string name, IEnumerable<SelectListItem> selectList)
        {
            return DropDownList(form, name, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        public static FlexFormGroup DropDownList(this FlexMvcForm form, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return DropDownList(form, name, selectList, null /* optionLabel */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup DropDownList(this FlexMvcForm form, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return DropDownList(form, name, selectList, null /* optionLabel */, htmlAttributes);
        }

        public static FlexFormGroup DropDownList(this FlexMvcForm form, string name, IEnumerable<SelectListItem> selectList, string optionLabel)
        {
            return DropDownList(form, name, selectList, optionLabel, null /* htmlAttributes */);
        }

        public static FlexFormGroup DropDownList(this FlexMvcForm form, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return DropDownList(form, name, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup DropDownList(this FlexMvcForm form, string name, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return DropDownListHelper(form, metadata: null, expression: name, selectList: selectList, optionLabel: optionLabel, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> DropDownListFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            return DropDownListFor(form, expression, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> DropDownListFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return DropDownListFor(form, expression, selectList, null /* optionLabel */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> DropDownListFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return DropDownListFor(form, expression, selectList, null /* optionLabel */, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> DropDownListFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel)
        {
            return DropDownListFor(form, expression, selectList, optionLabel, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> DropDownListFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return DropDownListFor(form, expression, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> DropDownListFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData);

            return DropDownListHelper(form, metadata, ExpressionHelper.GetExpressionText(expression), selectList, optionLabel, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> EnumDropDownListFor<TModel, TEnum>(this FlexMvcForm<TModel> form,
            Expression<Func<TModel, TEnum>> expression)
        {
            return EnumDropDownListFor(form, expression, optionLabel: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> EnumDropDownListFor<TModel, TEnum>(this FlexMvcForm<TModel> form,
            Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            return EnumDropDownListFor(form, expression, optionLabel: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> EnumDropDownListFor<TModel, TEnum>(this FlexMvcForm<TModel> form,
            Expression<Func<TModel, TEnum>> expression, IDictionary<string, object> htmlAttributes)
        {
            return EnumDropDownListFor(form, expression, optionLabel: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> EnumDropDownListFor<TModel, TEnum>(this FlexMvcForm<TModel> form,
            Expression<Func<TModel, TEnum>> expression, string optionLabel)
        {
            return EnumDropDownListFor(form, expression, optionLabel, (IDictionary<string, object>)null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> EnumDropDownListFor<TModel, TEnum>(this FlexMvcForm<TModel> form,
            Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes)
        {
            return EnumDropDownListFor(form, expression, optionLabel,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        // Unable to constrain TEnum.  Cannot include IComparable, IConvertible, IFormattable because Nullable<T> does
        // not implement those interfaces (and Int32 does).  Enum alone is not compatible with expression restrictions
        // because that requires a cast from all enum types.  And the struct generic constraint disallows passing a
        // Nullable<T> expression.
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> EnumDropDownListFor<TModel, TEnum>(this FlexMvcForm<TModel> form,
            Expression<Func<TModel, TEnum>> expression, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData);
            if (metadata == null)
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_SelectExtensions_InvalidExpressionParameterNoMetadata), expression.ToString()), "expression");

            }

            if (metadata.ModelType == null)
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_SelectExtensions_InvalidExpressionParameterNoModelType), expression.ToString()), "expression");
            }

            if (!EnumHelper.IsValidForEnumHelper(metadata.ModelType))
            {
                string formatString;
                if (EnumHelper.HasFlags(metadata.ModelType))
                {
                    formatString = FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_SelectExtensions_InvalidExpressionParameterTypeHasFlags);
                }
                else
                {
                    formatString = FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_SelectExtensions_InvalidExpressionParameterType);
                }

                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, formatString, expression.ToString()), "expression");
            }

            // Run through same processing as SelectInternal() to determine selected value and ensure it is included
            // in the select list.
            string expressionName = ExpressionHelper.GetExpressionText(expression);
            string expressionFullName =
                form.FHtmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionName);
            Enum currentValue = null;
            if (!String.IsNullOrEmpty(expressionFullName))
            {
                currentValue = form.FHtmlHelper.GetModelStateValue(expressionFullName, metadata.ModelType) as Enum;
            }

            if (currentValue == null && !String.IsNullOrEmpty(expressionName))
            {
                // Ignore any select list (enumerable with this name) in the view data
                currentValue = form.FHtmlHelper.HtmlHelper.ViewData.Eval(expressionName) as Enum;
            }

            if (currentValue == null)
            {
                currentValue = metadata.Model as Enum;
            }

            IList<SelectListItem> selectList = EnumHelper.GetSelectList(metadata.ModelType, currentValue);
            if (!String.IsNullOrEmpty(optionLabel) && selectList.Count != 0 && String.IsNullOrEmpty(selectList[0].Text))
            {
                // Were given an optionLabel and the select list has a blank initial slot.  Combine.
                selectList[0].Text = optionLabel;

                // Use the option label just once; don't pass it down the lower-level helpers.
                optionLabel = null;
            }

            return DropDownListHelper(form, metadata, expressionName, selectList, optionLabel, htmlAttributes);
        }

        private static FlexFormGroup DropDownListHelper(this FlexMvcForm form, ModelMetadata metadata, string expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(form, metadata, optionLabel, expression, selectList, allowMultiple: false, htmlAttributes: htmlAttributes);
        }

        private static FlexFormGroup<TModel> DropDownListHelper<TModel>(this FlexMvcForm<TModel> form, ModelMetadata metadata, string expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(form, metadata, optionLabel, expression, selectList, allowMultiple: false, htmlAttributes: htmlAttributes);
        }

        // ListBox

        public static FlexFormGroup ListBox(this FlexMvcForm form, string name)
        {
            return ListBox(form, name, null /* selectList */, null /* htmlAttributes */);
        }

        public static FlexFormGroup ListBox(this FlexMvcForm form, string name, IEnumerable<SelectListItem> selectList)
        {
            return ListBox(form, name, selectList, (IDictionary<string, object>)null);
        }

        public static FlexFormGroup ListBox(this FlexMvcForm form, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return ListBox(form, name, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup ListBox(this FlexMvcForm form, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return ListBoxHelper(form, metadata: null, name: name, selectList: selectList, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> ListBoxFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            return ListBoxFor(form, expression, selectList, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> ListBoxFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return ListBoxFor(form, expression, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> ListBoxFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData);

            return ListBoxHelper(form,
                                 metadata,
                                 ExpressionHelper.GetExpressionText(expression),
                                 selectList,
                                 htmlAttributes);
        }

        private static FlexFormGroup ListBoxHelper(FlexMvcForm form, ModelMetadata metadata, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(form, metadata, optionLabel: null, name: name, selectList: selectList, allowMultiple: true, htmlAttributes: htmlAttributes);
        }

        private static FlexFormGroup<TModel> ListBoxHelper<TModel>(FlexMvcForm<TModel> form, ModelMetadata metadata, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(form, metadata, optionLabel: null, name: name, selectList: selectList, allowMultiple: true, htmlAttributes: htmlAttributes);
        }

        // Helper methods

        private static IEnumerable<SelectListItem> GetSelectData(this FHtmlHelper htmlHelper, string name)
        {
            object o = null;
            if (htmlHelper.HtmlHelper.ViewData != null)
            {
                o = htmlHelper.HtmlHelper.ViewData.Eval(name);
            }
            if (o == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_HtmlHelper_MissingSelectData),
                        name,
                        "IEnumerable<SelectListItem>"));
            }
            IEnumerable<SelectListItem> selectList = o as IEnumerable<SelectListItem>;
            if (selectList == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_HtmlHelper_WrongSelectDataType),
                        name,
                        o.GetType().FullName,
                        "IEnumerable<SelectListItem>"));
            }
            return selectList;
        }

        internal static string ListItemToOption(SelectListItem item)
        {
            TagBuilder builder = new TagBuilder("option")
            {
                InnerHtml = HttpUtility.HtmlEncode(item.Text)
            };
            if (item.Value != null)
            {
                builder.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                builder.Attributes["selected"] = "selected";
            }
            return builder.ToString(TagRenderMode.Normal);
        }

        private static IEnumerable<SelectListItem> GetSelectListWithDefaultValue(IEnumerable<SelectListItem> selectList, object defaultValue, bool allowMultiple)
        {
            IEnumerable defaultValues;

            if (allowMultiple)
            {
                defaultValues = defaultValue as IEnumerable;
                if (defaultValues == null || defaultValues is string)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            CultureInfo.CurrentCulture,
                            FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_HtmlHelper_SelectExpressionNotEnumerable),
                            "expression"));
                }
            }
            else
            {
                defaultValues = new[] { defaultValue };
            }

            IEnumerable<string> values = from object value in defaultValues
                                         select Convert.ToString(value, CultureInfo.CurrentCulture);

            // ToString() by default returns an enum value's name.  But selectList may use numeric values.
            IEnumerable<string> enumValues = from Enum value in defaultValues.OfType<Enum>()
                                             select value.ToString("d");
            values = values.Concat(enumValues);

            HashSet<string> selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            List<SelectListItem> newSelectList = new List<SelectListItem>();

            foreach (SelectListItem item in selectList)
            {
                item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                newSelectList.Add(item);
            }
            return newSelectList;
        }

        private static FlexFormGroup SelectInternal(FlexMvcForm form, ModelMetadata metadata,
            string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple,
            IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder formGroup = SelectTagBuilderHelper(form, metadata, optionLabel, name,selectList, allowMultiple, htmlAttributes);

            return new FlexFormGroup(form.FormContext, form.FHtmlHelper, formGroup);
        }


        private static FlexFormGroup<TModel> SelectInternal<TModel>(FlexMvcForm<TModel> form, ModelMetadata metadata,
            string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple,
            IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder formGroup = SelectTagBuilderHelper(form, metadata, optionLabel, name,selectList, allowMultiple, htmlAttributes);

            return new FlexFormGroup<TModel>(form.FormContext, form.FHtmlHelper, formGroup);
        }


        private static FlexTagBuilder SelectTagBuilderHelper(FlexMvcForm form,  ModelMetadata metadata,
            string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple,
            IDictionary<string, object> htmlAttributes)
        {
            FHtmlHelper htmlHelper = form.FHtmlHelper;

            string fullName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException(FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_Common_NullOrEmpty), "name");
            }

            FlexLabel label = (metadata != null) ? htmlHelper.LabelHelper(metadata, name) : htmlHelper.Label(name);
            FlexSelect input = htmlHelper.SelectInternal(metadata, optionLabel, name, selectList, allowMultiple, htmlAttributes);
            FlexValidationMessage validateMessage = htmlHelper.ValidationMessageHelper(metadata, name, null, null);

            FlexTagBuilder formGroup = htmlHelper.Render.FormGroupHelper(new FlexTagBuilder(), form.FormContext, label.TagBuilder, input.TagBuilder, validateMessage.TagBuilder);
            return formGroup;
        }

        #endregion


        #region TEXTAREA

        // These values are similar to the defaults used by WebForms
        // when using <asp:TextBox TextMode="MultiLine"> without specifying
        // the Rows and Columns attributes.
        private const int TextAreaRows = 2;
        private const int TextAreaColumns = 20;

        private static Dictionary<string, object> implicitRowsAndColumns = new Dictionary<string, object>
        {
            { "rows", TextAreaRows.ToString(CultureInfo.InvariantCulture) },
            { "cols", TextAreaColumns.ToString(CultureInfo.InvariantCulture) },
        };

        private static Dictionary<string, object> GetRowsAndColumnsDictionary(int rows, int columns)
        {
            if (rows < 0)
            {
                throw new ArgumentOutOfRangeException("rows", FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_HtmlHelper_TextAreaParameterOutOfRange));
            }
            if (columns < 0)
            {
                throw new ArgumentOutOfRangeException("columns", FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_HtmlHelper_TextAreaParameterOutOfRange));
            }

            Dictionary<string, object> result = new Dictionary<string, object>();
            if (rows > 0)
            {
                result.Add("rows", rows.ToString(CultureInfo.InvariantCulture));
            }
            if (columns > 0)
            {
                result.Add("cols", columns.ToString(CultureInfo.InvariantCulture));
            }

            return result;
        }

        public static FlexFormGroup TextArea(this FlexMvcForm form, string name)
        {
            return TextArea(form, name, null /* value */, null /* htmlAttributes */);
        }

        public static FlexFormGroup TextArea(this FlexMvcForm form, string name, object htmlAttributes)
        {
            return TextArea(form, name, null /* value */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup TextArea(this FlexMvcForm form, string name, IDictionary<string, object> htmlAttributes)
        {
            return TextArea(form, name, null /* value */, htmlAttributes);
        }

        public static FlexFormGroup TextArea(this FlexMvcForm form, string name, string value)
        {
            return TextArea(form, name, value, null /* htmlAttributes */);
        }

        public static FlexFormGroup TextArea(this FlexMvcForm form, string name, string value, object htmlAttributes)
        {
            return TextArea(form, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup TextArea(this FlexMvcForm form, string name, string value, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromStringExpression(name, form.FHtmlHelper.HtmlHelper.ViewContext.ViewData);
            if (value != null)
            {
                metadata.Model = value;
            }

            return TextAreaHelper(form, metadata, name, implicitRowsAndColumns, htmlAttributes);
        }

        public static FlexFormGroup TextArea(this FlexMvcForm form, string name, string value, int rows, int columns, object htmlAttributes)
        {
            return TextArea(form, name, value, rows, columns, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup TextArea(this FlexMvcForm form, string name, string value, int rows, int columns, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromStringExpression(name, form.FHtmlHelper.HtmlHelper.ViewContext.ViewData);
            if (value != null)
            {
                metadata.Model = value;
            }

            return TextAreaHelper(form, metadata, name, GetRowsAndColumnsDictionary(rows, columns), htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextAreaFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression)
        {
            return TextAreaFor(form, expression, (IDictionary<string, object>)null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextAreaFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return TextAreaFor(form, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextAreaFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return TextAreaHelper(form,
                                  ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData),
                                  ExpressionHelper.GetExpressionText(expression),
                                  implicitRowsAndColumns,
                                  htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextAreaFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, int rows, int columns, object htmlAttributes)
        {
            return TextAreaFor(form, expression, rows, columns, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> TextAreaFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, int rows, int columns, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return TextAreaHelper(form,
                                  ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData),
                                  ExpressionHelper.GetExpressionText(expression),
                                  GetRowsAndColumnsDictionary(rows, columns),
                                  htmlAttributes);
        }

        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "If this fails, it is because the string-based version had an empty 'name' parameter")]
        internal static FlexFormGroup TextAreaHelper(FlexMvcForm form, ModelMetadata modelMetadata, string name, IDictionary<string, object> rowsAndColumns, IDictionary<string, object> htmlAttributes, string innerHtmlPrefix = null)
        {
            FlexTagBuilder formGroup = TextAreaTagBuilerHelper(form, modelMetadata, name, rowsAndColumns, htmlAttributes, innerHtmlPrefix);
            return new FlexFormGroup(form.FormContext, form.FHtmlHelper, formGroup);
        }

        internal static FlexFormGroup<TModel> TextAreaHelper<TModel>(FlexMvcForm<TModel> form, ModelMetadata modelMetadata, string name, IDictionary<string, object> rowsAndColumns, IDictionary<string, object> htmlAttributes, string innerHtmlPrefix = null)
        {
            FlexTagBuilder formGroup = TextAreaTagBuilerHelper(form, modelMetadata, name, rowsAndColumns, htmlAttributes, innerHtmlPrefix);
            return new FlexFormGroup<TModel>(form.FormContext, form.FHtmlHelper, formGroup);
        }


        private static FlexTagBuilder TextAreaTagBuilerHelper(FlexMvcForm form, ModelMetadata modelMetadata, string name, IDictionary<string, object> rowsAndColumns, IDictionary<string, object> htmlAttributes, string innerHtmlPrefix = null)
        {
            FHtmlHelper htmlHelper = form.FHtmlHelper;

            string fullName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException(FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_Common_NullOrEmpty), "name");
            }

            FlexLabel label = (modelMetadata != null) ? htmlHelper.LabelHelper(modelMetadata, name) : htmlHelper.Label(name);
            FlexTextArea input = htmlHelper.TextAreaHelper(modelMetadata, name, rowsAndColumns, htmlAttributes, innerHtmlPrefix);
            FlexValidationMessage validateMessage = htmlHelper.ValidationMessageHelper(modelMetadata, name, null, null);

            FlexTagBuilder formGroup = htmlHelper.Render.FormGroupHelper(new FlexTagBuilder(), form.FormContext, label.TagBuilder, input.TagBuilder, validateMessage.TagBuilder);

            return formGroup;
        }
        #endregion


        #region STATIC

        // Static

        public static FlexFormGroup Static(this FlexMvcForm form, string name)
        {
            return Static(form, name, value: null);
        }

        public static FlexFormGroup Static(this FlexMvcForm form, string name, object value)
        {
            return Static(form, name, value, format: null);
        }

        public static FlexFormGroup Static(this FlexMvcForm form, string name, object value, string format)
        {
            return Static(form, name, value, format, htmlAttributes: (object)null);
        }

        public static FlexFormGroup Static(this FlexMvcForm form, string name, object value, object htmlAttributes)
        {
            return Static(form, name, value, format: null, htmlAttributes: htmlAttributes);
        }

        public static FlexFormGroup Static(this FlexMvcForm form, string name, object value, string format, object htmlAttributes)
        {
            return Static(form, name, value, format, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexFormGroup Static(this FlexMvcForm form, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return Static(form, name, value, format: null, htmlAttributes: htmlAttributes);
        }

        public static FlexFormGroup Static(this FlexMvcForm form, string name, object value, string format, IDictionary<string, object> htmlAttributes)
        {
            return StaticInternalHelper(form,
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
        public static FlexFormGroup<TModel> StaticFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression)
        {
            return StaticFor(form, expression, format: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> StaticFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, string format)
        {
            return StaticFor(form, expression, format, (IDictionary<string, object>)null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> StaticFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return StaticFor(form, expression, format: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> StaticFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, string format, object htmlAttributes)
        {
            return StaticFor(form, expression, format: format, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> StaticFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            return StaticFor(form, expression, format: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexFormGroup<TModel> StaticFor<TModel, TProperty>(this FlexMvcForm<TModel> form, Expression<Func<TModel, TProperty>> expression, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, form.FHtmlHelper.HtmlHelper.ViewData);
            return StaticHelper(form,
                                 metadata,
                                 metadata.Model,
                                 ExpressionHelper.GetExpressionText(expression),
                                 format,
                                 htmlAttributes);
        }

        private static FlexFormGroup StaticHelper(FlexMvcForm form, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
        {
            return StaticInternalHelper(form,
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

        private static FlexFormGroup<TModel> StaticHelper<TModel>(FlexMvcForm<TModel> form, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
        {
            return StaticInternalHelper(form,
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

        private static FlexFormGroup StaticInternalHelper(FlexMvcForm form, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder formGroup = StaticTagBuilderHelper(form, metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);

            return new FlexFormGroup(form.FormContext, form.FHtmlHelper, formGroup);
        }

        private static FlexFormGroup<TModel> StaticInternalHelper<TModel>(FlexMvcForm<TModel> form, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder formGroup = StaticTagBuilderHelper(form, metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);

            return new FlexFormGroup<TModel>(form.FormContext, form.FHtmlHelper, formGroup);
        }

        private static FlexTagBuilder StaticTagBuilderHelper(FlexMvcForm form, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            FHtmlHelper htmlHelper = form.FHtmlHelper;

            string fullName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);


            FlexLabel label = (metadata != null) ? htmlHelper.LabelHelper(metadata, name) : htmlHelper.Label(name);
            FlexInput input = htmlHelper.StaticHelper<FlexInput>(metadata, name, value, useViewData, isChecked, setId, isExplicitValue, format, htmlAttributes);            

            FlexTagBuilder formGroup = htmlHelper.Render.FormGroupHelper(new FlexTagBuilder(), form.FormContext, label.TagBuilder, input.TagBuilder, new FlexTagBuilder());
            return formGroup;
        }

        
        #endregion


        #region Button

        public static FlexFormGroup Button(this FlexMvcForm form, FlexButton button)
        {            
            FHtmlHelper htmlHelper = form.FHtmlHelper;
            
            return new FlexFormGroup(form.FormContext, htmlHelper, htmlHelper.Render.FormGroupButton(new FlexTagBuilder(), form.FormContext, button.TagBuilder));           
        
        }

        public static FlexFormGroup Button(this FlexMvcForm form, FlexTagBuilder buttonTag)
        {
            FHtmlHelper htmlHelper = form.FHtmlHelper;

            return new FlexFormGroup(form.FormContext, htmlHelper, htmlHelper.Render.FormGroupButton(new FlexTagBuilder(), form.FormContext, buttonTag));

        }

        #endregion        
    }
}
