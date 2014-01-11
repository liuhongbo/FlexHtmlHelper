// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace FlexHtmlHelper.Html
{
    public static class NameExtensions
    {
        public static MvcHtmlString Id(this FlexHtmlHelper html, string name)
        {
            return MvcHtmlString.Create(html.AttributeEncode(html.ViewData.TemplateInfo.GetFullHtmlFieldId(name)));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        public static MvcHtmlString IdFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return Id(html, ExpressionHelper.GetExpressionText(expression));
        }

        public static MvcHtmlString IdForModel(this FlexHtmlHelper html)
        {
            return Id(html, String.Empty);
        }

        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "1#", Justification = "This is a shipped API.")]
        public static MvcHtmlString Name(this FlexHtmlHelper html, string name)
        {
            return MvcHtmlString.Create(html.AttributeEncode(html.ViewData.TemplateInfo.GetFullHtmlFieldName(name)));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        public static MvcHtmlString NameFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return Name(html, ExpressionHelper.GetExpressionText(expression));
        }

        public static MvcHtmlString NameForModel(this FlexHtmlHelper html)
        {
            return Name(html, String.Empty);
        }
    }
}
