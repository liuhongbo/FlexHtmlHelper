using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexButton: FlexElement
    {
        public FlexButton(FHtmlHelper flexHtmlHelper,FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper,tagBuilder)
        {

        }

        public FlexButton(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper,tagName)
        {

        }

        public FlexButton()
        {

        }

        public static FlexButton Empty = new FlexButton();

    }

    public static class FlexButtonExtensions
    {
        #region Button Size

        public static T btn_lg<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonSize(flexButton.TagBuilder, ButtonSizeStyle.Large);
            return flexButton;
        }

        public static T btn_sm<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonSize(flexButton.TagBuilder, ButtonSizeStyle.Small);
            return flexButton;
        }

        public static T btn_xs<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonSize(flexButton.TagBuilder, ButtonSizeStyle.ExtraSmall);
            return flexButton;
        }

        #endregion

        #region Button Style

        public static T btn_default<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonStyle(flexButton.TagBuilder, ButtonOptionStyle.Default);
            return flexButton;
        }

        public static T btn_primary<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonStyle(flexButton.TagBuilder, ButtonOptionStyle.Primary);
            return flexButton;
        }

        public static T btn_success<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonStyle(flexButton.TagBuilder, ButtonOptionStyle.Success);
            return flexButton;
        }

        public static T btn_info<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonStyle(flexButton.TagBuilder, ButtonOptionStyle.Info);
            return flexButton;
        }

        public static T btn_warning<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonStyle(flexButton.TagBuilder, ButtonOptionStyle.Warning);
            return flexButton;
        }

        public static T btn_danger<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonStyle(flexButton.TagBuilder, ButtonOptionStyle.Danger);
            return flexButton;
        }

        public static T btn_link<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonStyle(flexButton.TagBuilder, ButtonOptionStyle.Link);
            return flexButton;
        }

        #endregion

        public static T btn_block<T>(this T flexButton) where T : FlexButton
        {
            flexButton.Render.ButtonBlock(flexButton.TagBuilder);
            return flexButton;
        }
    }
}
