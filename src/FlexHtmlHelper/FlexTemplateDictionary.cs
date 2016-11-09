using System;
using System.Collections;
using System.Collections.Generic;

namespace FlexHtmlHelper
{
    public class FlexTemplateDictionary : IDictionary<string, IFlexTemplate>
    {
        private readonly Dictionary<string, IFlexTemplate> _innerDictionary = new Dictionary<string, IFlexTemplate>();
        private IFlexTemplate _defaultTemplate;
        
        public FlexTemplateDictionary()
        {
        }       

        public int Count
        {
            get { return _innerDictionary.Count; }
        }

        public IFlexTemplate DefaultTemplate
        {
            get
            {
                if (_defaultTemplate == null)
                {
                    _defaultTemplate = new DefaultTemplate();
                }
                return _defaultTemplate;
            }
            set { _defaultTemplate = value; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<string, IFlexTemplate>)_innerDictionary).IsReadOnly; }
        }

        public ICollection<string> Keys
        {
            get { return _innerDictionary.Keys; }
        }

        public ICollection<IFlexTemplate> Values
        {
            get { return _innerDictionary.Values; }
        }

        public IFlexTemplate this[string key]
        {
            get
            {
                IFlexTemplate Template;
                _innerDictionary.TryGetValue(key, out Template);
                return Template;
            }
            set { _innerDictionary[key] = value; }
        }

        public void Add(KeyValuePair<string, IFlexTemplate> item)
        {
            ((IDictionary<string, IFlexTemplate>)_innerDictionary).Add(item);
        }

        public void Add(string key, IFlexTemplate value)
        {
            _innerDictionary.Add(key, value);
        }

        public void Add(IFlexTemplate value)
        {
            _innerDictionary.Add(value.Name, value);
        }

        public void Clear()
        {
            _innerDictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, IFlexTemplate> item)
        {
            return ((IDictionary<string, IFlexTemplate>)_innerDictionary).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _innerDictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, IFlexTemplate>[] array, int arrayIndex)
        {
            ((IDictionary<string, IFlexTemplate>)_innerDictionary).CopyTo(array, arrayIndex);
        }

        public IFlexTemplate GetTemplate(string name)
        {
            return GetTemplate(name, true /* fallbackToDefault */);
        }

        public virtual IFlexTemplate GetTemplate(string name, bool fallbackToDefault)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            return GetTemplate(name, (fallbackToDefault) ? DefaultTemplate : null);
        }

        private IFlexTemplate GetTemplate(string name, IFlexTemplate fallbackTemplate)
        {       

            IFlexTemplate Template = null;

            if (_innerDictionary.TryGetValue(name, out Template))
            {
                return Template;
            }          

            return Template ?? fallbackTemplate;
        }

        public IEnumerator<KeyValuePair<string, IFlexTemplate>> GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, IFlexTemplate> item)
        {
            return ((IDictionary<string, IFlexTemplate>)_innerDictionary).Remove(item);
        }

        public bool Remove(string key)
        {
            return _innerDictionary.Remove(key);
        }

        public bool TryGetValue(string key, out IFlexTemplate value)
        {
            return _innerDictionary.TryGetValue(key, out value);
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_innerDictionary).GetEnumerator();
        }

        #endregion
    }
}
