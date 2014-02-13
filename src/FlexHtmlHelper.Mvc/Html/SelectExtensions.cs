﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Properties;

namespace FlexHtmlHelper.Mvc.Html
{
    public static class SelectExtensions
    {
        // DropDownList

        public static FlexSelect DropDownList(this FHtmlHelper htmlHelper, string name)
        {
            return DropDownList(htmlHelper, name, null /* selectList */, null /* optionLabel */, null /* htmlAttributes */);
        }

        public static FlexSelect DropDownList(this FHtmlHelper htmlHelper, string name, string optionLabel)
        {
            return DropDownList(htmlHelper, name, null /* selectList */, optionLabel, null /* htmlAttributes */);
        }

        public static FlexSelect DropDownList(this FHtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            return DropDownList(htmlHelper, name, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        public static FlexSelect DropDownList(this FHtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return DropDownList(htmlHelper, name, selectList, null /* optionLabel */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexSelect DropDownList(this FHtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return DropDownList(htmlHelper, name, selectList, null /* optionLabel */, htmlAttributes);
        }

        public static FlexSelect DropDownList(this FHtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel)
        {
            return DropDownList(htmlHelper, name, selectList, optionLabel, null /* htmlAttributes */);
        }

        public static FlexSelect DropDownList(this FHtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return DropDownList(htmlHelper, name, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexSelect DropDownList(this FHtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return DropDownListHelper(htmlHelper, metadata: null, expression: name, selectList: selectList, optionLabel: optionLabel, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect DropDownListFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            return DropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect DropDownListFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return DropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect DropDownListFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return DropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect DropDownListFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel)
        {
            return DropDownListFor(htmlHelper, expression, selectList, optionLabel, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect DropDownListFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return DropDownListFor(htmlHelper, expression, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect DropDownListFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.HtmlHelper.ViewData);

            return DropDownListHelper(htmlHelper, metadata, ExpressionHelper.GetExpressionText(expression), selectList, optionLabel, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect EnumDropDownListFor<TModel, TEnum>(this FHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression)
        {
            return EnumDropDownListFor(htmlHelper, expression, optionLabel: null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect EnumDropDownListFor<TModel, TEnum>(this FHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            return EnumDropDownListFor(htmlHelper, expression, optionLabel: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect EnumDropDownListFor<TModel, TEnum>(this FHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, IDictionary<string, object> htmlAttributes)
        {
            return EnumDropDownListFor(htmlHelper, expression, optionLabel: null, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect EnumDropDownListFor<TModel, TEnum>(this FHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, string optionLabel)
        {
            return EnumDropDownListFor(htmlHelper, expression, optionLabel, (IDictionary<string, object>)null);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect EnumDropDownListFor<TModel, TEnum>(this FHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes)
        {
            return EnumDropDownListFor(htmlHelper, expression, optionLabel,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        // Unable to constrain TEnum.  Cannot include IComparable, IConvertible, IFormattable because Nullable<T> does
        // not implement those interfaces (and Int32 does).  Enum alone is not compatible with expression restrictions
        // because that requires a cast from all enum types.  And the struct generic constraint disallows passing a
        // Nullable<T> expression.
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect EnumDropDownListFor<TModel, TEnum>(this FHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.HtmlHelper.ViewData);
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
                htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionName);
            Enum currentValue = null;
            if (!String.IsNullOrEmpty(expressionFullName))
            {
                currentValue = htmlHelper.GetModelStateValue(expressionFullName, metadata.ModelType) as Enum;
            }

            if (currentValue == null && !String.IsNullOrEmpty(expressionName))
            {
                // Ignore any select list (enumerable with this name) in the view data
                currentValue = htmlHelper.HtmlHelper.ViewData.Eval(expressionName) as Enum;
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

            return DropDownListHelper(htmlHelper, metadata, expressionName, selectList, optionLabel, htmlAttributes);
        }

        private static FlexSelect DropDownListHelper(FHtmlHelper htmlHelper, ModelMetadata metadata, string expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(htmlHelper, metadata, optionLabel, expression, selectList, allowMultiple: false, htmlAttributes: htmlAttributes);
        }

        // ListBox

        public static FlexSelect ListBox(this FHtmlHelper htmlHelper, string name)
        {
            return ListBox(htmlHelper, name, null /* selectList */, null /* htmlAttributes */);
        }

        public static FlexSelect ListBox(this FHtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            return ListBox(htmlHelper, name, selectList, (IDictionary<string, object>)null);
        }

        public static FlexSelect ListBox(this FHtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return ListBox(htmlHelper, name, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static FlexSelect ListBox(this FHtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return ListBoxHelper(htmlHelper, metadata: null, name: name, selectList: selectList, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect ListBoxFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            return ListBoxFor(htmlHelper, expression, selectList, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect ListBoxFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return ListBoxFor(htmlHelper, expression, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static FlexSelect ListBoxFor<TModel, TProperty>(this FHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.HtmlHelper.ViewData);

            return ListBoxHelper(htmlHelper,
                                 metadata,
                                 ExpressionHelper.GetExpressionText(expression),
                                 selectList,
                                 htmlAttributes);
        }

        private static FlexSelect ListBoxHelper(FHtmlHelper htmlHelper, ModelMetadata metadata, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(htmlHelper, metadata, optionLabel: null, name: name, selectList: selectList, allowMultiple: true, htmlAttributes: htmlAttributes);
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

        internal static FlexSelect SelectInternal(this FHtmlHelper htmlHelper, ModelMetadata metadata,
            string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple,
            IDictionary<string, object> htmlAttributes)
        {
            string fullName = htmlHelper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException(FHtmlHelper.MvcResource(FHtmlHelper.MvcResources_Common_NullOrEmpty), "name");
            }

            bool usedViewData = false;

            // If we got a null selectList, try to use ViewData to get the list of items.
            if (selectList == null)
            {
                selectList = htmlHelper.GetSelectData(name);
                usedViewData = true;
            }

            object defaultValue = (allowMultiple) ? htmlHelper.GetModelStateValue(fullName, typeof(string[])) : htmlHelper.GetModelStateValue(fullName, typeof(string));

            // If we haven't already used ViewData to get the entire list of items then we need to
            // use the ViewData-supplied value before using the parameter-supplied value.
            if (defaultValue == null && !String.IsNullOrEmpty(name))
            {
                if (!usedViewData)
                {
                    defaultValue = htmlHelper.HtmlHelper.ViewData.Eval(name);
                }
                else if (metadata != null)
                {
                    defaultValue = metadata.Model;
                }
            }

            if (defaultValue != null)
            {
                selectList = GetSelectListWithDefaultValue(selectList, defaultValue, allowMultiple);
            }



            FlexTagBuilder select = htmlHelper.Render.SelectHelper(new FlexTagBuilder(), optionLabel, fullName, selectList, allowMultiple, htmlAttributes);

            if (select != null)
            {
                // If there are any errors for a named field, we add the css attribute.
                ModelState modelState;
                if (htmlHelper.HtmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
                {
                    if (modelState.Errors.Count > 0)
                    {
                        select.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                    }
                }

                select.MergeAttributes(htmlHelper.HtmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));
            }

            return new FlexSelect(htmlHelper, select);
        }
    }
}
