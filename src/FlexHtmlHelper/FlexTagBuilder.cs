using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public class FlexTagBuilder : IHtmlString
    {        
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
            Parent = parentTag;
            Attributes = new SortedDictionary<string, string>(StringComparer.Ordinal);
        }

        #endregion


        public string ToHtmlString()
        {
            return Root.ToString();
        }
        

        public object BuildContext { get; set; }

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
        public FlexTagBuilder Parent { get; set; }

        /// <summary>
        /// return root tag
        /// </summary>
        public FlexTagBuilder Root
        {
            get
            {
                FlexTagBuilder root = this;
                while (root.Parent != null)
                {
                    root = root.Parent;
                }
                return root;
            }
        }

        public IDictionary<string, string> Attributes { get; private set; }

        /// <summary>
        /// return the attributes of the first non abstract tag, attributes on abstract tag is useless
        /// </summary>
        public IDictionary<string, string> TagAttributes
        {
            get
            {
                if (IsAbstractTag())
                {
                    var tag = this.Tag();
                    if (tag != null)
                    {
                        return tag.TagAttributes;
                    }
                }

                return Attributes;
            }
        }

        /// <summary>
        /// return attribte with specified name, return null if not found.  
        /// null could also means the value itself is null
        /// try to use string.Empty for the attribute if no value to void the confusing
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Attribute(string name)
        {
            string value = null; 
            TagAttributes.TryGetValue(name, out value);

            return value;
        }

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

        /// <summary>
        /// the tag's text tag
        /// </summary>
        public FlexTagBuilder TextTag
        {
            get
            {
                return this.Tag().InnerTags.FirstOrDefault(t => (t.IsTextTag()));
            }
        }

        /// <summary>
        /// get the non-abstract parent tag
        /// </summary>
        public FlexTagBuilder ParentTag
        {
            get
            {
                var p = this.Parent;
                if (p == null)
                {
                    return null;
                }
                else
                {
                    if (p.IsAbstractTag())
                    {
                        return p.ParentTag;
                    }
                }
                return p;
            }
        }

        /// <summary>
        /// find the first tag with name equals tagName, could be itself
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
        /// return all the tags with name tagName, including itself
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public IList<FlexTagBuilder> Tags(string tagName)
        {
            List<FlexTagBuilder> list = new List<FlexTagBuilder>();
            if (tagName == TagName) list.Add(this);
            if (InnerTags != null)
            {
                foreach (FlexTagBuilder tag in InnerTags)
                {
                    list.AddRange(tag.Tags(tagName));
                }
            } 

            return list;
        }

        public FlexTagBuilder LastTag(string tagName)
        {
            FlexTagBuilder lastTag = null;
            InternalLastTag(tagName, ref lastTag);
            return lastTag;
        }

        private void InternalLastTag(string tagName, ref FlexTagBuilder lastTag)
        {
            if (tagName == TagName) {
                lastTag = this;
            }           

            if (InnerTags != null)
            {
                foreach (FlexTagBuilder tag in InnerTags)
                {
                    tag.InternalLastTag(tagName, ref lastTag);
                }
            }

            return;
        }

        public FlexTagBuilder TagWithCssClass(string value)
        {
            string @class = Attribute("class");
            if (@class != null)
            {
                if (@class.Split(' ').Contains(value)) return this;
            }

            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithCssClass(value);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        public FlexTagBuilder TagWithCssClass(string tagName,string value)
        {
            if (tagName == this.TagName)
            {
                string @class = Attribute("class");
                if (@class != null)
                {
                    if (@class.Split(' ').Contains(value)) return this;
                }
            }
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithCssClass(value);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        public FlexTagBuilder TagWithAttribute(string name)
        {

            if (TagAttributes.ContainsKey(name)) return this;
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithAttribute(name);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        public FlexTagBuilder TagWithAttribute(string tagName, string name)
        {

            if (tagName == TagName)
            {
                if (TagAttributes.ContainsKey(name)) return this;
            }
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithAttribute(name);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        public FlexTagBuilder TagWithAttributeValue(string name, string value)
        {
            string v;
            if (TagAttributes.TryGetValue(name, out v))
            {
                if (value == v) return this;
            }

            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithAttributeValue(name,value);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        public FlexTagBuilder TagWithAttributeValue(string tagName, string name, string value)
        {
            string v;
            if (tagName == TagName)
            {
                if (TagAttributes.TryGetValue(name, out v))
                {
                    if (value == v) return this;
                }
            }

            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithAttributeValue(tagName,name, value);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        /// <summary>
        /// find the first inner tag with name equals tagName
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public FlexTagBuilder InnerTag(string tagName)
        {
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

        public FlexTagBuilder InnerTagWithCssClass(string value)
        {            
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithCssClass(value);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        public FlexTagBuilder InnerTagWithCssClass(string tagName, string value)
        {            
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithCssClass(value);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        public FlexTagBuilder InnerTagWithAttribute(string name)
        {
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithAttribute(name);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        public FlexTagBuilder InnerTagWithAttribute(string tagName, string name)
        {            
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithAttribute(name);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        public FlexTagBuilder InnerTagWithAttributeValue(string name, string value)
        {
            
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithAttribute(name, value);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        public FlexTagBuilder InnerTagWithAttributeValue(string tagName, string name, string value)
        {            
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in InnerTags)
            {
                var t = tag.TagWithAttributeValue(tagName, name, value);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        public FlexTagBuilder ChildTag(string tagName)
        {
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in this.Tag().InnerTags)
            {
                if (tag.Tag().TagName == tagName)
                {
                    return tag.Tag();
                }
            }
            return null;
        }

        public FlexTagBuilder ChildTagWithClass(string value)
        {
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in this.Tag().InnerTags)
            {               
                var s = tag.Tag().TagAttributes["class"];
                if ((s != null) && (s.Split(' ').Contains(value)))
                {
                    return tag.Tag();
                }               
            }
            return null;
        }    

        public FlexTagBuilder ChildTagWithClass(string tagName, string value)
        {
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in this.Tag().InnerTags)
            {
                if (tag.Tag().TagName == tagName)
                {
                    var s = tag.Tag().TagAttributes["class"];
                    if ((s != null) && (s.Split(' ').Contains(value)))
                    {
                        return tag.Tag();
                    }
                }
            }
            return null;
        }

        public FlexTagBuilder ChildTagWithAttribute(string name)
        {
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in this.Tag().InnerTags)
            {

                if (tag.Tag().TagAttributes.ContainsKey(name))
                {
                    return tag.Tag();
                }                
            }
            return null;
        }

        public FlexTagBuilder ChildTagWithAttribute(string tagName, string name)
        {
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in this.Tag().InnerTags)
            {
                if (tagName == Tag().TagName)
                {
                    if (tag.Tag().TagAttributes.ContainsKey(name))
                    {
                        return tag.Tag();
                    }
                }
            }
            return null;
        }

        public FlexTagBuilder ChildTagWithAttributeValue(string name, string value)
        {
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in this.Tag().InnerTags)
            {
                string v;
                if (tag.Tag().TagAttributes.TryGetValue(name, out v))
                {
                    if (v == value)
                    {
                        return tag.Tag();
                    }
                }
            }
            return null;
        }

        public FlexTagBuilder ChildTagWithAttributeValue(string tagName, string name, string value)
        {
            if (InnerTags == null) return null;
            foreach (FlexTagBuilder tag in this.Tag().InnerTags)
            {
                if (tagName == Tag().TagName)
                {
                    string v;
                    if (tag.Tag().TagAttributes.TryGetValue(name, out v))
                    {
                        if (v == value)
                        {
                            return tag.Tag();
                        }
                    }
                }
            }
            return null;
        }


        //return new tag
        public FlexTagBuilder AddTag(string tagName)
        {
            var tag = new FlexTagBuilder(tagName, this);
            InnerTags.Add(tag);            
            return tag;
        }

        //return this tag
        public FlexTagBuilder AddTag(FlexTagBuilder tag)
        {
            InnerTags.Add(tag);            
            tag.Parent = this;
            return this;
        }

        public FlexTagBuilder AddText(string text)
        {
            var tag = new FlexTagBuilder(this);
            tag.SetText(text);
            InnerTags.Add(tag);
            return this;
        }

        public FlexTagBuilder AddHtmlText(string text)
        {
            var tag = new FlexTagBuilder(this);
            tag.SetHtmlText(text);
            InnerTags.Add(tag);
            return this;
        }

        public FlexTagBuilder AddHtmlText(IHtmlString text)
        {
            var tag = new FlexTagBuilder(this);
            tag.SetHtmlText(text.ToHtmlString());
            InnerTags.Add(tag);
            return this;
        }

        public FlexTagBuilder InsertTag(int index, FlexTagBuilder tag)
        {
            InnerTags.Insert(index, tag);
            tag.Parent = this;
            return tag;
        }

        public FlexTagBuilder InsertTag(int index, string tagName)
        {
            var tag = new FlexTagBuilder(tagName, this);
            InnerTags.Insert(index, tag);
            return tag;
        }


        public bool RemoveTag(FlexTagBuilder tag)
        {

            if ((this == tag) || (this.Tag() == tag))
            {
                if (Parent != null)
                {
                    Parent.InnerTags.Remove(this);
                }
                else
                {
                    return false;
                }

                return true;
            }

            if (InnerTags == null) return false;
            foreach (FlexTagBuilder t in this.Tag().InnerTags)
            {
                if (RemoveTag(tag))
                {
                    return true;
                }
            }
            return false;
        }


        public bool RemoveTag(string tagName)
        {

            if (this.Tag().TagName == tagName)
            {
                if (Parent != null)
                {
                    Parent.InnerTags.Remove(this);
                }
                else
                {
                    return false;
                }

                return true;
            }

            if (InnerTags == null) return false;
            foreach (FlexTagBuilder t in this.Tag().InnerTags)
            {
                if (RemoveTag(tagName))
                {
                    return true;
                }
            }
            return false;
        }


        public bool RemoveInnerTag(FlexTagBuilder tag)
        {
            if (InnerTags == null) return false;
            foreach (FlexTagBuilder t in this.Tag().InnerTags)
            {
                if (RemoveTag(tag))
                {
                    return true;
                }
            }
            return false;
        }

        public bool RemoveInnerTag(string tagName)
        {
            if (InnerTags == null) return false;
            foreach (FlexTagBuilder t in this.Tag().InnerTags)
            {
                if (RemoveTag(tagName))
                {
                    return true;
                }
            }
            return false;
        }

        public bool RemoveChildTag(FlexTagBuilder tag)
        {
            if (InnerTags == null) return false;
            foreach (FlexTagBuilder t in this.Tag().InnerTags)
            {
                if ((t == tag) || (t.Tag() == tag))
                {
                    this.InnerTags.Remove(t);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveChildTag(string name)
        {
            if (InnerTags == null) return false;
            foreach (FlexTagBuilder t in this.Tag().InnerTags)
            {
                if (t.Tag().TagName == name)
                {
                    this.InnerTags.Remove(t);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// replace itself
        /// </summary>
        /// <param name="newTag"></param>
        /// <returns></returns>
        public bool Replace(FlexTagBuilder newTag)
        {
            return Replace(this, newTag);
        }

        /// <summary>
        /// replace
        /// </summary>
        /// <param name="oldTag"></param>
        /// <param name="newTag"></param>
        /// <returns></returns>
        public bool Replace(FlexTagBuilder oldTag, FlexTagBuilder newTag)
        {
            var p = oldTag.Parent;
            if (p != null)
            {
                var index = p.InnerTags.IndexOf(oldTag);
                if (index >= 0)
                {
                    p.InnerTags.Remove(oldTag);
                    p.InnerTags.Insert(index, newTag);
                    return true;
                }

            }
            return false;
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


        private FlexTagBuilder CssStyle(string name, string value, bool checkDuplicate = false)
        {
            string s;

            if (TagAttributes.TryGetValue("style", out s))
            {
                if (checkDuplicate)
                {
                    var styles = s.Split(new char[]{';',' '},  StringSplitOptions.RemoveEmptyEntries);
                    if (styles.Any(style=>{var t = style.Split(new char[]{';',' '},  StringSplitOptions.RemoveEmptyEntries); if ((t.Length>1)&&(t[0] == name)) return true; return false;  }))
                    {
                        return this;
                    }
                }
                TagAttributes["style"] = s + string.Format("{0}:{1};", name, value);
            }
            else
            {
                TagAttributes["style"] = string.Format("{0}:{1};", name, value);
            }
            return this;
        }

        public FlexTagBuilder AddCssStyle(string name, string value, bool checkDuplicate = false)
        {
            FlexTagBuilder tag = this.Tag();

            if (tag != null)
            {
                tag.CssStyle(name, value, checkDuplicate);
            }

            return this;
        }

        /// <summary>
        /// add css to this tag
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private FlexTagBuilder CssClass(string value, bool checkDuplicate = false)
        {            
            string v;

            if (TagAttributes.TryGetValue("class", out v))
            {
                if (checkDuplicate)
                {
                    var values = v.Split(' ');
                    if (values.Contains(value))
                    {
                        return this;
                    }
                }
                TagAttributes["class"] = value + " " + v;
            }
            else
            {
                TagAttributes["class"] = value;
            }
            return this;
        }


        /// <summary>
        /// add css to first non-abstract tag
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public FlexTagBuilder AddCssClass(string value, bool checkDuplicate = false)
        {

            FlexTagBuilder tag = this.Tag();

            if (tag != null)
            {
                tag.CssClass(value, checkDuplicate);
            }
             
            return this;
        }

        public FlexTagBuilder RemoveCssClass(string value)
        {
            string v;

            if (TagAttributes.TryGetValue("class", out v))
            {
                var values = v.Split(' ');
                string newValue = "";
                foreach (var s in values)
                {
                    if (!s.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    {
                        newValue = s + " " + newValue;
                    }
                }
                TagAttributes["class"] = newValue;
            }
            
            return this;
        }

        public FlexTagBuilder RemoveCssClass(Regex r)
        {
            string v;

            if (TagAttributes.TryGetValue("class", out v))
            {
                var values = v.Split(' ');
                string newValue = "";
                foreach (var s in values)
                {
                    if (!r.Match(s).Success)
                    {
                        newValue = s + " " + newValue;
                    }
                }
                TagAttributes["class"] = newValue;
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
            if (!TagAttributes.ContainsKey("id"))
            {
                string sanitizedId = CreateSanitizedId(name, IdAttributeDotReplacement);
                if (!String.IsNullOrEmpty(sanitizedId))
                {
                    TagAttributes["id"] = sanitizedId;
                }
            }
        }

        private void AppendAttributes(StringBuilder sb)
        {
            foreach (var attribute in TagAttributes)
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

        public FlexTagBuilder MergeAttribute(string key, string value)
        {
            MergeAttribute(key, value, replaceExisting: false);
            return this;
        }

        public FlexTagBuilder MergeAttribute(string key, string value, bool replaceExisting)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("key");
            }

            if (replaceExisting || !TagAttributes.ContainsKey(key))
            {
                TagAttributes[key] = value;
            }
            return this;
        }

        public FlexTagBuilder AddAttribute(string key, string value)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("key");
            }
            TagAttributes[key] = value;
            return this;
        }

        public FlexTagBuilder MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
        {
            MergeAttributes(attributes, replaceExisting: false);
            return this;
        }

        public FlexTagBuilder MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting)
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
            return this;
        }

        internal void SetText(string text)
        {
            _text = HttpUtility.HtmlEncode(text);
        }

        internal void SetHtmlText(string text)
        {
            _text = text;
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

            if (InnerTags != null)
            {
                foreach (var t in InnerTags)
                {
                    t.AppendString(sb);
                }
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
