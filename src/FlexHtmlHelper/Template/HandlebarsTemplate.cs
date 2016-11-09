using System;

namespace FlexHtmlHelper.Template
{
    public class HandlebarsTemplate : DefaultTemplate,  IFlexTemplate
    {
        public override string Name
        {
            get
            {
                return "Handlebars";
            }
        }

        public override string GetValue(string key, string value)
        {
            return string.Format("{{{{{0}}}}}", key);
        }

        public override string GetChecked(string key, string value, bool isChecked)
        {
            return string.Format("{{{{check_{0} '{1}' {2}}}}}", key.ToLowerInvariant(),  value,  key);
        }

        public override string GetSelected(string key, string value, bool isSelected)
        {
            return string.Format("{{{{select_{0} '{1}' {2}}}}}", key.ToLowerInvariant(), value, key);
        }
    }
}
