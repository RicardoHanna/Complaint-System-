using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class ComplaintViewModel
    {
        public int Id { get; set; }
   
        public string ComplaintName { get; set; }

        public string LocationComplaint { get; set; }

        public string DescriptionComplaint { get; set; }


        public IFormFile Path { get; set; }
    }
}
