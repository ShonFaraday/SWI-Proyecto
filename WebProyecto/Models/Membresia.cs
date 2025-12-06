using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProyecto.Models
{
   
        [Table("Membresias")]
        public class Membresia
        {
            [Key]
            [Display(Name = "Código")]
            public int ID_M { get; set; }

            [Required(ErrorMessage = "El nombre de la membresía es obligatorio")]
            [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
            public string Nombre { get; set; }

            [StringLength(150, ErrorMessage = "Máximo 150 caracteres")]
            [Display(Name = "Descripción")]
            public string? Descripcion { get; set; }

            [Display(Name = "Máx. libros por préstamo")]
            [Range(1, int.MaxValue, ErrorMessage = "Debe ser al menos 1")]
            public int Max_Libros_Prestamo { get; set; }

            [Display(Name = "Días de préstamo")]
            [Range(1, 365, ErrorMessage = "Debe estar entre 1 y 365 días")]
            public int Dias_Prestamo { get; set; }

            [Display(Name = "Costo")]
            [DataType(DataType.Currency)]
            [Column(TypeName = "decimal(10,2)")]
            [Range(0, 999999, ErrorMessage = "El costo no puede ser negativo")]
            public decimal Costo { get; set; }
        }
    }


