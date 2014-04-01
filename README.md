FlexHtmlHelper
==============

A flexible html helper library for asp.net mvc and asp.net webpages application.

V1.0 is really just a asp.net mvc htmlhelper for bootstrap 3.

Quick Start
----------

* Install Package from Nuget

        Install-Package FlexHtmlHelper.Mvc


* Add the following lines to the view's web.config

        <system.web.webPages.razor>
            <pages pageBaseType="System.Web.Mvc.WebViewPage">
              <namespaces>
                ...     
                <add namespace="FlexHtmlHelper.Mvc"/>
                <add namespace="FlexHtmlHelper.Mvc.Html"/>
                <add namespace="FlexHtmlHelper.Render"/>
                ...
              </namespaces>
            </pages>
        </system.web.webPages.razor>
        
* Add the following line in Application_Start()

        FlexHtmlHelper.FlexRenders.Renders.DefaultRender = FlexHtmlHelper.FlexRenders.Renders["Bootstrap3"];

* Use the FlexHtmlHelper functions, for example,

        @using (var form = Html.f().Form().horizontal().control_label_col_sm(2).control_col_sm(4).control_col_md(4).BeginForm())
        {
            @form.HiddenFor(m => m.PersonId)
            @form.TextBoxFor(m => m.FirstName).input_placeholder("input_lg input_focus").input_focus().input_lg()
            @form.TextBoxFor(m => m.LastName).input_placeholder("has_error").has_error()
            @form.TextBoxFor(m => m.Email).input_placeholder("col_md_6").input_col_md(6)
            @form.TextBoxFor(m => m.Title).input_placeholder("disabled").input_disabled()
            @form.PasswordFor(m => m.Password).input_col_md(3).help_text("at least 6 characters")

            @form.RadioButtonFor(m => m.Gender, "M").label_text("Male").RadioButtonFor(m => m.Gender, "F").label_text("Female")
            @form.DropDownListFor(m => m.TimeZoneId, Model.AvailableTimeZones).input_col_md(6)
            @form.ListBoxFor(m => m.FavoriteMusicGenres, Model.AvailableFavoriteMusicGenres).input_col_md(6).help_text("You can select multiple genres by holding the ctrl key on your keyboard")
            @form.RadioButtonFor(m => m.Race, "W").label_text("White").RadioButtonFor(m => m.Race, "B").label_text("Black").RadioButtonFor(m => m.Race, "A").label_text("Asian").RadioButtonFor(m => m.Race, "L").label_text("latin")
            @form.TextBoxFor(m => m.File, new { type = "file" })
            @form.TextAreaFor(m => m.Description).input_col_md(6)
            @form.CheckBoxFor(m => m.ForumPosts).CheckBoxFor(m => m.Newsletter).CheckBoxFor(m => m.BlogPosts)
            @form.CheckBoxFor(m => m.AcceptTerms).label_text("I have read and accepted the terms and conditions")
            @form.StaticFor(m => m.Log)

            @form.Button(Html.f().Button("submit", "Submit").btn_primary()).Button(Html.f().Button("button", "Cancel"))
            @form.Button(Html.f().Button("button","Delete").btn_danger().btn_block())
        }
		

Why FlexHtmlHelper
----------




