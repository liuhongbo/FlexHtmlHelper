// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace FlexHtmlHelper.Mvc.Html
{
    public static class ValidationExtensions
    {
        private const string HiddenListItem = @"<li style=""display:none""></li>";
        private static string _resourceClassKey;       

        public static string ResourceClassKey
        {
            get { return _resourceClassKey ?? String.Empty; }
            set { _resourceClassKey = value; }
        }

        private static FieldValidationMetadata ApplyFieldValidationMetadata(FHtmlHelper htmlHelper, ModelMetadata modelMetadata, string modelName)
        {
            FormContext formContext = htmlHelper.HtmlHelper.ViewContext.FormContext;
            FieldValidationMetadata fieldMetadata = formContext.GetValidationMetadataForField(modelName, true /* createIfNotFound */);

            // write rules to context object
            IEnumerable<ModelValidator> validators = ModelValidatorProviders.Providers.GetValidators(modelMetadata, htmlHelper.HtmlHelper.ViewContext);
            foreach (ModelClientValidationRule rule in validators.SelectMany(v => v.GetClientValidationRules()))
            {
                fieldMetadata.ValidationRules.Add(rule);
            }

            return fieldMetadata;
        }

        private static string GetInvalidPropertyValueResource(HttpContextBase httpContext)
        {
            string resourceValue = null;
            if (!String.IsNullOrEmpty(ResourceClassKey) && (httpContext != null))
            {
                // If the user specified a ResourceClassKey try to load the resource they specified.
                // If the class key is invalid, an exception will be thrown.
                // If the class key is valid but the resource is not found, it returns null, in which
                // case it will fall back to the MVC default error message.
                resourceValue = httpContext.GetGlobalResourceObject(ResourceClassKey, "InvalidPropertyValue", CultureInfo.CurrentUICulture) as string;
            }
            return resourceValue ?? FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_Common_ValueNotValidForProperty);
        }

        private static string GetUserErrorMessageOrDefault(HttpContextBase httpContext, ModelError error, ModelState modelState)
        {
            if (!String.IsNullOrEmpty(error.ErrorMessage))
            {
                return error.ErrorMessage;
            }
            if (modelState == null)
            {
                return null;
            }

            string attemptedValue = (modelState.Value != null) ? modelState.Value.AttemptedValue : null;
            return String.Format(CultureInfo.CurrentCulture, GetInvalidPropertyValueResource(httpContext), attemptedValue);
        }

        // Validate

        public static void Validate(this FHtmlHelper htmlHelper, string modelName)
        {
            if (modelName == null)
            {
                throw new ArgumentNullException("modelName");
            }

            ValidateHelper(htmlHelper,
                           ModelMetadata.FromStringExpression(modelName, htmlHelper.HtmlHelper.ViewContext.ViewData),
                           modelName);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static void ValidateFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            ValidateHelper(htmlHelper,
                           ModelMetadata.FromLambdaExpression(expression, htmlHelper.HtmlHelper.ViewData),
                           ExpressionHelper.GetExpressionText(expression));
        }

        private static void ValidateHelper(FHtmlHelper htmlHelper, ModelMetadata modelMetadata, string expression)
        {
            FormContext formContext = htmlHelper.HtmlHelper.ViewContext.ClientValidationEnabled ? htmlHelper.HtmlHelper.ViewContext.FormContext : null;
            if (formContext == null || htmlHelper.HtmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
            {
                return; // nothing to do
            }

            string modelName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
            ApplyFieldValidationMetadata(htmlHelper, modelMetadata, modelName);
        }

        // ValidationMessage

        public static FlexValidationMessage ValidationMessage(this FHtmlHelper htmlHelper, string modelName)
        {
            return ValidationMessage(htmlHelper, modelName, null /* validationMessage */, new RouteValueDictionary());
        }

        public static FlexValidationMessage ValidationMessage(this FHtmlHelper htmlHelper, string modelName, object htmlAttributes)
        {
            return ValidationMessage(htmlHelper, modelName, null /* validationMessage */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", Justification = "'validationMessage' refers to the message that will be rendered by the ValidationMessage helper.")]
        public static FlexValidationMessage ValidationMessage(this FHtmlHelper htmlHelper, string modelName, string validationMessage)
        {
            return ValidationMessage(htmlHelper, modelName, validationMessage, new RouteValueDictionary());
        }

        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", Justification = "'validationMessage' refers to the message that will be rendered by the ValidationMessage helper.")]
        public static FlexValidationMessage ValidationMessage(this FHtmlHelper htmlHelper, string modelName, string validationMessage, object htmlAttributes)
        {
            return ValidationMessage(htmlHelper, modelName, validationMessage, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexValidationMessage ValidationMessage(this FHtmlHelper htmlHelper, string modelName, IDictionary<string, object> htmlAttributes)
        {
            return ValidationMessage(htmlHelper, modelName, null /* validationMessage */, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", Justification = "'validationMessage' refers to the message that will be rendered by the ValidationMessage helper.")]
        public static FlexValidationMessage ValidationMessage(this FHtmlHelper htmlHelper, string modelName, string validationMessage, IDictionary<string, object> htmlAttributes)
        {
            if (modelName == null)
            {
                throw new ArgumentNullException("modelName");
            }

            return ValidationMessageHelper(htmlHelper,
                                           ModelMetadata.FromStringExpression(modelName, htmlHelper.HtmlHelper.ViewContext.ViewData),
                                           modelName,
                                           validationMessage,
                                           htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexValidationMessage ValidationMessageFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return ValidationMessageFor(htmlHelper, expression, null /* validationMessage */, new RouteValueDictionary());
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexValidationMessage ValidationMessageFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage)
        {
            return ValidationMessageFor(htmlHelper, expression, validationMessage, new RouteValueDictionary());
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexValidationMessage ValidationMessageFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, object htmlAttributes)
        {
            return ValidationMessageFor(htmlHelper, expression, validationMessage, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexValidationMessage ValidationMessageFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, IDictionary<string, object> htmlAttributes)
        {
            return ValidationMessageHelper(htmlHelper,
                                           ModelMetadata.FromLambdaExpression(expression, htmlHelper.HtmlHelper.ViewData),
                                           ExpressionHelper.GetExpressionText(expression),
                                           validationMessage,
                                           htmlAttributes);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Normalization to lowercase is a common requirement for JavaScript and HTML values")]
        internal static FlexValidationMessage ValidationMessageHelper(this FHtmlHelper htmlHelper, ModelMetadata modelMetadata, string expression, string validationMessage, IDictionary<string, object> htmlAttributes)
        {
            string modelName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
            FormContext formContext = htmlHelper.HtmlHelper.ViewContext.ClientValidationEnabled ? htmlHelper.HtmlHelper.ViewContext.FormContext : null;

            if (!htmlHelper.HtmlHelper.ViewData.ModelState.ContainsKey(modelName) && formContext == null)
            {
                return FlexValidationMessage.Empty;
            }

            ModelState modelState = htmlHelper.HtmlHelper.ViewData.ModelState[modelName];
            ModelErrorCollection modelErrors = (modelState == null) ? null : modelState.Errors;
            ModelError modelError = (((modelErrors == null) || (modelErrors.Count == 0)) ? null : modelErrors.FirstOrDefault(m => !String.IsNullOrEmpty(m.ErrorMessage)) ?? modelErrors[0]);

            if (modelError == null && formContext == null)
            {
                return FlexValidationMessage.Empty;
            }

            string message = null;

            if (!String.IsNullOrEmpty(validationMessage))
            {
                message = validationMessage;
            }
            else if (modelError != null)
            {
                message = GetUserErrorMessageOrDefault(htmlHelper.HtmlHelper.ViewContext.HttpContext, modelError, modelState);
            }

            FlexTagBuilder vm = htmlHelper.Render.ValidationMessageHelper(new FlexTagBuilder(), message, (modelError != null), htmlAttributes);

            vm.AddCssClass((modelError != null) ? HtmlHelper.ValidationMessageCssClassName : HtmlHelper.ValidationMessageValidCssClassName);

            if (formContext != null)
            {
                bool replaceValidationMessageContents = String.IsNullOrEmpty(validationMessage);

                if (htmlHelper.HtmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
                {
                    vm.MergeAttribute("data-valmsg-for", modelName);
                    vm.MergeAttribute("data-valmsg-replace", replaceValidationMessageContents.ToString().ToLowerInvariant());
                }
                else
                {
                    FieldValidationMetadata fieldMetadata = ApplyFieldValidationMetadata(htmlHelper, modelMetadata, modelName);
                    // rules will already have been written to the metadata object
                    fieldMetadata.ReplaceValidationMessageContents = replaceValidationMessageContents; // only replace contents if no explicit message was specified

                    // client validation always requires an ID
                    vm.GenerateId(modelName + "_validationMessage");
                    fieldMetadata.ValidationMessageId = vm.Attributes["id"];
                }
            }

            return new FlexValidationMessage(htmlHelper, vm);
        }

        // ValidationSummary

        public static FlexValidationSummary ValidationSummary(this FHtmlHelper htmlHelper)
        {
            return ValidationSummary(htmlHelper, false /* excludePropertyErrors */);
        }

        public static FlexValidationSummary ValidationSummary(this FHtmlHelper htmlHelper, bool excludePropertyErrors)
        {
            return ValidationSummary(htmlHelper, excludePropertyErrors, null /* message */);
        }

        public static FlexValidationSummary ValidationSummary(this FHtmlHelper htmlHelper, string message)
        {
            return ValidationSummary(htmlHelper, false /* excludePropertyErrors */, message, (object)null /* htmlAttributes */);
        }

        public static FlexValidationSummary ValidationSummary(this FHtmlHelper htmlHelper, bool excludePropertyErrors, string message)
        {
            return ValidationSummary(htmlHelper, excludePropertyErrors, message, (object)null /* htmlAttributes */);
        }

        public static FlexValidationSummary ValidationSummary(this FHtmlHelper htmlHelper, string message, object htmlAttributes)
        {
            return ValidationSummary(htmlHelper, false /* excludePropertyErrors */, message, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexValidationSummary ValidationSummary(this FHtmlHelper htmlHelper, bool excludePropertyErrors, string message, object htmlAttributes)
        {
            return ValidationSummary(htmlHelper, excludePropertyErrors, message, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexValidationSummary ValidationSummary(this FHtmlHelper htmlHelper, string message, IDictionary<string, object> htmlAttributes)
        {
            return ValidationSummary(htmlHelper, false /* excludePropertyErrors */, message, htmlAttributes);
        }

        internal static FlexValidationSummary ValidationSummary(this FHtmlHelper htmlHelper, bool excludePropertyErrors, string message, IDictionary<string, object> htmlAttributes)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            FormContext formContext = htmlHelper.HtmlHelper.ViewContext.ClientValidationEnabled ? htmlHelper.HtmlHelper.ViewContext.FormContext : null;
            if (htmlHelper.HtmlHelper.ViewData.ModelState.IsValid)
            {
                if (formContext == null)
                {
                    // No client side validation
                    return FlexValidationSummary.Empty;
                }
                // TODO: This isn't really about unobtrusive; can we fix up non-unobtrusive to get rid of this, too?
                if (htmlHelper.HtmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled && excludePropertyErrors)
                {
                    // No client-side updates
                    return FlexValidationSummary.Empty;
                }
            }           

            IEnumerable<ModelState> modelStates = GetModelStateList(htmlHelper, excludePropertyErrors);
            List<string> errorMessages = new List<string>();

            foreach (ModelState modelState in modelStates)
            {
                foreach (ModelError modelError in modelState.Errors)
                {
                    string errorText = GetUserErrorMessageOrDefault(htmlHelper.HtmlHelper.ViewContext.HttpContext, modelError, null /* modelState */);
                    if (!String.IsNullOrEmpty(errorText))
                    {                       
                        errorMessages.Add(errorText);
                    }
                }
            }

       
            FlexTagBuilder summary = htmlHelper.Render.ValidationSummaryHelper(new FlexTagBuilder(), message, errorMessages, htmlAttributes);

            summary.AddCssClass((htmlHelper.HtmlHelper.ViewData.ModelState.IsValid) ? HtmlHelper.ValidationSummaryValidCssClassName : HtmlHelper.ValidationSummaryCssClassName);
           
            if (formContext != null)
            {
                if (htmlHelper.HtmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
                {
                    if (!excludePropertyErrors)
                    {
                        // Only put errors in the validation summary if they're supposed to be included there
                        summary.MergeAttribute("data-valmsg-summary", "true");
                    }
                }
                else
                {
                    // client val summaries need an ID
                    summary.GenerateId("validationSummary");
                    formContext.ValidationSummaryId = summary.Attributes["id"];
                    formContext.ReplaceValidationSummary = !excludePropertyErrors;
                }
            }
            return new FlexValidationSummary(htmlHelper, summary);
        }

        // Returns non-null list of model states, which caller will render in order provided.
        private static IEnumerable<ModelState> GetModelStateList(FHtmlHelper htmlHelper, bool excludePropertyErrors)
        {
            if (excludePropertyErrors)
            {
                ModelState ms;
                htmlHelper.HtmlHelper.ViewData.ModelState.TryGetValue(htmlHelper.HtmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix, out ms);
                if (ms != null)
                {
                    return new ModelState[] { ms };
                }

                return new ModelState[0];
            }
            else
            {
                // Sort modelStates to respect the ordering in the metadata.                 
                // ModelState doesn't refer to ModelMetadata, but we can correlate via the property name.
                Dictionary<string, int> ordering = new Dictionary<string, int>();

                var metadata = htmlHelper.HtmlHelper.ViewData.ModelMetadata;
                if (metadata != null)
                {
                    foreach (ModelMetadata m in metadata.Properties)
                    {
                        ordering[m.PropertyName] = m.Order;
                    }
                }

                return from kv in htmlHelper.HtmlHelper.ViewData.ModelState
                       let name = kv.Key
                       orderby ordering.GetOrDefault(name, ModelMetadata.DefaultOrder)
                       select kv.Value;
            }
        }
    }
}
