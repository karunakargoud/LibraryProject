using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class Library
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LibraryId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public Student Student { get; set; }

        public Book Book { get; set; }

        
    }
}