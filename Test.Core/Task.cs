using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Core
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        
        public int? AssignedForId { get; set; }

        [ForeignKey("AssignedForId")]
        public Employee Employee { get; set; }

        public ICollection<TaskDetails> TaskDetails { get; set; }


    }
}
