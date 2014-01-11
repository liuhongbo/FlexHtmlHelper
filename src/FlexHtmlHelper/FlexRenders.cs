using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexHtmlHelper.Render;

namespace FlexHtmlHelper
{
    public static class FlexRenders
    {
        private static readonly FlexRenderDictionary _renders = CreateDefaultRenderDictionary();

        public static FlexRenderDictionary Renders
        {
            get { return _renders; }
        }        

        private static FlexRenderDictionary CreateDefaultRenderDictionary()
        {

            FlexRenderDictionary renders = new FlexRenderDictionary();
            
            renders.Add(new DefaultRender());            
            renders.Add(new Bootstrap3Render());

            return renders;
        }
    }


}