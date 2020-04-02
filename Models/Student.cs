using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Student
    {
        public int ID { get; set; }
        

        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "This field cannot be empty")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Enter a valid book title")]
        public string Name { get; set; }


        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "This field cannot be empty")]
        [RegularExpression("^([6-9]{1})([0-9]{9})$", ErrorMessage = "Enter a valid mobile number")]
        public long Mobile { get; set; }


        [Required(ErrorMessage = "This field cannot be empty")]
        [EmailAddress(ErrorMessage = "Enter valid Email ID")]
        public string Email { get; set; }


        [Required(ErrorMessage = "This field cannot be empty")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Enter a valid address")]
        public string Address { get; set; }


        [Required(ErrorMessage = "This field cannot be empty")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Enter a valid city name")]
        public string City { get; set; }
        

        [Required(ErrorMessage = "This field cannot be empty")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Enter a valid state name")]
        public string State { get; set; }


        [Required(ErrorMessage = "This field cannot be empty")]
        [RegularExpression("^([0-9]{6})$", ErrorMessage = "Enter a valid area pin code")]
        public int PinCode { get; set; }

        public int BookIssuedCount { get; set; }
    }

    public class StudentBundle
    {
        public Student StudentDetails { get; set; }
        public List<Issue> IssueDetails { get; set; }
    }
}