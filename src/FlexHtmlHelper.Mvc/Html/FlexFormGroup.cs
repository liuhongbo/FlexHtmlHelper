using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHtmlHelper.Mvc.Html
{
    public class FlexFormGroup: FlexElement
    {
        public FlexFormGroup(FHtmlHelper flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {

        }

        public FlexFormGroup(FHtmlHelper flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        public FlexFormGroup()
        {

        }

        public FlexTagBuilder InputTag
        {
            get
            {
                var tag = TagBuilder.Tag("input");
                if (tag == null)
                {
                    tag = TagBuilder.Tag("select");
                }
                return tag;
            }
        }        

        public static FlexFormGroup Empty = new FlexFormGroup();

    }

    public static class FlexFormGroupExtensions
    {

        public static T LabelText<T>(this T formGroup, string text) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupLabelText(formGroup.TagBuilder, text);
            return formGroup;
        }

        public static T HelpText<T>(this T formGroup, string text) where T: FlexFormGroup
        {
            formGroup.Render.FormGroupHelpText(formGroup.TagBuilder, text);
            return formGroup;
        }        

        public static T Placeholder<T>(this T formGroup, string text) where T: FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.Placeholder(input, text);
            }
            return formGroup;
        }

        public static T Disabled<T>(this T formGroup) where T : FlexFormGroup
        {
             var input = formGroup.InputTag;
             if (input != null)
             {
                 formGroup.Render.Disabled(input);
             }
            return formGroup;
        }

        public static T Focus<T>(this T formGroup) where T : FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.Focus(input);
            }
            return formGroup;
        }

        public static T HasWarning<T>(this T formGroup) where T: FlexFormGroup
        {
            formGroup.Render.FormGroupValidationState(formGroup.TagBuilder,ValidationState.Warning);
            return formGroup;
        }

        public static T HasError<T>(this T formGroup) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupValidationState(formGroup.TagBuilder, ValidationState.Error);
            return formGroup;
        }

        public static T HasSuccess<T>(this T formGroup) where T : FlexFormGroup
        {
            formGroup.Render.FormGroupValidationState(formGroup.TagBuilder, ValidationState.Succuss);
            return formGroup;
        }

        public static T Large<T>(this T formGroup) where T : FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.InputHeight(input, InputHeightStyle.Large);
            }
            return formGroup;
        }

        public static T Small<T>(this T formGroup) where T : FlexFormGroup
        {
            var input = formGroup.InputTag;
            if (input != null)
            {
                formGroup.Render.InputHeight(input, InputHeightStyle.Small);
            }
            return formGroup;
        }
    }

    public class FlexFormGroup<TModel> : FlexFormGroup
    {
        public FlexFormGroup(FHtmlHelper<TModel> flexHtmlHelper, FlexTagBuilder tagBuilder)
            : base(flexHtmlHelper, tagBuilder)
        {

        }

        public FlexFormGroup(FHtmlHelper<TModel> flexHtmlHelper, string tagName)
            : base(flexHtmlHelper, tagName)
        {

        }

        public FlexFormGroup()
        {

        }

        public static new FlexFormGroup<TModel> Empty = new FlexFormGroup<TModel>();
    }
}
