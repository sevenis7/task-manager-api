using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class PriorityItemModel
    {
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Color is required")]
        public required string Color { get; set; }

        [Required(ErrorMessage = "Order is required")]
        public required int Order { get; set; }

    }
}
