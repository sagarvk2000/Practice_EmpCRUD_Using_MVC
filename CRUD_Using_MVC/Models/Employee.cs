using System.ComponentModel.DataAnnotations;

namespace CRUD_Using_MVC.Models
{
    public class Employee
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        public string? Department { get; set; }

        [ScaffoldColumn(false)]
        public int isActive { get; set; }
    }
}
