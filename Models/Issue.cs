using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Issue
    {
        public int ID { get; set; }

        [Required]
        public int BookID { get; set; }
        public int BookISBN { get; set; }
        public string BookTitle { get; set; }

        [Required]
        public int StudentID { get; set; }
        public string StudentName { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ReturnDate { get; set; }
        public DateTime ReturnedOn { get; set; }

        public double PenaltyAmount { get; set; }
        public int Status { get; set; }
    }

    public class IssueBundle
    {
        public List<Issue> Issued { get; set; }
        public List<Issue> Returned { get; set; }
        public Issue NewIssue { get; set; }
    }
}