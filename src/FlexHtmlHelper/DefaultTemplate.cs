using System;

namespace FlexHtmlHelper
{
    public class DefaultTemplate : IFlexTemplate
    {
        public virtual string Name
        {
            get
            {
                return "Default";
            }
        }        

        public virtual string GetValue(string key, string value)
        {
            return value;
        }

        public virtual string GetChecked(string key, string value, bool isChecked)
        {
            return isChecked ? "checked" : null;
        }

        public virtual string GetSelected(string key, string value, bool isSelected)
        {
            return isSelected ? "selected" : null;
        }
    }
}
