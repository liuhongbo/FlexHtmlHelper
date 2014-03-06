using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace FlexHtmlHelper.Render
{
    public class Bootstrap3Render :DefaultRender, IFlexRender
    {
        public override string Name
        {
            get
            {
                return "Bootstrap3";
            }
        }

        #region Helper

        public override FlexTagBuilder LabelHelper(FlexTagBuilder tagBuilder, string @for, string text, IDictionary<string, object> htmlAttributes = null)
        {
            FlexTagBuilder tag = new FlexTagBuilder("label");
            tag.TagAttributes.Add("for", @for);
            tag.AddText(text);
            tag.MergeAttributes(htmlAttributes, replaceExisting: true);

            tagBuilder.AddTag(tag);
            return tag;
        }

        public override FlexTagBuilder FormHelper(FlexTagBuilder tagBuilder, string formAction, string formMethod, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("form");
            tag.MergeAttributes(htmlAttributes);
            // action is implicitly generated, so htmlAttributes take precedence.
            tag.MergeAttribute("action", formAction);
            // method is an explicit parameter, so it takes precedence over the htmlAttributes.
            tag.MergeAttribute("method", formMethod, true);

            tag.MergeAttribute("role", "form");

            tagBuilder.AddTag(tag);

            return tag;
        }

        public override FlexTagBuilder StaticHelper(FlexTagBuilder tagBuilder, string name, string value, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("p");
            tag.MergeAttributes(htmlAttributes);
            tag.AddText(value);
            tagBuilder.AddTag(tag);

            return tag;
        }

        private string GetInputType(FlexTagBuilder inputTag)
        {
            string inputType = null;
            if (!inputTag.TagAttributes.Keys.Contains("type"))
            {

                if (inputTag.Tag().TagName.Equals("select", StringComparison.InvariantCultureIgnoreCase))
                {
                    inputType = "select";
                }
                else if (inputTag.Tag().TagName.Equals("textarea", StringComparison.InvariantCultureIgnoreCase))
                {
                    inputType = "textarea";
                }
                else if (inputTag.Tag().TagName.Equals("p", StringComparison.InvariantCultureIgnoreCase))
                {
                    inputType = "static";
                }               
            }
            else
            {
                inputType = inputTag.TagAttributes["type"];
            }

            return inputType;
        }

        public override FlexTagBuilder FormGroupHelper(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder labelTag, FlexTagBuilder inputTag, FlexTagBuilder validationMessageTag)
        {
            FlexTagBuilder tag = new FlexTagBuilder("div");

            string inputType = GetInputType(inputTag);

            switch (formContext.LayoutStyle)
            {
                case FormLayoutStyle.Default:
                    labelTag.AddCssClass("control-label");
                    switch (inputType)
                    {
                        case "text":
                        case "email":
                        case "password":
                        case "select":
                            goto default;                            
                        case "radio":
                            tag.AddCssClass("radio");
                            tag.AddTag(labelTag);
                            labelTag.InsertTag(0, inputTag);
                            break;
                        case "checkbox":
                            tag.AddCssClass("checkbox");
                            tag.AddTag(labelTag);
                            labelTag.InsertTag(0, inputTag);
                            break;
                        case "hidden":
                            tag.AddTag(inputTag);
                            break;
                        case "file":
                            tag.AddCssClass("form-group");
                            tag.AddTag(labelTag);
                            tag.AddTag(inputTag);
                            tag.AddTag(validationMessageTag);
                            break;
                        case "static":
                            tag.AddCssClass("form-group");
                            tag.AddTag(labelTag);
                            tag.AddTag(inputTag).AddCssClass("form-control-static");
                            break;
                        default:
                            tag.AddCssClass("form-group");
                            tag.AddTag(labelTag);
                            tag.AddTag(inputTag).AddCssClass("form-control");
                            tag.AddTag(validationMessageTag);
                            break;
                    }
                    break;
                case FormLayoutStyle.Horizontal:
                    switch (inputType)
                    {
                        case "text":
                        case "email":
                        case "password":
                        case "select":
                            goto default;
                        case "radio":
                             tag.AddCssClass("form-group");
                            FlexTagBuilder radioColTag = new FlexTagBuilder("div");
                            foreach (var col in formContext.LabelColumns)
                            {
                                GridColumnOffset(radioColTag, col.Key, col.Value);
                            }
                            foreach (var col in formContext.InputColumns)
                            {
                                GridColumns(radioColTag, col.Key, col.Value);
                            }

                            tag.AddTag(radioColTag);
                            FlexTagBuilder radioTag = new FlexTagBuilder("div");
                            radioTag.AddCssClass("radio");
                            radioColTag.AddTag(radioTag);

                            radioTag.AddTag(labelTag);
                            labelTag.InsertTag(0,inputTag);
                            break;
                        case "checkbox":
                            tag.AddCssClass("form-group");
                            FlexTagBuilder colTag = new FlexTagBuilder("div");
                            foreach (var col in formContext.LabelColumns)
                            {
                                GridColumnOffset(colTag, col.Key, col.Value);
                            }
                            foreach (var col in formContext.InputColumns)
                            {
                                GridColumns(colTag, col.Key, col.Value);
                            }

                            tag.AddTag(colTag);
                            FlexTagBuilder checkBoxTag = new FlexTagBuilder("div");
                            checkBoxTag.AddCssClass("checkbox");
                            colTag.AddTag(checkBoxTag);

                            checkBoxTag.AddTag(labelTag);
                            labelTag.InsertTag(0,inputTag);
                            break;
                        case "hidden":
                            tag.AddTag(inputTag);
                            break;
                        case "file":
                            tag.AddCssClass("form-group");
                            foreach (var col in formContext.LabelColumns)
                            {
                                GridColumns(labelTag, col.Key, col.Value);
                            }
                            labelTag.AddCssClass("control-label");
                            tag.AddTag(labelTag);

                            FlexTagBuilder fileDivTag = new FlexTagBuilder("div");
                            foreach (var col in formContext.InputColumns)
                            {
                                GridColumns(fileDivTag, col.Key, col.Value);
                            }

                            fileDivTag.AddTag(inputTag).AddCssClass("form-control");

                            tag.AddTag(fileDivTag);
                            tag.AddTag(validationMessageTag);
                            break;
                        case "static":
                            tag.AddCssClass("form-group");
                            foreach (var col in formContext.LabelColumns)
                            {
                                GridColumns(labelTag, col.Key, col.Value);
                            }
                            labelTag.AddCssClass("control-label");
                            tag.AddTag(labelTag);
                            
                            FlexTagBuilder staticDivTag = new FlexTagBuilder("div");
                            foreach (var col in formContext.InputColumns)
                            {
                                GridColumns(staticDivTag, col.Key, col.Value);
                            }
                            staticDivTag.AddTag(inputTag).AddCssClass("form-control-static");
                            tag.AddTag(staticDivTag);
                            break;
                        default:                            
                            tag.AddCssClass("form-group");
                            foreach (var col in formContext.LabelColumns)
                            {
                                GridColumns(labelTag, col.Key, col.Value);
                            }
                            labelTag.AddCssClass("control-label");
                            tag.AddTag(labelTag);
                            
                            FlexTagBuilder inputDivTag = new FlexTagBuilder("div");
                            foreach (var col in formContext.InputColumns)
                            {
                                GridColumns(inputDivTag, col.Key, col.Value);
                            }
                            inputDivTag.AddTag(inputTag).AddCssClass("form-control");

                            tag.AddTag(inputDivTag);
                            tag.AddTag(validationMessageTag);
                            break;
                    }
                    break;
                case FormLayoutStyle.Inline:
                    switch (inputType)
                    {
                        case "text":
                        case "email":
                        case "password":
                        case "select":
                            goto default;
                        case "radio":
                            tag.AddCssClass("radio");
                            tag.AddTag(labelTag);
                            labelTag.InsertTag(0, inputTag);
                            break;
                        case "checkbox":
                            tag.AddCssClass("checkbox");
                            tag.AddTag(labelTag);
                            labelTag.InsertTag(0, inputTag);
                            break;
                        case "hidden":
                            tag.AddTag(inputTag);
                            break;
                        case "file":
                            tag.AddCssClass("form-group");
                            labelTag.AddCssClass("sr-only");
                            tag.AddTag(labelTag);
                            tag.AddTag(inputTag);
                            tag.AddTag(validationMessageTag);
                            break;
                        case "static":
                            tag.AddCssClass("form-group");
                            labelTag.AddCssClass("sr-only");
                            tag.AddTag(labelTag);
                            tag.AddTag(inputTag).AddCssClass("form-control-static");
                            break;
                        default:
                            tag.AddCssClass("form-group");
                            labelTag.AddCssClass("sr-only");
                            tag.AddTag(labelTag);
                            tag.AddTag(inputTag).AddCssClass("form-control");
                            tag.AddTag(validationMessageTag);
                            break;
                    }
                    break;                   
            }
            
            tagBuilder.AddTag(tag);

            return tag;
        }

        #endregion

        #region Grid System

        public override FlexTagBuilder GridRow(FlexTagBuilder tagBuilder)
        {
            FlexTagBuilder tag = new FlexTagBuilder("div");
            tag.AddCssClass("row");
            tag.AddTag(tagBuilder.Root);

            return tagBuilder;
        }

        public override FlexTagBuilder GridColumns(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            string cssClass = "";
            switch (style)
            {
                case GridStyle.ExtraSmall:
                    cssClass = "col-xs-";
                    break;
                case GridStyle.Small:
                    cssClass = "col-sm-";
                    break;
                case GridStyle.Medium:
                    cssClass = "col-md-";
                    break;
                case GridStyle.Large:
                    cssClass = "col-lg-";
                    break;
            }
            tagBuilder.AddCssClass(cssClass + columns.ToString());
            return tagBuilder;
        }

        public override FlexTagBuilder GridColumnOffset(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            string cssClass = "";
            switch (style)
            {
                case GridStyle.ExtraSmall:
                    cssClass = "col-xs-offset-";
                    break;
                case GridStyle.Small:
                    cssClass = "col-sm-offset-";
                    break;
                case GridStyle.Medium:
                    cssClass = "col-md-offset-";
                    break;
                case GridStyle.Large:
                    cssClass = "col-lg-offset-";
                    break;
            }
            tagBuilder.AddCssClass(cssClass + columns.ToString());
            return tagBuilder;
        }

        public override FlexTagBuilder GridColumnPush(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            string cssClass = "";
            switch (style)
            {
                case GridStyle.ExtraSmall:
                    cssClass = "col-xs-push-";
                    break;
                case GridStyle.Small:
                    cssClass = "col-sm-push-";
                    break;
                case GridStyle.Medium:
                    cssClass = "col-md-push-";
                    break;
                case GridStyle.Large:
                    cssClass = "col-lg-push-";
                    break;
            }
            tagBuilder.AddCssClass(cssClass + columns.ToString());
            return tagBuilder;
        }

        public override FlexTagBuilder GridColumnPull(FlexTagBuilder tagBuilder, GridStyle style, int columns)
        {
            string cssClass = "";
            switch (style)
            {
                case GridStyle.ExtraSmall:
                    cssClass = "col-xs-pull-";
                    break;
                case GridStyle.Small:
                    cssClass = "col-sm-pull-";
                    break;
                case GridStyle.Medium:
                    cssClass = "col-md-pull-";
                    break;
                case GridStyle.Large:
                    cssClass = "col-lg-pull-";
                    break;
            }
            tagBuilder.AddCssClass(cssClass + columns.ToString());
            return tagBuilder;
        }

        public override FlexTagBuilder GridColumnVisible(FlexTagBuilder tagBuilder, GridStyle style, bool visible)
        {
            string s = string.Empty;

            switch (style)
            {
                case GridStyle.ExtraSmall:
                    s = "xs";
                    break;
                case GridStyle.Small:
                    s = "sm";
                    break;
                case GridStyle.Medium:
                    s = "md";
                    break;
                case GridStyle.Large:
                    s = "lg";
                    break;
                case GridStyle.Print:
                    s = "print";
                    break;

            }

            string css = string.Format("{0}-{1}", visible ? "visible" : "hidden", s);

            tagBuilder.AddCssClass(css);

            return tagBuilder;
        }

        #endregion


        #region Form

        public override FlexTagBuilder FormLayout(FlexTagBuilder tagBuilder, FormLayoutStyle layout)
        {
            switch (layout)
            {
                case FormLayoutStyle.Horizontal:
                    tagBuilder.AddCssClass("form-horizontal");
                    break;
                case FormLayoutStyle.Inline:
                    tagBuilder.AddCssClass("form-inline");
                    break;
            }
            return tagBuilder;
        }        

        public override FlexTagBuilder FormGroupHelpText(FlexTagBuilder tagBuilder, string text)
        {
            tagBuilder.AddTag("span").AddCssClass("help-block").AddText(text);
            return tagBuilder;
        }


        public override FlexTagBuilder FormGroupLabelText(FlexTagBuilder tagBuilder, string text)
        {
            var tag = tagBuilder.LastTag("label");
            if (tag != null)
            {
                tag.TextTag.SetText(text);
            }
            return tagBuilder;
        }

        public override FlexTagBuilder FormGroupValidationState(FlexTagBuilder tagBuilder, ValidationState state)
        {
            switch (state)
            {
                case ValidationState.Warning:
                    tagBuilder.Tag().AddCssClass("has-warning");
                    break;
                case ValidationState.Error:
                    tagBuilder.Tag().AddCssClass("has-error");
                    break;
                case ValidationState.Succuss:
                    tagBuilder.Tag().AddCssClass("has-success");
                    break;
            }

            tagBuilder.Tag().AddCssClass("");
            return tagBuilder;
        }        

        public override FlexTagBuilder FormGroupAddInput(FlexFormContext formContext, FlexTagBuilder formGroupTag, FlexTagBuilder labelTag, FlexTagBuilder inputTag)
        {
            string inputType = GetInputType(inputTag);

            switch (inputType)
            {
                case "checkbox":
                    labelTag.Tag().AddCssClass("checkbox-inline");
                    labelTag.Tag().InsertTag(0, inputTag);
                    break;
                case "radio":
                    labelTag.Tag().AddCssClass("radio-inline");
                    labelTag.Tag().InsertTag(0, inputTag);
                    break;
                default:
                    return formGroupTag;                    
            }

            FlexTagBuilder div = null;

            switch (formContext.LayoutStyle)
            {
                case FormLayoutStyle.Default:
                    div = formGroupTag.TagWithCssClass("div", "checkbox");
                    if (div != null)
                    {
                        div.RemoveCssClass("checkbox");
                        div.ChildTag("label").AddCssClass("checkbox-inline");
                    }
                    else
                    {
                        div = formGroupTag.TagWithCssClass("div", "radio");
                        if (div != null)
                        {
                            div.RemoveCssClass("radio");
                            div.ChildTag("label").AddCssClass("radio-inline");
                        }
                    }
                    formGroupTag.AddCssClass("form-group",true);
                    formGroupTag.AddTag(labelTag);
                    break;
                case FormLayoutStyle.Horizontal:
                    FlexTagBuilder colTag =  formGroupTag.Tag().ChildTag("div");
                    if (colTag!= null)
                    {
                        div = colTag.Tag().ChildTagWithClass("div", "checkbox");
                        if (div != null)
                        {
                            var label = div.Tag().ChildTag("label");
                            if (label != null)
                            {
                                label.Tag().AddCssClass("checkbox-inline");
                                colTag.RemoveChildTag(div);
                                colTag.InsertTag(0, label);                                
                            }
                        }
                        else 
                        {
                            div = colTag.Tag().ChildTagWithClass("div", "radio");
                            if (div != null)
                            {
                                var label = div.Tag().ChildTag("label");
                                if (label != null)
                                {
                                    label.Tag().AddCssClass("radio-inline");
                                    colTag.RemoveChildTag(div);
                                    colTag.InsertTag(0, label);
                                }
                            }
                        }
                        colTag.AddTag(labelTag);
                    }
                    break;
                case FormLayoutStyle.Inline:                    
                    formGroupTag.AddTag(labelTag);
                    break;
            }

            return formGroupTag;
        }

        public override FlexTagBuilder FormGroupInputGridColumns(FlexFormContext formContext, FlexTagBuilder formGroupTag, GridStyle style, int columns)
        {
            FlexTagBuilder div = null;
            FlexTagBuilder input = null;
            int index = 0;
            string inputType = string.Empty;

            string cssClass = "";
            switch (style)
            {
                case GridStyle.ExtraSmall:
                    cssClass = "col-xs-";
                    break;
                case GridStyle.Small:
                    cssClass = "col-sm-";
                    break;
                case GridStyle.Medium:
                    cssClass = "col-md-";
                    break;
                case GridStyle.Large:
                    cssClass = "col-lg-";
                    break;
            }

            switch (formContext.LayoutStyle)
            {
                case FormLayoutStyle.Default:
                    input = formGroupTag.LastTag("input");
                    if (input != null)
                    {
                        inputType = GetInputType(input);
                        switch (inputType)
                        {
                            case "text":
                            case "password":
                            case "hidden":
                            case "email":                
                            case "file":
                                div = new FlexTagBuilder("div");
                                input.Replace(div);
                                div.AddCssClass("row").AddTag("div").AddCssClass(cssClass + columns.ToString()).AddTag(input);
                                break;
                        }
                    }
                    else
                    {
                        input = formGroupTag.LastTag("select");
                        if (input != null)
                        {
                            div = new FlexTagBuilder("div");
                            input.Replace(div);
                            div.AddCssClass("row").AddTag("div").AddCssClass(cssClass + columns.ToString()).AddTag(input);
                        }
                        else
                        {
                            input = formGroupTag.LastTag("textarea");
                            if (input != null)
                            {
                                div = new FlexTagBuilder("div");
                                input.Replace(div);
                                div.AddCssClass("row").AddTag("div").AddCssClass(cssClass + columns.ToString()).AddTag(input);
                            }
                        }
                    }
                    break;
                case FormLayoutStyle.Horizontal:
                     input = formGroupTag.LastTag("input");
                    if (input != null)
                    {
                        inputType = GetInputType(input);
                        switch (inputType)
                        {
                            case "text":
                            case "password":
                            case "hidden":
                            case "email":                
                            case "file":
                                div = input.ParentTag;
                                div.RemoveCssClass(new Regex(@"\A"+cssClass+@"\d+"));
                                div.AddCssClass(cssClass + columns.ToString());
                                break;
                        }
                    }
                    else
                    {
                        input = formGroupTag.LastTag("select");
                        if (input != null)
                        {
                            div = input.ParentTag;
                            div.AddCssClass(cssClass + columns.ToString());
                        }
                        else
                        {
                            input = formGroupTag.LastTag("textarea");
                            if (input != null)
                            {
                                div = input.ParentTag;
                                div.AddCssClass(cssClass + columns.ToString());
                            }
                        }
                    }
                    break;
                case FormLayoutStyle.Inline:
                    break;
            }
            return formGroupTag;
        }

        #endregion


        #region Input      

        public override FlexTagBuilder InputHeight(FlexTagBuilder tagBuilder, InputHeightStyle size)
        {
            switch (size)
            {
                case InputHeightStyle.Small:
                    tagBuilder.Tag().AddCssClass("input-sm");
                    break;
                case InputHeightStyle.Normal:
                    break;
                case InputHeightStyle.Large:
                    tagBuilder.Tag().AddCssClass("input-lg");
                    break;

            }
            return tagBuilder;
        }       

        #endregion
    }
}