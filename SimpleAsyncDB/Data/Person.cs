using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleAsyncDB.Data
{
    class Person
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(128)]
        public string LastName { get; set; }
        [Required]
        public DateTimeOffset CreationTime { get; set; }
    }
}
