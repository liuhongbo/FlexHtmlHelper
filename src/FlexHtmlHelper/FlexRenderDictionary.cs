using System;
using System.Collections;
using System.Collections.Generic;

namespace FlexHtmlHelper
{
    public class FlexRenderDictionary : IDictionary<string, IFlexRender>
    {
        private readonly Dictionary<string, IFlexRender> _innerDictionary = new Dictionary<string, IFlexRender>();
        private IFlexRender _defaultRender;
        
        public FlexRenderDictionary()
        {
        }       

        public int Count
        {
            get { return _innerDictionary.Count; }
        }

        public IFlexRender DefaultRender
        {
            get
            {
                if (_defaultRender == null)
                {
                    _defaultRender = new DefaultRender();
                }
                return _defaultRender;
            }
            set { _defaultRender = value; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<string, IFlexRender>)_innerDictionary).IsReadOnly; }
        }

        public ICollection<string> Keys
        {
            get { return _innerDictionary.Keys; }
        }

        public ICollection<IFlexRender> Values
        {
            get { return _innerDictionary.Values; }
        }

        public IFlexRender this[string key]
        {
            get
            {
                IFlexRender render;
                _innerDictionary.TryGetValue(key, out render);
                return render;
            }
            set { _innerDictionary[key] = value; }
        }

        public void Add(KeyValuePair<string, IFlexRender> item)
        {
            ((IDictionary<string, IFlexRender>)_innerDictionary).Add(item);
        }

        public void Add(string key, IFlexRender value)
        {
            _innerDictionary.Add(key, value);
        }

        public void Add(IFlexRender value)
        {
            _innerDictionary.Add(value.Name, value);
        }

        public void Clear()
        {
            _innerDictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, IFlexRender> item)
        {
            return ((IDictionary<string, IFlexRender>)_innerDictionary).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _innerDictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, IFlexRender>[] array, int arrayIndex)
        {
            ((IDictionary<string, IFlexRender>)_innerDictionary).CopyTo(array, arrayIndex);
        }

        public IFlexRender GetRender(string name)
        {
            return GetRender(name, true /* fallbackToDefault */);
        }

        public virtual IFlexRender GetRender(string name, bool fallbackToDefault)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            return GetRender(name, (fallbackToDefault) ? DefaultRender : null);
        }

        private IFlexRender GetRender(string name, IFlexRender fallbackRender)
        {       

            IFlexRender render = null;

            if (_innerDictionary.TryGetValue(name, out render))
            {
                return render;
            }          

            return render ?? fallbackRender;
        }

        public IEnumerator<KeyValuePair<string, IFlexRender>> GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, IFlexRender> item)
        {
            return ((IDictionary<string, IFlexRender>)_innerDictionary).Remove(item);
        }

        public bool Remove(string key)
        {
            return _innerDictionary.Remove(key);
        }

        public bool TryGetValue(string key, out IFlexRender value)
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
