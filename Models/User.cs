using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DemoApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Display(Name = "First Name")]
        public String FirstName { get; set; }
        [Display(Name = "Last Name")]
        public String LastName { get; set; }
        public virtual IList<Job> Jobs { get; set; }
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }
    }
}