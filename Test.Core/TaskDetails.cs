using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Core
{
    public class TaskDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int? TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Task Task { get; set; }


    }
}
