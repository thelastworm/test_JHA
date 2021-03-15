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
        public int SoftwareEngineerId { get; set; }
        public string SoftwareEngineerFirstName { get; set; }
        public string SoftwareEngineerLastName { get; set; }
        public ICollection<TaskDetailsModel> TaskDetails { get; set; }
    }
}
