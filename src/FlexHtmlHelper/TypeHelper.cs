// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. see http://aspnetwebstack.codeplex.com/SourceControl/latest#License.txt for license information.
// ported from aspnetwebstack
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Routing;

namespace FlexHtmlHelper
{
    internal static class TypeHelper
    {
        /// <summary>
        /// Given an object of anonymous type, add each property as a key and associated with its value to a dictionary.
        ///
        /// This helper will cache accessors and types, and is intended when the anonymous object is accessed multiple
        /// times throughout the lifetime of the web application.
        /// </summary>
        public static RouteValueDictionary ObjectToDictionary(object value)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();

            if (value != null)
            {
                foreach (PropertyHelper helper in PropertyHelper.GetProperties(value))
                {
                    dictionary.Add(helper.Name, helper.GetValue(value));
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Given an object of anonymous type, add each property as a key and associated with its value to a dictionary.
        ///
        /// This helper will not cache accessors and types, and is intended when the anonymous object is accessed once
        /// or very few times throughout the lifetime of the web application.
        /// </summary>
        public static RouteValueDictionary ObjectToDictionaryUncached(object value)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();

            if (value != null)
            {
                foreach (PropertyHelper helper in PropertyHelper.GetProperties(value))
                {
                    dictionary.Add(helper.Name, helper.GetValue(value));
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Given an object of anonymous type, add each property as a key and associated with its value to the given dictionary.
        /// </summary>
        public static void AddAnonymousObjectToDictionary(IDictionary<string, object> dictionary, object value)
        {
            var values = ObjectToDictionary(value);
            foreach (var item in values)
            {
                dictionary.Add(item);
            }
        }

        /// <remarks>This code is copied from http://www.liensberger.it/web/blog/?p=191 </remarks>
        public static bool IsAnonymousType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            // TODO: The only way to detect anonymous types right now.
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                   && type.IsGenericType && type.Name.Contains("AnonymousType")
                   && (type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) || type.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase))
                   && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }
    }
}
