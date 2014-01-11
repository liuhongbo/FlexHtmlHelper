﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace FlexHtmlHelper.Html
{
    public static class DisplayTextExtensions
    {
        public static MvcHtmlString DisplayText(this FlexHtmlHelper html, string name)
        {
            return DisplayTextHelper(ModelMetadata.FromStringExpression(name, html.ViewContext.ViewData));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DisplayTextFor<TModel, TResult>(this HtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression)
        {
            return DisplayTextHelper(ModelMetadata.FromLambdaExpression(expression, html.ViewData));
        }

        private static MvcHtmlString DisplayTextHelper(ModelMetadata metadata)
        {
            return MvcHtmlString.Create(metadata.SimpleDisplayText);
        }
    }
}
