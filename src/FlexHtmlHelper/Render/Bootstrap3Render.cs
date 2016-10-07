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

        public override FlexTagBuilder FormHelper(FlexTagBuilder tagBuilder, string tagName, string formAction, string formMethod, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder(tagName);
            tag.MergeAttributes(htmlAttributes);
            // action is implicitly generated, so htmlAttributes take precedence.
            if (!string.IsNullOrEmpty(formAction))
            {
                tag.MergeAttribute("action", formAction);
            }

            // method is an explicit parameter, so it takes precedence over the htmlAttributes.
            if (!string.IsNullOrEmpty(formMethod))
            {
                tag.MergeAttribute("method", formMethod, true);
            }

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
                            tag.AddTag(inputTag.AddCssClass("form-control-static"));
                            break;
                        default:
                            tag.AddCssClass("form-group");
                            tag.AddTag(labelTag);
                            tag.AddTag(inputTag.AddCssClass("form-control"));
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

                            fileDivTag.AddTag(inputTag.AddCssClass("form-control"));

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
                            staticDivTag.AddTag(inputTag.AddCssClass("form-control-static"));
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
                            inputDivTag.AddTag(inputTag.AddCssClass("form-control"));
                            inputDivTag.AddTag(validationMessageTag);
                            tag.AddTag(inputDivTag);                            
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
                            tag.AddTag(inputTag.AddCssClass("form-control-static"));
                            break;
                        default:
                            tag.AddCssClass("form-group");
                            labelTag.AddCssClass("sr-only");
                            tag.AddTag(labelTag);
                            tag.AddTag(inputTag.AddCssClass("form-control"));
                            tag.AddTag(validationMessageTag);
                            break;
                    }
                    break;                   
            }
            
            tagBuilder.AddTag(tag);

            return tag;
        }

        public override FlexTagBuilder ButtonHelper(FlexTagBuilder tagBuilder, string type, string text, string value, string name, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("button");
            if (!string.IsNullOrEmpty(text))
            {
                tag.AddText(text);
            }

            tag.MergeAttributes(htmlAttributes);
            
            if (!string.IsNullOrEmpty(type))
            {
                tag.MergeAttribute("type", type);
            }

            if (!string.IsNullOrEmpty(value))
            {
                tag.MergeAttribute("value", value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                tag.MergeAttribute("name", name);
            }

            tag.AddCssClass("btn").AddCssClass("btn-default");

            tagBuilder.AddTag(tag);
            return tagBuilder;
        }

        public override FlexTagBuilder LinkButtonHelper(FlexTagBuilder tagBuilder)
        {
            tagBuilder.Tag().AddCssClass("btn").AddCssClass("btn-default");
            return tagBuilder;
        }

        public override FlexTagBuilder PagingLinkHelper(FlexTagBuilder tagBuilder, int totalItemCount, int pageNumber, int pageSize, int maxPagingLinks, Func<int, string> pagingUrlResolver, IDictionary<string, object> htmlAttributes)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");
            if (totalItemCount < pageNumber)
                throw new ArgumentOutOfRangeException("totalItemCount", totalItemCount, "totalItemCount can not be less than pageNumber");

            // set source to blank list if superset is null to prevent exceptions           
            var pageCount = totalItemCount > 0
                            ? (int)Math.Ceiling(totalItemCount / (double)pageSize)
                            : 0;
            var hasPreviousPage = pageNumber > 1;
            var hasNextPage = pageNumber < pageCount;
            var isFirstPage = pageNumber == 1;
            var isLastPage = pageNumber >= pageCount;
            var firstItemOnPage = (pageNumber - 1) * pageSize + 1;
            var numberOfLastItemOnPage = firstItemOnPage + pageSize - 1;
            var lastItemOnPage = numberOfLastItemOnPage > totalItemCount
                                ? totalItemCount
                                : numberOfLastItemOnPage;

            int halfPagingLinks = maxPagingLinks / 2;
            int startPageNumber = 1;
            int endPageNumber = 1;
            if (pageNumber < halfPagingLinks)
            {
                startPageNumber = 1;
                endPageNumber = pageCount > maxPagingLinks ? maxPagingLinks : pageCount;
            }
            else
            {
                if (pageNumber + halfPagingLinks > pageCount)
                {
                    endPageNumber = pageCount;
                    startPageNumber = pageCount > maxPagingLinks ? pageCount - maxPagingLinks + 1: 1 ;
                }
                else
                {
                    startPageNumber = pageNumber - halfPagingLinks + 1;
                    endPageNumber = pageCount > (startPageNumber + maxPagingLinks - 1) ? (startPageNumber + maxPagingLinks - 1) : pageCount;
                }
            }

            FlexTagBuilder ul = new FlexTagBuilder("ul");
            ul.AddCssClass("pagination");
            
            FlexTagBuilder preLi = new FlexTagBuilder("li");    
            
            if (hasPreviousPage)
            {
                preLi.AddTag("a").MergeAttributes(htmlAttributes).AddHtmlText(@"&laquo;").Attributes.Add("href", pagingUrlResolver(pageNumber - 1));
            }
            else
            {
                preLi.AddCssClass("disabled").AddTag("a").MergeAttributes(htmlAttributes).AddHtmlText(@"&laquo;").Attributes.Add("href", "#");
            }
            ul.AddTag(preLi);


            for(int i=startPageNumber; i<=endPageNumber; i++)
            {
                FlexTagBuilder li = new FlexTagBuilder("li");

                li.AddTag("a").MergeAttributes(htmlAttributes).AddText(i.ToString()).Attributes.Add("href", pagingUrlResolver(i));
                if (i == pageNumber)
                {
                    li.AddCssClass("active");
                }
               
                ul.AddTag(li);
            }

            if (endPageNumber < pageCount)
            {
                FlexTagBuilder moreLi = new FlexTagBuilder("li");
                moreLi.AddCssClass("disabled").AddTag("a").MergeAttributes(htmlAttributes).AddHtmlText(@"...").Attributes.Add("href", "#");
                ul.AddTag(moreLi);
            }

            FlexTagBuilder nextLi = new FlexTagBuilder("li");
            if (hasNextPage)
            {
                nextLi.AddTag("a").MergeAttributes(htmlAttributes).AddHtmlText(@"&raquo;").Attributes.Add("href", pagingUrlResolver(pageNumber + 1));
            }
            else
            {
                nextLi.AddCssClass("disabled").AddTag("a").MergeAttributes(htmlAttributes).AddHtmlText(@"&raquo;").Attributes.Add("href", "#");
            }

            ul.AddTag(nextLi);

            tagBuilder.AddTag(ul);

            return tagBuilder;
        }

        public override FlexTagBuilder IconHelper(FlexTagBuilder tagBuilder, string name, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("span");
            tag.AddCssClass("glyphicon ").AddCssClass("glyphicon-" + name);
            tag.MergeAttributes(htmlAttributes);


            tagBuilder.AddTag(tag);
            return tagBuilder;            
        }

        public override FlexTagBuilder ModalHelper(FlexTagBuilder tagBuilder, string title, IDictionary<string, object> htmlAttributes)
        {
            FlexTagBuilder tag = new FlexTagBuilder("div");
            tag.AddCssClass("modal").AddCssClass("fade").AddAttribute("tabindex", "-1").AddAttribute("role", "dialog").AddAttribute("aria-hidden", "true")
               .AddTag(new FlexTagBuilder("div").AddCssClass("modal-dialog")
                        .AddTag(new FlexTagBuilder("div").AddCssClass("modal-content")
                            .AddTag(new FlexTagBuilder("div").AddCssClass("modal-header")
                                .AddTag(new FlexTagBuilder("button").AddCssClass("close").AddAttribute("type", "button").AddAttribute("data-dismiss", "modal").AddAttribute("aria-hidden", "true").AddHtmlText("&times;"))
                                .AddTag(new FlexTagBuilder("h4").AddCssClass("modal-title").AddText(title??"")))
                            .AddTag(new FlexTagBuilder("div").AddCssClass("modal-body"))
                            .AddTag(new FlexTagBuilder("div").AddCssClass("modal-footer"))));

            tag.MergeAttributes(htmlAttributes);
            tagBuilder.AddTag(tag);
            return tagBuilder;
        }

        public override FlexTagBuilder ModalHeaderHelper(FlexTagBuilder tagBuilder, FlexTagBuilder header)
        {
            var tag = tagBuilder.TagWithCssClass("modal-header");
            if (tag != null)
            {
                tag.AddTag(header);
            }
            return tagBuilder;
        }

        public override FlexTagBuilder ModalBodyHelper(FlexTagBuilder tagBuilder, FlexTagBuilder body)
        {
            var tag = tagBuilder.TagWithCssClass("modal-body");
            if (tag != null)
            {
                tag.AddTag(body);
            }
            return tagBuilder;
        }

        public override FlexTagBuilder ModalFooterHelper(FlexTagBuilder tagBuilder, FlexTagBuilder footer)
        {
            var tag = tagBuilder.TagWithCssClass("modal-footer");
            if (tag != null)
            {
                tag.AddTag(footer);
            }
            return tagBuilder;
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

        public override FlexTagBuilder FormGroupAddInput(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder labelTag, FlexTagBuilder inputTag)
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
                    return tagBuilder;                    
            }

            FlexTagBuilder div = null;

            switch (formContext.LayoutStyle)
            {
                case FormLayoutStyle.Default:
                    div = tagBuilder.TagWithCssClass("div", "checkbox");
                    if (div != null)
                    {
                        div.RemoveCssClass("checkbox");
                        div.ChildTag("label").AddCssClass("checkbox-inline");
                    }
                    else
                    {
                        div = tagBuilder.TagWithCssClass("div", "radio");
                        if (div != null)
                        {
                            div.RemoveCssClass("radio");
                            div.ChildTag("label").AddCssClass("radio-inline");
                        }
                    }
                    tagBuilder.AddCssClass("form-group",true);
                    tagBuilder.AddTag(labelTag);
                    break;
                case FormLayoutStyle.Horizontal:
                    FlexTagBuilder colTag =  tagBuilder.Tag().ChildTag("div");
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
                    tagBuilder.AddTag(labelTag);
                    break;
            }

            return tagBuilder;
        }

        public override FlexTagBuilder FormGroupInputGridColumns(FlexTagBuilder tagBuilder, FlexFormContext formContext, GridStyle style, int columns)
        {
            FlexTagBuilder div = null;
            FlexTagBuilder input = null;            
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
                    input = tagBuilder.LastTag("input");
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
                        input = tagBuilder.LastTag("select");
                        if (input != null)
                        {
                            div = new FlexTagBuilder("div");
                            input.Replace(div);
                            div.AddCssClass("row").AddTag("div").AddCssClass(cssClass + columns.ToString()).AddTag(input);
                        }
                        else
                        {
                            input = tagBuilder.LastTag("textarea");
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
                     input = tagBuilder.LastTag("input");
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
                        input = tagBuilder.LastTag("select");
                        if (input != null)
                        {
                            div = input.ParentTag;
                            div.AddCssClass(cssClass + columns.ToString());
                        }
                        else
                        {
                            input = tagBuilder.LastTag("textarea");
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
            return tagBuilder;
        }


        public override FlexTagBuilder FormGroupButton(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder buttonTag)
        {
            FlexTagBuilder tag = new FlexTagBuilder("div");
            tag.AddCssClass("form-group");
            switch (formContext.LayoutStyle)
            {
                case FormLayoutStyle.Default:
                    tag.AddTag(buttonTag);
                    break;
                case FormLayoutStyle.Horizontal:
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
                    colTag.AddTag(buttonTag);
                    break;
                case FormLayoutStyle.Inline:
                    break;

            } 

            return tagBuilder.AddTag(tag);
        }

        public override FlexTagBuilder FormGroupAddButton(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder buttonTag)
        {
            FlexTagBuilder p = null;           

            var btn = tagBuilder.LastTag("button");
            if (btn == null)
            {
                btn = tagBuilder.LastTag("a");
                if (btn.TagWithCssClass("btn") != btn)
                {
                    btn = null;
                }
            }
            if (btn != null)
            {
                p = btn.ParentTag;
            }            

            if (p != null)
            {
                p.AddText(" ");
                p.AddTag(buttonTag);
            }
            return tagBuilder;
        }

        public override FlexTagBuilder FormGroupHeight(FlexTagBuilder tagBuilder, FormGroupHeightStyle size)
        {

            var groupTag = tagBuilder.TagWithCssClass("form-group");
            if (groupTag != null)
            {
                switch (size)
                {
                    case FormGroupHeightStyle.Small:
                        tagBuilder.Tag().AddCssClass("form-group-sm");
                        break;
                    case FormGroupHeightStyle.Normal:
                        break;
                    case FormGroupHeightStyle.Large:
                        tagBuilder.Tag().AddCssClass("form-group-lg");
                        break;
                }
            }
            return tagBuilder;
        }

        public override FlexTagBuilder FormGroupLink(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder linkTag)
        {
            FlexTagBuilder tag = new FlexTagBuilder("div");
            tag.AddCssClass("form-group");
            switch (formContext.LayoutStyle)
            {
                case FormLayoutStyle.Default:
                    tag.AddTag(linkTag);
                    break;
                case FormLayoutStyle.Horizontal:
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
                    colTag.AddTag(linkTag);
                    break;
                case FormLayoutStyle.Inline:
                    break;

            }

            return tagBuilder.AddTag(tag);
        }

        public override FlexTagBuilder FormGroupAddLink(FlexTagBuilder tagBuilder, FlexFormContext formContext, FlexTagBuilder linkTag)
        {
            FlexTagBuilder p = null;

            var link = tagBuilder.LastTag("a");
            if (link != null)
            {
                p = link.ParentTag;
            }

            if (p != null)
            {
                p.AddText(" ");
                p.AddTag(linkTag);
            }
            return tagBuilder;
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

        #region Button

        public override FlexTagBuilder ButtonSize(FlexTagBuilder tagBuilder, ButtonSizeStyle size)
        {
            switch (size)
            {
                case ButtonSizeStyle.Large:
                    tagBuilder.Tag().AddCssClass("btn-lg");
                    break;
                case ButtonSizeStyle.Small:
                    tagBuilder.Tag().AddCssClass("btn-sm");
                    break;
                case ButtonSizeStyle.ExtraSmall:
                    tagBuilder.Tag().AddCssClass("btn-xs");
                    break;

            }
            return tagBuilder;
        }

        public override FlexTagBuilder ButtonStyle(FlexTagBuilder tagBuilder, ButtonOptionStyle style)
        {

            tagBuilder.Tag().RemoveCssClass("btn-default");

            switch (style)
            {
                case ButtonOptionStyle.Default:
                    tagBuilder.Tag().AddCssClass("btn-default");
                    break;
                case ButtonOptionStyle.Primary:
                    tagBuilder.Tag().AddCssClass("btn-primary");
                    break;
                case ButtonOptionStyle.Success:
                    tagBuilder.Tag().AddCssClass("btn-success");
                    break;
                case ButtonOptionStyle.Warning:
                    tagBuilder.Tag().AddCssClass("btn-warning");
                    break;
                case ButtonOptionStyle.Info:
                    tagBuilder.Tag().AddCssClass("btn-info");
                    break;
                case ButtonOptionStyle.Danger:
                    tagBuilder.Tag().AddCssClass("btn-danger");
                    break;
                case ButtonOptionStyle.Link:
                    tagBuilder.Tag().AddCssClass("btn-link");
                    break;

            }
            return tagBuilder;
        }

        public override FlexTagBuilder ButtonBlock(FlexTagBuilder tagBuilder)
        {
            tagBuilder.Tag().AddCssClass("btn-block");
            return tagBuilder;
        }

        #endregion

        #region Element

        public override FlexTagBuilder Collapse(FlexTagBuilder tagBuilder, string target)
        {
            tagBuilder.Tag().MergeAttribute("data-toggle", "collapse");
            tagBuilder.Tag().MergeAttribute("data-target", target);
            return tagBuilder;
        }

        public override FlexTagBuilder Collapsible(FlexTagBuilder tagBuilder, bool show = false)
        {
            if (show)
            {
                tagBuilder.Tag().AddCssClass("collapse").AddCssClass("in");
            }
            else
            {
                tagBuilder.Tag().AddCssClass("collapse");
            }
            return tagBuilder;
        }

        #endregion

        #region Text Helper

        public virtual FlexTagBuilder TextAlignment(FlexTagBuilder tagBuilder, TextAlignment textAlignment)
        {
            switch (textAlignment)
            {
                case  FlexHtmlHelper.TextAlignment.Left:
                    tagBuilder.Tag().AddCssClass("text-left");
                    break;
                case FlexHtmlHelper.TextAlignment.Center:
                    tagBuilder.Tag().AddCssClass("text-center");
                    break;
                case FlexHtmlHelper.TextAlignment.Right:
                    tagBuilder.Tag().AddCssClass("text-right");
                    break;
                case FlexHtmlHelper.TextAlignment.Justify:
                    tagBuilder.Tag().AddCssClass("text-justify");
                    break;
                case FlexHtmlHelper.TextAlignment.NoWrap:
                    tagBuilder.Tag().AddCssClass("text-nowrap");
                    break;
            }

            return tagBuilder;
        }

        public virtual FlexTagBuilder TextTransformation(FlexTagBuilder tagBuilder, TextTransformation textTransformation)
        {
            switch (textTransformation)
            {
                case FlexHtmlHelper.TextTransformation.LowerCase:
                    tagBuilder.Tag().AddCssClass("text-lowercase");
                    break;
                case FlexHtmlHelper.TextTransformation.UpperCase:
                    tagBuilder.Tag().AddCssClass("text-uppercase");
                    break;
                case FlexHtmlHelper.TextTransformation.Capitalize:
                    tagBuilder.Tag().AddCssClass("text-capitalize");
                    break;
            }
            return tagBuilder;
        }

        public virtual FlexTagBuilder TextContextualColor(FlexTagBuilder tagBuilder, TextContextualColor textContextualColor)
        {
            switch (textContextualColor)
            {
                case FlexHtmlHelper.TextContextualColor.Muted:
                    tagBuilder.Tag().AddCssClass("text-muted");
                    break;
                case FlexHtmlHelper.TextContextualColor.Primary:
                    tagBuilder.Tag().AddCssClass("text-primary");
                    break;
                case FlexHtmlHelper.TextContextualColor.Success:
                    tagBuilder.Tag().AddCssClass("text-success");
                    break;
                case FlexHtmlHelper.TextContextualColor.Info:
                    tagBuilder.Tag().AddCssClass("text-info");
                    break;
                case FlexHtmlHelper.TextContextualColor.Warning:
                    tagBuilder.Tag().AddCssClass("text-warning");
                    break;
                case FlexHtmlHelper.TextContextualColor.Danger:
                    tagBuilder.Tag().AddCssClass("text-danger");
                    break;
            }

            return tagBuilder;
        }

        public virtual FlexTagBuilder ContextualBackground(FlexTagBuilder tagBuilder, ContextualBackground contextualBackground)
        {
            switch (contextualBackground)
            {
                case FlexHtmlHelper.ContextualBackground.Primary:
                    tagBuilder.Tag().AddCssClass("bg-primary");
                    break;
                case FlexHtmlHelper.ContextualBackground.Success:
                    tagBuilder.Tag().AddCssClass("bg-success");
                    break;
                case FlexHtmlHelper.ContextualBackground.Info:
                    tagBuilder.Tag().AddCssClass("bg-info");
                    break;
                case FlexHtmlHelper.ContextualBackground.Warning:
                    tagBuilder.Tag().AddCssClass("bg-warning");
                    break;
                case FlexHtmlHelper.ContextualBackground.Danger:
                    tagBuilder.Tag().AddCssClass("bg-danger");
                    break;
            }

            return tagBuilder;
        }

        #endregion

        #region Link

        public override FlexTagBuilder PagingLinkSize(FlexTagBuilder tagBuilder, PagingLinkSizeStyle size)
        {
            switch (size)
            {
                case PagingLinkSizeStyle.Large:
                    tagBuilder.Tag().AddCssClass("pagination-lg");
                    break;
                case PagingLinkSizeStyle.Small:
                    tagBuilder.Tag().AddCssClass("pagination-sm");
                    break;
            }
            return tagBuilder;
        }

        #endregion

        #region Modal

        public override FlexTagBuilder ModalSize(FlexTagBuilder tagBuilder, ModalSizeStyle size)
        {
            var tag = tagBuilder.TagWithCssClass("modal-dialog");
            if (tag != null)
            {
                switch (size)
                {
                    case ModalSizeStyle.Large:
                        tag.AddCssClass("modal-lg");
                        break;
                    case ModalSizeStyle.Small:
                        tag.AddCssClass("modal-sm");
                        break;
                }
            }
            return tagBuilder;
        }

        public override FlexTagBuilder ModalOption(FlexTagBuilder tagBuilder, string name, string value)
        {
            var tag = tagBuilder.TagWithCssClass("modal");
            if (tag != null)
            {
                tag.AddAttribute("data-"+name, value);
            }
            return tagBuilder;
        }

        #endregion
    }
}