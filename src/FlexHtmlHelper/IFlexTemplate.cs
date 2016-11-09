using System;

namespace FlexHtmlHelper
{
    public interface IFlexTemplate
    {
        string Name { get; }
        string GetValue(string key, string value);
        string GetChecked(string key, string value, bool isChecked);
        string GetSelected(string key, string value, bool isSelected);    
    }
}
