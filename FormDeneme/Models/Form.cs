using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormDeneme.Models
{
    public class Form
    {
        public int Id { get; set; }

        [Column(TypeName = "nchar(50)")]
        [Required(ErrorMessage = "Bu alan gerekli")]
        [DisplayName("Form Adı")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter")]
        public string Name { get; set; }

        [Column(TypeName = "nchar(150)")]
        [Required(ErrorMessage = "Bu alan gerekli")]
        [DisplayName("Açıklama")]
        [StringLength(150, ErrorMessage = "En fazla 150 karakter")]
        public string Description { get; set; }


        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedAt { get; set; }
    }
}
