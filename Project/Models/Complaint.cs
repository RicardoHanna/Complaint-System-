using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 4)]
        [Required(ErrorMessage ="The Category field is requied")]
        public string ComplaintName { get; set; }
        [StringLength(15, MinimumLength = 4)]
        [Required(ErrorMessage = "The Location field is requied")]
        public string LocationComplaint { get; set; }
        [StringLength(60,MinimumLength =7)]
        [Required(ErrorMessage = "The Description field is requied")]
        public string DescriptionComplaint { get; set; }
        [Required]
        public string Path { get; set; }

    

       
    }

}

