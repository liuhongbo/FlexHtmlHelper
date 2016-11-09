// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. see http://aspnetwebstack.codeplex.com/SourceControl/latest#License.txt for license information.
// ported from aspnetwebstack
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace FlexHtmlHelper.Mvc.Html
{
    public static class InputExtensions
    {
        // CheckBox

        public static FlexCheckBox CheckBox(this FHtmlHelper htmlHelper, string name)
        {
            return CheckBox(htmlHelper, name, htmlAttributes: (object)null);
        }

        public static FlexCheckBox CheckBox(this FHtmlHelper htmlHelper, string name, bool isChecked)
        {
            return CheckBox(htmlHelper, name, isChecked, htmlAttributes: (object)null);
        }

        public static FlexCheckBox CheckBox(this FHtmlHelper htmlHelper, string name, bool isChecked, object htmlAttributes)
        {
            return CheckBox(htmlHelper, name, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexCheckBox CheckBox(this FHtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            return CheckBox(htmlHelper, name, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexCheckBox CheckBox(this FHtmlHelper htmlHelper, string name, IDictionary<string, object> htmlAttributes)
        {
            return CheckBoxHelper(htmlHelper, metadata: null, name: name, isChecked: null, htmlAttributes: htmlAttributes);
        }

        public static FlexCheckBox CheckBox(this FHtmlHelper htmlHelper, string name, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            return CheckBoxHelper(htmlHelper, metadata: null, name: name, isChecked: isChecked, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexCheckBox CheckBoxFor<TModel>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression)
        {
            return CheckBoxFor(htmlHelper, expression, htmlAttributes: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexCheckBox CheckBoxFor<TModel>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object htmlAttributes)
        {
            return CheckBoxFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexCheckBox CheckBoxFor<TModel>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.HtmlHelper.ViewData);
            bool? isChecked = null;
            if (metadata.Model != null)
            {
                bool modelChecked;
                if (Boolean.TryParse(metadata.Model.ToString(), out modelChecked))
                {
                    isChecked = modelChecked;
                }
            }

            return CheckBoxHelper(htmlHelper, metadata, ExpressionHelper.GetExpressionText(expression), isChecked, htmlAttributes);
        }

        private static FlexCheckBox CheckBoxHelper(FHtmlHelper htmlHelper, ModelMetadata metadata, string name, bool? isChecked, IDictionary<string, object> htmlAttributes)
        {
            RouteValueDictionary attributes = ToRouteValueDictionary(htmlAttributes);

            bool explicitValue = isChecked.HasValue;
            if (explicitValue)
            {
                attributes.Remove("checked"); // Explicit value must override dictionary
            }

            return InputHelper<FlexCheckBox>(htmlHelper,
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

        public static FlexHidden Hidden(this FHtmlHelper htmlHelper, string name)
        {
            return Hidden(htmlHelper, name, value: null, htmlAttributes: null);
        }

        public static FlexHidden Hidden(this FHtmlHelper htmlHelper, string name, object value)
        {
            return Hidden(htmlHelper, name, value, htmlAttributes: null);
        }

        public static FlexHidden Hidden(this FHtmlHelper htmlHelper, string name, object value, object htmlAttributes)
        {
            return Hidden(htmlHelper, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexHidden Hidden(this FHtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return HiddenHelper(htmlHelper,
                                metadata: null,
                                value: value,
                                useViewData: value == null,
                                expression: name,
                                htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexHidden HiddenFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return HiddenFor(htmlHelper, expression, (IDictionary<string, object>)null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexHidden HiddenFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return HiddenFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexHidden HiddenFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.HtmlHelper.ViewData);
            return HiddenHelper(htmlHelper,
                                metadata,
                                metadata.Model,
                                false,
                                ExpressionHelper.GetExpressionText(expression),
                                htmlAttributes);
        }

        private static FlexHidden HiddenHelper(FHtmlHelper htmlHelper, ModelMetadata metadata, object value, bool useViewData, string expression, IDictionary<string, object> htmlAttributes)
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

            return InputHelper<FlexHidden>(htmlHelper,
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

        public static FlexPassword Password(this FHtmlHelper htmlHelper, string name)
        {
            return Password(htmlHelper, name, value: null);
        }

        public static FlexPassword Password(this FHtmlHelper htmlHelper, string name, object value)
        {
            return Password(htmlHelper, name, value, htmlAttributes: null);
        }

        public static FlexPassword Password(this FHtmlHelper htmlHelper, string name, object value, object htmlAttributes)
        {
            return Password(htmlHelper, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexPassword Password(this FHtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return PasswordHelper(htmlHelper, metadata: null, name: name, value: value, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexPassword PasswordFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return PasswordFor(htmlHelper, expression, htmlAttributes: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexPassword PasswordFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return PasswordFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexPassword PasswordFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return PasswordHelper(htmlHelper,
                                  ModelMetadata.FromLambdaExpression(expression, htmlHelper.HtmlHelper.ViewData),
                                  ExpressionHelper.GetExpressionText(expression),
                                  value: null,
                                  htmlAttributes: htmlAttributes);
        }

        private static FlexPassword PasswordHelper(FHtmlHelper htmlHelper, ModelMetadata metadata, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper<FlexPassword>(htmlHelper,
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

        public static FlexRadioButton RadioButton(this FHtmlHelper htmlHelper, string name, object value)
        {
            return RadioButton(htmlHelper, name, value, htmlAttributes: (object)null);
        }

        public static FlexRadioButton RadioButton(this FHtmlHelper htmlHelper, string name, object value, object htmlAttributes)
        {
            return RadioButton(htmlHelper, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexRadioButton RadioButton(this FHtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            // Determine whether or not to render the checked attribute based on the contents of ViewData.
            string valueString = Convert.ToString(value, CultureInfo.CurrentCulture);
            bool isChecked = (!String.IsNullOrEmpty(name)) && (String.Equals(htmlHelper.EvalString(name), valueString, StringComparison.OrdinalIgnoreCase));
            // checked attributes is implicit, so we need to ensure that the dictionary takes precedence.
            RouteValueDictionary attributes = ToRouteValueDictionary(htmlAttributes);
            if (attributes.ContainsKey("checked"))
            {
                return InputHelper<FlexRadioButton>(htmlHelper,
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

            return RadioButton(htmlHelper, name, value, isChecked, htmlAttributes);
        }

        public static FlexRadioButton RadioButton(this FHtmlHelper htmlHelper, string name, object value, bool isChecked)
        {
            return RadioButton(htmlHelper, name, value, isChecked, htmlAttributes: (object)null);
        }

        public static FlexRadioButton RadioButton(this FHtmlHelper htmlHelper, string name, object value, bool isChecked, object htmlAttributes)
        {
            return RadioButton(htmlHelper, name, value, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexRadioButton RadioButton(this FHtmlHelper htmlHelper, string name, object value, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            // checked attribute is an explicit parameter so it takes precedence.
            RouteValueDictionary attributes = ToRouteValueDictionary(htmlAttributes);
            attributes.Remove("checked");
            return InputHelper<FlexRadioButton>(htmlHelper,
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
        public static FlexRadioButton RadioButtonFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value)
        {
            return RadioButtonFor(htmlHelper, expression, value, htmlAttributes: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexRadioButton RadioButtonFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value, object htmlAttributes)
        {
            return RadioButtonFor(htmlHelper, expression, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexRadioButton RadioButtonFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.HtmlHelper.ViewData);
            return RadioButtonHelper(htmlHelper,
                                     metadata,
                                     metadata.Model,
                                     ExpressionHelper.GetExpressionText(expression),
                                     value,
                                     null /* isChecked */,
                                     htmlAttributes);
        }

        private static FlexRadioButton RadioButtonHelper(FHtmlHelper htmlHelper, ModelMetadata metadata, object model, string name, object value, bool? isChecked, IDictionary<string, object> htmlAttributes)
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

            return InputHelper<FlexRadioButton>(htmlHelper,
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

        public static FlexTextBox TextBox(this FHtmlHelper htmlHelper, string name)
        {
            return TextBox(htmlHelper, name, value: null);
        }

        public static FlexTextBox TextBox(this FHtmlHelper htmlHelper, string name, object value)
        {
            return TextBox(htmlHelper, name, value, format: null);
        }

        public static FlexTextBox TextBox(this FHtmlHelper htmlHelper, string name, object value, string format)
        {
            return TextBox(htmlHelper, name, value, format, htmlAttributes: (object)null);
        }

        public static FlexTextBox TextBox(this FHtmlHelper htmlHelper, string name, object value, object htmlAttributes)
        {
            return TextBox(htmlHelper, name, value, format: null, htmlAttributes: htmlAttributes);
        }

        public static FlexTextBox TextBox(this FHtmlHelper htmlHelper, string name, object value, string format, object htmlAttributes)
        {
            return TextBox(htmlHelper, name, value, format, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexTextBox TextBox(this FHtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return TextBox(htmlHelper, name, value, format: null, htmlAttributes: htmlAttributes);
        }

        public static FlexTextBox TextBox(this FHtmlHelper htmlHelper, string name, object value, string format, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper<FlexTextBox>(htmlHelper,
                               InputType.Text,
                               metadata: null,
                               name: name,
                               value: value,
                               useViewData: (value == null),
                               isChecked: false,
                               setId: true,
                               isExplicitValue: true,
                               format: format,
                               htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexTextBox TextBoxFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.TextBoxFor(expression, format: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexTextBox TextBoxFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format)
        {   
            return htmlHelper.TextBoxFor(expression, format, (IDictionary<string, object>)null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexTextBox TextBoxFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return htmlHelper.TextBoxFor(expression, format: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexTextBox TextBoxFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, object htmlAttributes)
        {
            return htmlHelper.TextBoxFor(expression, format: format, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexTextBox TextBoxFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.TextBoxFor(expression, format: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexTextBox TextBoxFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.HtmlHelper.ViewData);
            return TextBoxHelper(htmlHelper,
                                 metadata,
                                 metadata.Model,
                                 ExpressionHelper.GetExpressionText(expression),
                                 format,
                                 htmlAttributes);
        }

        private static FlexTextBox TextBoxHelper(this FHtmlHelper htmlHelper, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
        {
            return InputHelper<FlexTextBox>(htmlHelper,
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

        internal static T InputHelper<T>(this FHtmlHelper htmlHelper, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            string fullName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }          

            string valueParameter = htmlHelper.HtmlHelper.FormatValue(value, format);
            bool usedModelState = false;
            string attemptedValue = null;           
           
            FlexTagBuilder input = null;

            switch (inputType)
            {
                case InputType.CheckBox:
                    bool? modelStateWasChecked = htmlHelper.GetModelStateValue(fullName, typeof(bool)) as bool?;
                    if (modelStateWasChecked.HasValue)
                    {
                        isChecked = modelStateWasChecked.Value;
                        usedModelState = true;
                    }
                    if (!usedModelState)
                    {
                        string modelStateValue = htmlHelper.GetModelStateValue(fullName, typeof(string)) as string;
                        if (modelStateValue != null)
                        {
                            isChecked = String.Equals(modelStateValue, valueParameter, StringComparison.Ordinal);
                            usedModelState = true;
                        }
                    }
                    if (!usedModelState && useViewData)
                    {
                        isChecked = htmlHelper.EvalBoolean(fullName);
                    }
                    input = htmlHelper.Render.CheckBoxHelper(new FlexTagBuilder(), fullName, htmlHelper.Template.GetChecked(fullName, valueParameter, isChecked), htmlHelper.Template.GetValue(fullName, valueParameter), htmlAttributes);                   
                    break;
                case InputType.Hidden:
                    attemptedValue = (string)htmlHelper.GetModelStateValue(fullName, typeof(string));
                    input = htmlHelper.Render.HiddenHelper(new FlexTagBuilder(), fullName, htmlHelper.Template.GetValue(fullName, attemptedValue ?? ((useViewData) ? htmlHelper.EvalString(fullName, format) : valueParameter)), htmlAttributes);
                    break;
                case InputType.Password:
                    input = htmlHelper.Render.PasswordHelper(new FlexTagBuilder(), fullName, htmlHelper.Template.GetValue(fullName, valueParameter), htmlAttributes);
                    break;
                case InputType.Radio:
                    if (!usedModelState)
                    {
                        string modelStateValue = htmlHelper.GetModelStateValue(fullName, typeof(string)) as string;
                        if (modelStateValue != null)
                        {
                            isChecked = String.Equals(modelStateValue, valueParameter, StringComparison.Ordinal);
                            usedModelState = true;
                        }
                    }
                    if (!usedModelState && useViewData)
                    {
                        isChecked = htmlHelper.EvalBoolean(fullName);
                    }
                    input = htmlHelper.Render.RadioHelper(new FlexTagBuilder(), fullName, htmlHelper.Template.GetChecked(fullName, valueParameter, isChecked), valueParameter, htmlAttributes);
                    break;
                case InputType.Text:
                    attemptedValue = (string)htmlHelper.GetModelStateValue(fullName, typeof(string));
                    input = htmlHelper.Render.TextBoxHelper(new FlexTagBuilder(), fullName, htmlHelper.Template.GetValue(fullName, attemptedValue ?? ((useViewData) ? htmlHelper.EvalString(fullName, format) : valueParameter)), htmlAttributes);
                    break;
            }

            //client side validation
            if (input != null)
            {
                ModelState modelState;                
                if (htmlHelper.HtmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
                {
                    if (modelState.Errors.Count > 0)
                    {
                        input.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                        
                    }
                }               
                input.MergeAttributes(htmlHelper.HtmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));
            }
            else
            {
                input = new FlexTagBuilder();
            }

            Object ret = null;

            switch (inputType)
            {
                case InputType.CheckBox:                    
                    ret = new FlexCheckBox(htmlHelper, input);
                    break;
                case InputType.Hidden:                    
                    ret = new FlexHidden(htmlHelper, input);
                    break;
                case InputType.Password:
                    ret = new FlexPassword(htmlHelper, input);
                    break;
                case InputType.Radio:
                    ret = new FlexRadioButton(htmlHelper, input);
                    break;
                case InputType.Text:
                    ret =  new FlexTextBox(htmlHelper, input);
                    break;
            }
            return (T)ret;
        }

        private static RouteValueDictionary ToRouteValueDictionary(IDictionary<string, object> dictionary)
        {
            return dictionary == null ? new RouteValueDictionary() : new RouteValueDictionary(dictionary);
        }

        internal static T StaticHelper<T>(this FHtmlHelper htmlHelper,ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
        {
            string fullName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }

            string valueParameter = htmlHelper.FormatValue(value, format);
            
            string attemptedValue = null;

            FlexTagBuilder input = null;
           
            attemptedValue = (string)htmlHelper.GetModelStateValue(fullName, typeof(string));
            input = htmlHelper.Render.StaticHelper(new FlexTagBuilder(), fullName, htmlHelper.Template.GetValue(fullName, attemptedValue ?? ((useViewData) ? htmlHelper.EvalString(fullName, format) : valueParameter)), htmlAttributes);
           
            Object ret = null;

            ret = new FlexStatic(htmlHelper, input);
                   
            return (T)ret;
        }
    }
}
