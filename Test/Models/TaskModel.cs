using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Type { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public int SoftwareEnginnerId { get; set; }
        public string SoftwareEnginnerFirstName { get; set; }
        public string SoftwareEnginnerLastName { get; set; }
        public ICollection<TaskDetailsModel> Tasks { get; set; }
    }
}
