using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "This field cannot be empty")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Enter a valid category name")]
        public string Name { get; set; }

        public int BookCount { get; set; }
    }
}