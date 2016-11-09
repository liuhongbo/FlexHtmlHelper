using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexHtmlHelper.Template;

namespace FlexHtmlHelper
{
    public static class FlexTemplates
    {
        private static readonly FlexTemplateDictionary _Templates = CreateDefaultTemplateDictionary();

        public static FlexTemplateDictionary Templates
        {
            get { return _Templates; }
        }        

        private static FlexTemplateDictionary CreateDefaultTemplateDictionary()
        {

            FlexTemplateDictionary Templates = new FlexTemplateDictionary();
            
            Templates.Add(new DefaultTemplate());            
            Templates.Add(new HandlebarsTemplate());

            return Templates;
        }
    }


}