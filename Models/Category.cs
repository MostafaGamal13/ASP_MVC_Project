using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public virtual ICollection<Item>? Items { get; set; }
    }
}
