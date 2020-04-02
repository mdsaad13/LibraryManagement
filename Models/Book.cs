using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Required]
        public int ISBN { get; set; }


        [Display(Name = "Book Title")]
        [Required(ErrorMessage = "This field cannot be empty")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Enter a valid book title")]
        public string Title { get; set; }


        [Display(Name = "Publication")]
        [Required(ErrorMessage = "Kindly select a publication")]
        public int PubID { get; set; }


        [Display(Name = "Publication")]
        public string PubName { get; set; }


        [Display(Name = "Category")]
        [Required(ErrorMessage = "Kindly select a category")]
        public int CatID { get; set; }


        [Display(Name = "Category")]
        public string CatName { get; set; }


        [Display(Name = "Book Cover")]
        public string ImgUrl { get; set; }


        [Required(ErrorMessage = "Please select a file")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase Image { get; set; }


        [Display(Name = "Book Author")]
        [Required(ErrorMessage = "This field cannot be empty")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Enter a valid book author")]
        public string Author { get; set; }


        [Required]
        public string Edition { get; set; }

        public int IsIssued { get; set; }
    }

    public class BookBundle
    {
        public Book BookDetails { get; set; }
        public List<Issue> IssueList { get; set; }
    }
}