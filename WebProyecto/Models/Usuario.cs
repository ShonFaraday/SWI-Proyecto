using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProyecto.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Display(Name = "Código")]
        public int ID_U { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El PIN es obligatorio")]
        [StringLength(15, ErrorMessage = "Máximo 15 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "PIN")]
        public string PIN { get; set; }
    }
}