using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContextLayer.Models
{
    [Table("Designation", Schema = "dbo")]
    public class Designation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DesignationId { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        [Display(Name = "Designation Code")]
        public string DesignationCode { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Designation Name")]
        public string DesignationName { get; set; }

        [ForeignKey("DepartmentInfo")]
        [Required]
        public int DepartmentId { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }
        
        public virtual Department DepartmentInfo { get; set; }
    }
}
