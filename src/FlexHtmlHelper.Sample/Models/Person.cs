using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FlexHtmlHelperSample.Models
{
    public class Person
    {
        public Person()
        {
            AvailableTimeZones = new List<SelectListItem>();
            AvailableFavoriteMusicGenres = new List<SelectListItem>();
        }

        [DisplayName("PersonId")]
        public int PersonId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
        
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("Race")]
        public string Race { get; set; }

        [DisplayName("Time Zone")]
        public string TimeZoneId { get; set; }

        public IList<SelectListItem> AvailableTimeZones { get; set; }

        [DisplayName("Fafavorite Music Genres")]
        public string FavoriteMusicGenres { get; set; }

        public IList<SelectListItem> AvailableFavoriteMusicGenres { get; set; }

        [DisplayName("Avator")]
        public HttpPostedFileBase File { get; set; }

        [DisplayName("Accept Terms")]
        public bool AcceptTerms { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Log")]
        public string Log { get; set; }

        [DisplayName("Newsletter")]
        public bool Newsletter { get; set; }

        [DisplayName("Forum Posts")]
        public bool ForumPosts { get; set; }

        [DisplayName("Blog Posts")]
        public bool BlogPosts { get; set; }      

    }
}