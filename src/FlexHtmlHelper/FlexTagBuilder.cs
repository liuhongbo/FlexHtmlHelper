using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;


namespace FlexHtmlHelper
{
    public enum FlexTagRenderMode
    {
        Normal,
        StartTag,
        EndTag,
        SelfClosing
    }

    public class FlexTagBuilder
    {
        private string _idAttributeDotReplacement;

        private string _text;

        public FlexTagBuilder()
            : this(null, null)
        {

        }

        public FlexTagBuilder(FlexTagBuilder parentTag)
            : this(null, parentTag)
        {

        }

        public FlexTagBuilder(string tagName)
            : this(tagName, null)
        {

        }

        public FlexTagBuilder(string tagName, FlexTagBuilder parentTag)
        {
            if (!String.IsNullOrEmpty(tagName) || (parentTag == null))
            {
                InnerTags = new List<FlexTagBuilder>();
            }

            TagName = tagName;
            ParentTag = parentTag;
            Attributes = new SortedDictionary<string, string>(StringComparer.Ordinal);
        }


        public IDictionary<string, string> Attributes { get; private set; }
        public IList<FlexTagBuilder> InnerTags { get; private set; }
        public FlexTagBuilder ParentTag { get; private set; }
        public FlexTagBuilder RootTag
        {
            get
            {
                FlexTagBuilder root = this;
                while (root.ParentTag != null)
                {
                    root = root.ParentTag;
                }
                return root;
            }
        }

        private bool IsTextTag()
        {
            return (string.IsNullOrEmpty(TagName) && (ParentTag != null));
        }

        private bool IsFakeRootTag()
        {
            return (string.IsNullOrEmpty(TagName) && (ParentTag == null));
        }


        public FlexTagBuilder AddTag(string tagName)
        {
            var tag = new FlexTagBuilder(tagName, this);
            InnerTags.Add(tag);            
            return tag;
        }

        public FlexTagBuilder AddTag(FlexTagBuilder tag)
        {
            InnerTags.Add(tag);
            return tag;
        }

        public FlexTagBuilder AddText(string text)
        {
            var tag = new FlexTagBuilder(this);
            tag.SetText(text);
            InnerTags.Add(tag);
            return this;
        }

        public string IdAttributeDotReplacement
        {
            get
            {
                if (String.IsNullOrEmpty(_idAttributeDotReplacement))
                {
                    _idAttributeDotReplacement = "_";
                }
                return _idAttributeDotReplacement;
            }
            set { _idAttributeDotReplacement = value; }
        }
        

        public string TagName { get; private set; }

        public void AddCssClass(string value)
        {
            string currentValue;

            if (Attributes.TryGetValue("class", out currentValue))
            {
                Attributes["class"] = value + " " + currentValue;
            }
            else
            {
                Attributes["class"] = value;
            }
        }

        public static string CreateSanitizedId(string originalId)
        {
            return CreateSanitizedId(originalId, "_");
        }

        public static string CreateSanitizedId(string originalId, string invalidCharReplacement)
        {
            if (String.IsNullOrEmpty(originalId))
            {
                return null;
            }

            if (invalidCharReplacement == null)
            {
                throw new ArgumentNullException("invalidCharReplacement");
            }

            char firstChar = originalId[0];
            if (!Html401IdUtil.IsLetter(firstChar))
            {
                // the first character must be a letter
                return null;
            }

            StringBuilder sb = new StringBuilder(originalId.Length);
            sb.Append(firstChar);

            for (int i = 1; i < originalId.Length; i++)
            {
                char thisChar = originalId[i];
                if (Html401IdUtil.IsValidIdCharacter(thisChar))
                {
                    sb.Append(thisChar);
                }
                else
                {
                    sb.Append(invalidCharReplacement);
                }
            }

            return sb.ToString();
        }


        public void GenerateId(string name)
        {
            if (!Attributes.ContainsKey("id"))
            {
                string sanitizedId = CreateSanitizedId(name, IdAttributeDotReplacement);
                if (!String.IsNullOrEmpty(sanitizedId))
                {
                    Attributes["id"] = sanitizedId;
                }
            }
        }

        private void AppendAttributes(StringBuilder sb)
        {
            foreach (var attribute in Attributes)
            {
                string key = attribute.Key;
                if (String.Equals(key, "id", StringComparison.Ordinal /* case-sensitive */) && String.IsNullOrEmpty(attribute.Value))
                {
                    continue; // DevDiv Bugs #227595: don't output empty IDs
                }
                string value = HttpUtility.HtmlAttributeEncode(attribute.Value);
                sb.Append(' ')
                    .Append(key)
                    .Append("=\"")
                    .Append(value)
                    .Append('"');
            }
        }

        public void MergeAttribute(string key, string value)
        {
            MergeAttribute(key, value, replaceExisting: false);
        }

        public void MergeAttribute(string key, string value, bool replaceExisting)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("key");
            }

            if (replaceExisting || !Attributes.ContainsKey(key))
            {
                Attributes[key] = value;
            }
        }

        public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
        {
            MergeAttributes(attributes, replaceExisting: false);
        }

        public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting)
        {
            if (attributes != null)
            {
                foreach (var entry in attributes)
                {
                    string key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
                    string value = Convert.ToString(entry.Value, CultureInfo.InvariantCulture);
                    MergeAttribute(key, value, replaceExisting);
                }
            }
        }

        private void SetText(string text)
        {
            _text = HttpUtility.HtmlEncode(text);
        }

        internal HtmlString ToHtmlString(FlexTagRenderMode renderMode)
        {
            return new HtmlString(ToString(renderMode));
        }

        public override string ToString()
        {
            return ToString(FlexTagRenderMode.Normal);
        }

        private void AppendString(StringBuilder sb)
        {   
            if (IsTextTag())
            {
                sb.Append(_text);
                return;
            }

            if (!IsFakeRootTag())
            {
                sb.Append('<')
                        .Append(TagName);
                AppendAttributes(sb);
                sb.Append('>');               
            }

            foreach (var t in InnerTags)
            {
                t.AppendString(sb);
            }

            if (!IsFakeRootTag())
            {
                sb.Append("</")
                       .Append(TagName)
                       .Append('>');
            }            
        }

        public string ToString(FlexTagRenderMode renderMode)
        {
            StringBuilder sb = new StringBuilder();
            switch (renderMode)
            {
                case FlexTagRenderMode.StartTag:
                    sb.Append('<')
                        .Append(TagName);
                    AppendAttributes(sb);
                    sb.Append('>');
                    break;
                case FlexTagRenderMode.EndTag:
                    sb.Append("</")
                        .Append(TagName)
                        .Append('>');
                    break;
                case FlexTagRenderMode.SelfClosing:
                    sb.Append('<')
                        .Append(TagName);
                    AppendAttributes(sb);
                    sb.Append(" />");
                    break;
                default:
                    AppendString(sb);
                    break;
            }
            return sb.ToString();
        }

        // Valid IDs are defined in http://www.w3.org/TR/html401/types.html#type-id
        private static class Html401IdUtil
        {
            private static bool IsAllowableSpecialCharacter(char c)
            {
                switch (c)
                {
                    case '-':
                    case '_':
                    case ':':
                        // note that we're specifically excluding the '.' character
                        return true;

                    default:
                        return false;
                }
            }

            private static bool IsDigit(char c)
            {
                return ('0' <= c && c <= '9');
            }

            public static bool IsLetter(char c)
            {
                return (('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z'));
            }

            public static bool IsValidIdCharacter(char c)
            {
                return (IsLetter(c) || IsDigit(c) || IsAllowableSpecialCharacter(c));
            }
        }
    }
}
