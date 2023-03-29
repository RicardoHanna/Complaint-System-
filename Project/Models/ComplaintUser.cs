using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class ComplaintUser
    {
   
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser IdentityUser{ get; set; }

      
        public int ComplaintId { get; set; }

        [ForeignKey("Id")]
        public virtual Complaint Complaint { get; set; }


    }
}