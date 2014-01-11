using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlexHtmlHelper;
using FlexHtmlHelper.Mvc.Html;

namespace FlexHtmlHelper.Tests
{
    [TestClass]
    public class FlexLabelTest
    {
        [TestMethod]
        public void AddClass()
        {
            FlexTagBuilder tag = new FlexTagBuilder("A");

            FlexLabel label = new FlexLabel(null,tag);

            string str = label.addClass("b").ToHtmlString();
        }   
    }
}
