using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class GenderListModel:GetUserId
    {
      
        public List<Complaint> Complaints { get; set; }
       
        public SelectList ComplaintName { get; set; }
      
        public string SearchName { get; set; }
   
        public string SearchString { get; set; }

        public IdentityUser user { get; set; }  

        public GetUserId getid { get; set; }
  

    }
}
