using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        private IDictionary<string, string> _attributes;

        private string _idAttributeDotReplacement;

        private string _text;

        static private string[] _voidElements = {"area", "base", "br", "col", "command", "embed", "hr", "img", "input","keygen", "link", "meta", "param", "source", "track", "wbr" };

        #region Constructor

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
            _attributes = new SortedDictionary<string, string>(StringComparer.Ordinal);
        }

        #endregion


        /// <summary>
        /// http://www.w3.org/TR/html-markup/syntax.html
        /// </summary>
        /// <returns></returns>
        private bool IsVoidElement()
        {
            return string.IsNullOrEmpty(TagName) ? false : _voidElements.Contains(TagName.ToLowerInvariant());
        }


        /// <summary>
        /// text tag is an abstract tag with text
        /// </summary>
        /// <returns></returns>
        private bool IsTextTag()
        {
            return (string.IsNullOrEmpty(TagName) && (!string.IsNullOrEmpty(_text)));
        }

        private bool IsAbstractTag()
        {
            return (string.IsNullOrEmpty(TagName));
        }        

        /// <summary>
        /// return the attributes of the first non abstract tag, attributes on abstract tag is useless
        /// </summary>
        public IDictionary<string, string> Attributes
        {
            get
            {
                if (IsAbstractTag())
                {
                    var tag = this.Tag();
                    if (tag != null)
                    {
                        return tag.Attributes;
                    }
                }               
                
                return _attributes;
            }
            private set
            {
                _attributes = value;
            }
        }

        /// <summary>
        /// tag name
        /// </summary>
        public string TagName { get; set; }


        /// <summary>
        /// children tags
        /// </summary>
        public IList<FlexTagBuilder> InnerTags { get; private set; }


        /// <summary>
        /// return parent tag
        /// </summary>
        public FlexTagBuilder ParentTag { get; private set; }

        /// <summary>
        /// return root tag
        /// </summary>
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

        /// <summary>
        /// find the first inner tag with name tagName
        /// </summary>
        /// <param name="tagName">tag's name</param>
        /// <returns></returns>
        public FlexTagBuilder Tag(string tagName)
        {   
            if (tagName == TagName) return this;
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.Tag(tagName);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        /// <summary>
        /// find the first tag with name tagName
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public FlexTagBuilder this[string tagName]
        {
            get
            {
                return Tag(tagName);
            }
        }

        /// <summary>
        /// find the first non-abstract tag
        /// </summary>
        /// <returns></returns>
        public FlexTagBuilder Tag()
        {
            if (!IsAbstractTag()) 
            {
                return this;
            }
            else
            {
                if (InnerTags == null) return null;
                if (InnerTags.Count == 0) return null;
                foreach (FlexTagBuilder tag in InnerTags)
                {
                    var t = tag.Tag();
                    if (t != null)
                    {
                        return t;
                    }
                }
                return null;
            }
        }

        public FlexTagBuilder TextTag
        {
            get
            {
                return InnerTags.FirstOrDefault(t => (t.IsTextTag()));
            }
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
            tag.ParentTag = this;
            return tag;
        }

        public FlexTagBuilder InsertTag(int index, FlexTagBuilder tag)
        {
            InnerTags.Insert(index, tag);
            tag.ParentTag = this;
            return tag;
        }

        public FlexTagBuilder InsertTag(int index, string tagName)
        {
            var tag = new FlexTagBuilder(tagName, this);
            InnerTags.Insert(index, tag);
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


        /// <summary>
        /// add css to this tag
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private FlexTagBuilder CssClass(string value)
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
            return this;
        }


        /// <summary>
        /// add css to first non-abstract tag
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public FlexTagBuilder AddCssClass(string value)
        {

            FlexTagBuilder tag = this.Tag();

            if (tag != null)
            {
                tag.CssClass(value);
            }
             
            return this;
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

        internal void SetText(string text)
        {
            _text = HttpUtility.HtmlEncode(text);
        }

        internal HtmlString ToHtmlString(FlexTagRenderMode renderMode)
        {
            return new HtmlString(ToString(renderMode));
        }

        public override string ToString()
        {
            return IsVoidElement() ? ToString(FlexTagRenderMode.SelfClosing) : ToString(FlexTagRenderMode.Normal);
        }

        private void AppendString(StringBuilder sb)
        {   
            if (IsTextTag())
            {
                sb.Append(_text);
                return;
            }

            if (IsVoidElement())
            {
                sb.Append('<').Append(TagName);
                AppendAttributes(sb);
                sb.Append(" />");
                return;
            }

            if (!IsAbstractTag())
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

            if (!IsAbstractTag())
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
