using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContextLayer.Models
{
    [Table("Employee", Schema = "dbo")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [ForeignKey("DesignationInfo")]
        [Required]
        public int DesignationId { get; set; }

        [NotMapped]
        public string DesignationName { get; set; }
        
        public virtual Designation DesignationInfo { get; set; }
    }
}