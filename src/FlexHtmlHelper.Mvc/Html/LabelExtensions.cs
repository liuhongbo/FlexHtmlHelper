// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. see http://aspnetwebstack.codeplex.com/SourceControl/latest#License.txt for license information.
// ported from aspnetwebstack
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace FlexHtmlHelper.Mvc.Html
{
    public static class LabelExtensions
    {
        public static FlexLabel Label(this FHtmlHelper html, string expression)
        {
            return Label(html,
                         expression,
                         labelText: null);
        }

        public static FlexLabel Label(this FHtmlHelper html, string expression, string labelText)
        {
            return Label(html, expression, labelText, htmlAttributes: null);
        }

        public static FlexLabel Label(this FHtmlHelper html, string expression, object htmlAttributes)
        {
            return Label(html, expression, labelText: null, htmlAttributes: htmlAttributes);
        }

        public static FlexLabel Label(this FHtmlHelper html, string expression, IDictionary<string, object> htmlAttributes)
        {
            return Label(html, expression, labelText: null, htmlAttributes: htmlAttributes);
        }

        public static FlexLabel Label(this FHtmlHelper html, string expression, string labelText, object htmlAttributes)
        {
            return Label(html,
                        expression,
                        labelText,
                        HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexLabel Label(this FHtmlHelper html, string expression, string labelText, IDictionary<string, object> htmlAttributes)
        {
            return LabelHelper(html,
                               ModelMetadata.FromStringExpression(expression, html.HtmlHelper.ViewData),
                               expression,
                               labelText,
                               htmlAttributes);
        }      

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexLabel LabelFor<TModel, TValue>(this FHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return LabelFor<TModel, TValue>(html, expression, labelText: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexLabel LabelFor<TModel, TValue>(this FHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText)
        {
            return LabelFor(html, expression, labelText, htmlAttributes: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexLabel LabelFor<TModel, TValue>(this FHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LabelFor(html, expression, labelText: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexLabel LabelFor<TModel, TValue>(this FHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            return LabelFor(html, expression, labelText: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexLabel LabelFor<TModel, TValue>(this FHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes)
        {
            return LabelFor(html,
                            expression,
                            labelText,
                            HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexLabel LabelFor<TModel, TValue>(this FHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, IDictionary<string, object> htmlAttributes)
        {
            return LabelHelper(html,
                               ModelMetadata.FromLambdaExpression(expression, html.HtmlHelper.ViewData),
                               ExpressionHelper.GetExpressionText(expression),
                               labelText,
                               htmlAttributes);
        }      

        public static FlexLabel LabelForModel(this FHtmlHelper html)
        {
            return LabelForModel(html, labelText: null);
        }

        public static FlexLabel LabelForModel(this FHtmlHelper html, string labelText)
        {
            return LabelHelper(html, html.HtmlHelper.ViewData.ModelMetadata, String.Empty, labelText);
        }

        public static FlexLabel LabelForModel(this FHtmlHelper html, object htmlAttributes)
        {
            return LabelHelper(html, html.HtmlHelper.ViewData.ModelMetadata, String.Empty, labelText: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexLabel LabelForModel(this FHtmlHelper html, IDictionary<string, object> htmlAttributes)
        {
            return LabelHelper(html, html.HtmlHelper.ViewData.ModelMetadata, String.Empty, labelText: null, htmlAttributes: htmlAttributes);
        }

        public static FlexLabel LabelForModel(this FHtmlHelper html, string labelText, object htmlAttributes)
        {
            return LabelHelper(html, html.HtmlHelper.ViewData.ModelMetadata, String.Empty, labelText, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexLabel LabelForModel(this FHtmlHelper html, string labelText, IDictionary<string, object> htmlAttributes)
        {
            return LabelHelper(html, html.HtmlHelper.ViewData.ModelMetadata, String.Empty, labelText, htmlAttributes);
        }

        internal static FlexLabel LabelHelper(this FHtmlHelper html, ModelMetadata metadata, string htmlFieldName, string labelText = null, IDictionary<string, object> htmlAttributes = null)
        {
            string resolvedLabelText = labelText ?? metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(resolvedLabelText))
            {               
                return FlexLabel.Empty;
            }

            FlexTagBuilder label = html.Render.LabelHelper(new FlexTagBuilder(), FlexTagBuilder.CreateSanitizedId(html.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)), resolvedLabelText, htmlAttributes);

            return new FlexLabel(html, label);            
        }   

    }
   
}
