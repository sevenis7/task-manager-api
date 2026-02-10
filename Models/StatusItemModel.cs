using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class StatusItemModel
    {
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Order is required")]
        public required int Order { get; set; }
    }
}
