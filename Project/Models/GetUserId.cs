using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class GetUserId
    {
     public GetUserId()
        {
            UserId =UserId;
            Id = Id;
        }
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } 
    }
}
