using System.ComponentModel.DataAnnotations;

namespace DOTNetCoreADONETFramworkDemo.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? City { get; set; }
    }
}
