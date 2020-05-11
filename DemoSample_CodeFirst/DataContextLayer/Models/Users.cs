using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContextLayer.Models
{
    [Table("Employee", Schema = "dbo")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Password { get; set; }


    }
}