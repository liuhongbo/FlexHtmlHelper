using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlexHtmlHelper;
using FlexHtmlHelper.Mvc.Html;
using FlexHtmlHelper.Mvc;

namespace FlexHtmlHelper.Tests
{
    [TestClass]
    public class FHtmlHelperTest
    {
        [TestMethod]
        public void MvcResource()
        {
            var str = FHtmlHelper.MvcResource("Common_ValueNotValidForProperty");
            Assert.AreEqual(str, @"The value '{0}' is invalid.");
        }   
    }
}
