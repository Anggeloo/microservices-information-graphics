using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace microservices_information_graphics.Models
{
    [Table("information_graphics")]
    public class InformationGraphic
    {
        [Key]
        [Column("graphic_id")]
        public int GraphicId { get; set; }

        [Required]
        [Column("graphic_code")]
        [StringLength(100)]
        public string GraphicCode { get; set; }

        [Required]
        [Column("graphic_type")]
        [StringLength(50)]
        public string GraphicType { get; set; }

        [Required]
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("user_created_by")]
        [StringLength(100)]
        public string UserCreatedBy { get; set; }

        [Column("status")]
        public bool Status { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
