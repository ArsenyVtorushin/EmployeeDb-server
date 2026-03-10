using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    [Table("employees", Schema = "public")]
    public class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("full_name")]
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("email")]
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
