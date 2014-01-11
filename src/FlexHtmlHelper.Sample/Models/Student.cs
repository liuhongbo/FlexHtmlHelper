using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlexHtmlHelperSample.Models
{
    public class Student
    {
        [DisplayName("Student Weight")]
        public decimal weight { get; set; }
    }
}