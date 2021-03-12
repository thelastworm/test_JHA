using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Test.Core;

namespace Test.Models
{
    public class EmployeeModel
    {

        public int Id { get; set; }
        [Required]
        [StringLength(100)]

        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]

        public string LastName { get; set; }
        [Required]
        [StringLength(500)]

        public string Address { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

       
        public RoleType Role { get; set; }
        


    }
}
