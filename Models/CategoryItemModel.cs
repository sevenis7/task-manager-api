using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class CategoryItemModel
    {
        [Required(ErrorMessage = "Name is requeired")]
        public required string Name { get; set; }

        public string? Icon {  get; set; }
    }
}
