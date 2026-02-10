using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class UpdateTaskModel
    {
        [Required(ErrorMessage = "Name is requeired")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Description is requeired")]
        public required string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public int? CategoryId { get; set; }

        public int? PriroityId { get; set; }
    }
}
