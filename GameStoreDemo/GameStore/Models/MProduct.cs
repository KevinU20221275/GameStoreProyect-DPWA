using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class MProduct
    {
        [Key]
        public int idProduct { get; set; }

        [Required]
        [Display(Name = "Nombre del Video Juego")]
        public string productName { get; set; }

        [Display (Name = "Categoria")]
        public int idCategory { get; set; }

        [Display (Name = "Categoria")]
        [ForeignKey("idCategory")]
        public virtual MCategory Category { get; set; }

        [Display(Name = "Consola")]
        public int idConsole { get; set; }

        [Display (Name = "Consola")]
        [ForeignKey("idConsole")]
        public virtual MConsole Console { get; set;}
    }
}
