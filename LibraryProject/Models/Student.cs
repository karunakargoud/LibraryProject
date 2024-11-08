using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Class { get; set; }

        public string Photo { get; set; }

        public string Video {  get; set; }
        [NotMapped]
        public HttpFileCollectionBase FileCollection { get; set; }

        public List<Library> Libraries { get; set; }

    }
}