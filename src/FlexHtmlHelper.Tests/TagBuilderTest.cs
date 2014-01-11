using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlexHtmlHelper;

namespace FlexHtmlHelper.Tests
{
    [TestClass]
    public class TagBuilderTest
    {
        [TestMethod]
        public void OneNodeLevelWithoutText()
        {
            FlexTagBuilder t = new FlexTagBuilder("A");            
            string str = t.ToString();
            Assert.IsTrue(str == "<A></A>");
        }

        [TestMethod]
        public void OneNodeLevelWithFakeRoot()
        {
            FlexTagBuilder r = new FlexTagBuilder();
            r.AddTag("A");
            string str = r.ToString();
            Assert.IsTrue(str == "<A></A>");
        }

        [TestMethod]
        public void OneNodeLevelWithText()
        {
            FlexTagBuilder t = new FlexTagBuilder("A");
            t.AddText("B");
            string str = t.ToString();
            Assert.IsTrue(str == "<A>B</A>");
        }

        [TestMethod]
        public void TwoNodeLevelWithoutText()
        {
            FlexTagBuilder t = new FlexTagBuilder("A");
            t.AddTag("B");
            string str = t.ToString();
            Assert.IsTrue(str == "<A><B></B></A>");
        }

        [TestMethod]
        public void TwoNodeLevelWithTextInSecondLevel()
        {
            FlexTagBuilder t = new FlexTagBuilder("A");
            t.AddTag("B").AddText("C");
            string str = t.ToString();
            Assert.IsTrue(str == "<A><B>C</B></A>");
        }

        [TestMethod]
        public void TwoNodeLevelWithTextInBothLevelFront()
        {
            FlexTagBuilder t = new FlexTagBuilder("A");

            t.AddText("D").AddTag("B").AddText("C");
            string str = t.ToString();
            Assert.IsTrue(str == "<A>D<B>C</B></A>");
        }

        [TestMethod]
        public void TwoNodeLevelWithTextInBothLevelRear()
        {
            FlexTagBuilder t = new FlexTagBuilder("A");

            t.AddTag("B").AddText("C");
            t.AddText("D");
            string str = t.ToString();
            Assert.IsTrue(str == "<A><B>C</B>D</A>");
        }

        [TestMethod]
        public void TwoNodeLevelWithTextInBothLevelFrontRear()
        {
            FlexTagBuilder t = new FlexTagBuilder("A");

            t.AddText("E").AddTag("B").AddText("C");
            t.AddText("D");
            string str = t.ToString();
            Assert.IsTrue(str == "<A>E<B>C</B>D</A>");
        }

    }
}
