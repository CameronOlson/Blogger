using System.ComponentModel.DataAnnotations;

namespace Blogger.Models
{
  public class Blog
  {
       public int Id { get; set; }
        [Required]
       
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
   
        public string imgUrl { get; set; }
     
        public bool published { get; set; }
    
        public string CreatorId { get; set; }
       
        public Profile Creator { get; set; }
  }
}