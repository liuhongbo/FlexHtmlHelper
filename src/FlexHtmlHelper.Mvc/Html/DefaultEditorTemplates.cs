﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace FlexHtmlHelper.Html
{
    internal static class DefaultEditorTemplates
    {
        private const string HtmlAttributeKey = "htmlAttributes";

        internal static string BooleanTemplate(FlexHtmlHelper html)
        {
            bool? value = null;
            if (html.ViewContext.ViewData.Model != null)
            {
                value = Convert.ToBoolean(html.ViewContext.ViewData.Model, CultureInfo.InvariantCulture);
            }

            return html.ViewContext.ViewData.ModelMetadata.IsNullableValueType
                       ? BooleanTemplateDropDownList(html, value)
                       : BooleanTemplateCheckbox(html, value ?? false);
        }

        private static string BooleanTemplateCheckbox(FlexHtmlHelper html, bool value)
        {
            return html.CheckBox(String.Empty, value, CreateHtmlAttributes(html, "check-box")).ToHtmlString();
        }

        private static string BooleanTemplateDropDownList(FlexHtmlHelper html, bool? value)
        {
            return html.DropDownList(String.Empty, TriStateValues(value), CreateHtmlAttributes(html, "list-box tri-state")).ToHtmlString();
        }

        internal static string CollectionTemplate(FlexHtmlHelper html)
        {
            return CollectionTemplate(html, TemplateHelpers.TemplateHelper);
        }

        internal static string CollectionTemplate(FlexHtmlHelper html, TemplateHelpers.TemplateHelperDelegate templateHelper)
        {
            ViewDataDictionary viewData = html.ViewContext.ViewData;
            object model = viewData.ModelMetadata.Model;
            if (model == null)
            {
                return String.Empty;
            }

            IEnumerable collection = model as IEnumerable;
            if (collection == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        MvcResources.Templates_TypeMustImplementIEnumerable,
                        model.GetType().FullName));
            }

            Type typeInCollection = typeof(string);
            Type genericEnumerableType = TypeHelpers.ExtractGenericInterface(collection.GetType(), typeof(IEnumerable<>));
            if (genericEnumerableType != null)
            {
                typeInCollection = genericEnumerableType.GetGenericArguments()[0];
            }
            bool typeInCollectionIsNullableValueType = TypeHelpers.IsNullableValueType(typeInCollection);

            string oldPrefix = viewData.TemplateInfo.HtmlFieldPrefix;

            try
            {
                viewData.TemplateInfo.HtmlFieldPrefix = String.Empty;

                string fieldNameBase = oldPrefix;
                StringBuilder result = new StringBuilder();
                int index = 0;

                foreach (object item in collection)
                {
                    Type itemType = typeInCollection;
                    if (item != null && !typeInCollectionIsNullableValueType)
                    {
                        itemType = item.GetType();
                    }
                    ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => item, itemType);
                    string fieldName = String.Format(CultureInfo.InvariantCulture, "{0}[{1}]", fieldNameBase, index++);
                    string output = templateHelper(html, metadata, fieldName, null /* templateName */, DataBoundControlMode.Edit, null /* additionalViewData */);
                    result.Append(output);
                }

                return result.ToString();
            }
            finally
            {
                viewData.TemplateInfo.HtmlFieldPrefix = oldPrefix;
            }
        }

        internal static string DecimalTemplate(FlexHtmlHelper html)
        {
            if (html.ViewContext.ViewData.TemplateInfo.FormattedModelValue == html.ViewContext.ViewData.ModelMetadata.Model)
            {
                html.ViewContext.ViewData.TemplateInfo.FormattedModelValue = String.Format(CultureInfo.CurrentCulture, "{0:0.00}", html.ViewContext.ViewData.ModelMetadata.Model);
            }

            return StringTemplate(html);
        }

        internal static string HiddenInputTemplate(FlexHtmlHelper html)
        {
            string result;
            ViewDataDictionary viewData = html.ViewContext.ViewData;
            if (viewData.ModelMetadata.HideSurroundingHtml)
            {
                result = String.Empty;
            }
            else
            {
                result = DefaultDisplayTemplates.StringTemplate(html);
            }

            object model = viewData.Model;

            Binary modelAsBinary = model as Binary;
            if (modelAsBinary != null)
            {
                model = Convert.ToBase64String(modelAsBinary.ToArray());
            }
            else
            {
                byte[] modelAsByteArray = model as byte[];
                if (modelAsByteArray != null)
                {
                    model = Convert.ToBase64String(modelAsByteArray);
                }
            }

            object htmlAttributesObject = viewData[HtmlAttributeKey];
            result += html.Hidden(String.Empty, model, htmlAttributesObject).ToHtmlString();
            return result;
        }

        internal static string MultilineTextTemplate(FlexHtmlHelper html)
        {
            return html.TextArea(String.Empty,
                                 html.ViewContext.ViewData.TemplateInfo.FormattedModelValue.ToString(),
                                 0 /* rows */, 0 /* columns */,
                                 CreateHtmlAttributes(html, "text-box multi-line")).ToHtmlString();
        }

        private static IDictionary<string, object> CreateHtmlAttributes(FlexHtmlHelper html, string className, string inputType = null)
        {
            object htmlAttributesObject = html.ViewContext.ViewData[HtmlAttributeKey];
            if (htmlAttributesObject != null)
            {
                return MergeHtmlAttributes(htmlAttributesObject, className, inputType);
            }
            
            var htmlAttributes = new Dictionary<string, object>()
            { 
                { "class", className }
            };
            if (inputType != null)
            {
                htmlAttributes.Add("type", inputType);
            }
            return htmlAttributes;
        }

        private static IDictionary<string, object> MergeHtmlAttributes(object htmlAttributesObject, string className, string inputType)
        {
            RouteValueDictionary htmlAttributes = FlexHtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObject);
            string htmlClassName;
            if (htmlAttributes.TryGetValue("class", out htmlClassName))
            {
                htmlClassName += " " + className;
                htmlAttributes["class"] = htmlClassName;
            }
            else
            {
                htmlAttributes.Add("class", className);
            }

            // The input type from the provided htmlAttributes overrides the inputType parameter.
            if (inputType != null && !htmlAttributes.ContainsKey("type"))
            {
                htmlAttributes.Add("type", inputType);
            }

            return htmlAttributes;
        }

        internal static string ObjectTemplate(FlexHtmlHelper html)
        {
            return ObjectTemplate(html, TemplateHelpers.TemplateHelper);
        }

        internal static string ObjectTemplate(FlexHtmlHelper html, TemplateHelpers.TemplateHelperDelegate templateHelper)
        {
            ViewDataDictionary viewData = html.ViewContext.ViewData;
            TemplateInfo templateInfo = viewData.TemplateInfo;
            ModelMetadata modelMetadata = viewData.ModelMetadata;
            StringBuilder builder = new StringBuilder();

            if (templateInfo.TemplateDepth > 1)
            {
                // DDB #224751
                return modelMetadata.Model == null ? modelMetadata.NullDisplayText : modelMetadata.SimpleDisplayText;
            }

            foreach (ModelMetadata propertyMetadata in modelMetadata.Properties.Where(pm => ShouldShow(pm, templateInfo)))
            {
                if (!propertyMetadata.HideSurroundingHtml)
                {
                    string label = LabelExtensions.LabelHelper(html, propertyMetadata, propertyMetadata.PropertyName).ToHtmlString();
                    if (!String.IsNullOrEmpty(label))
                    {
                        builder.AppendFormat(CultureInfo.InvariantCulture, "<div class=\"editor-label\">{0}</div>\r\n", label);
                    }

                    builder.Append("<div class=\"editor-field\">");
                }

                builder.Append(templateHelper(html, propertyMetadata, propertyMetadata.PropertyName, null /* templateName */, DataBoundControlMode.Edit, null /* additionalViewData */));

                if (!propertyMetadata.HideSurroundingHtml)
                {
                    builder.Append(" ");
                    builder.Append(html.ValidationMessage(propertyMetadata.PropertyName));
                    builder.Append("</div>\r\n");
                }
            }

            return builder.ToString();
        }

        internal static string PasswordTemplate(FlexHtmlHelper html)
        {
            return html.Password(String.Empty,
                                 html.ViewContext.ViewData.TemplateInfo.FormattedModelValue,
                                 CreateHtmlAttributes(html, "text-box single-line password")).ToHtmlString();
        }

        private static bool ShouldShow(ModelMetadata metadata, TemplateInfo templateInfo)
        {
            return
                metadata.ShowForEdit
                && metadata.ModelType != typeof(EntityState)
                && !metadata.IsComplexType
                && !templateInfo.Visited(metadata);
        }

        internal static string StringTemplate(FlexHtmlHelper html)
        {
            return HtmlInputTemplateHelper(html);
        }

        internal static string PhoneNumberInputTemplate(FlexHtmlHelper html)
        {
            return HtmlInputTemplateHelper(html, inputType: "tel");
        }

        internal static string UrlInputTemplate(FlexHtmlHelper html)
        {
            return HtmlInputTemplateHelper(html, inputType: "url");
        }

        internal static string EmailAddressInputTemplate(FlexHtmlHelper html)
        {
            return HtmlInputTemplateHelper(html, inputType: "email");
        }

        internal static string DateTimeInputTemplate(FlexHtmlHelper html)
        {
            ApplyRfc3339DateFormattingIfNeeded(html, "{0:yyyy-MM-ddTHH:mm:ss.fffK}");
            return HtmlInputTemplateHelper(html, inputType: "datetime");
        }

        internal static string DateTimeLocalInputTemplate(FlexHtmlHelper html)
        {
            ApplyRfc3339DateFormattingIfNeeded(html, "{0:yyyy-MM-ddTHH:mm:ss.fff}");
            return HtmlInputTemplateHelper(html, inputType: "datetime-local");
        }

        internal static string DateInputTemplate(FlexHtmlHelper html)
        {
            ApplyRfc3339DateFormattingIfNeeded(html, "{0:yyyy-MM-dd}");
            return HtmlInputTemplateHelper(html, inputType: "date");
        }

        internal static string TimeInputTemplate(FlexHtmlHelper html)
        {
            ApplyRfc3339DateFormattingIfNeeded(html, "{0:HH:mm:ss.fff}");
            return HtmlInputTemplateHelper(html, inputType: "time");
        }

        internal static string NumberInputTemplate(FlexHtmlHelper html)
        {
            return HtmlInputTemplateHelper(html, inputType: "number");
        }

        internal static string ColorInputTemplate(FlexHtmlHelper html)
        {
            string value = null;
            if (html.ViewContext.ViewData.Model != null)
            {
                if (html.ViewContext.ViewData.Model is Color)
                {
                    Color color = (Color)html.ViewContext.ViewData.Model;
                    value = String.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
                }
                else
                {
                    value = html.ViewContext.ViewData.Model.ToString();
                }
            }

            return HtmlInputTemplateHelper(html, "color", value);
        }

        private static void ApplyRfc3339DateFormattingIfNeeded(FlexHtmlHelper html, string format)
        {
            if (html.Html5DateRenderingMode != Html5DateRenderingMode.Rfc3339)
            {
                return;
            }

            ModelMetadata metadata = html.ViewContext.ViewData.ModelMetadata;
            object value = metadata.Model;
            if (html.ViewContext.ViewData.TemplateInfo.FormattedModelValue != value && metadata.HasNonDefaultEditFormat)
            {
                return;
            }

            if (value is DateTime || value is DateTimeOffset)
            {
                html.ViewContext.ViewData.TemplateInfo.FormattedModelValue = String.Format(CultureInfo.InvariantCulture, format, value);
            }
        }

        private static string HtmlInputTemplateHelper(FlexHtmlHelper html, string inputType = null)
        {
            return HtmlInputTemplateHelper(html, inputType, html.ViewContext.ViewData.TemplateInfo.FormattedModelValue);
        }

        private static string HtmlInputTemplateHelper(FlexHtmlHelper html, string inputType, object value)
        {
            return html.TextBox(
                    name: String.Empty,
                    value: value,
                    htmlAttributes: CreateHtmlAttributes(html, className: "text-box single-line", inputType: inputType))
                .ToHtmlString();
        }

        internal static List<SelectListItem> TriStateValues(bool? value)
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = MvcResources.Common_TriState_NotSet, Value = String.Empty, Selected = !value.HasValue },
                new SelectListItem { Text = MvcResources.Common_TriState_True, Value = "true", Selected = value.HasValue && value.Value },
                new SelectListItem { Text = MvcResources.Common_TriState_False, Value = "false", Selected = value.HasValue && !value.Value },
            };
        }
    }
}