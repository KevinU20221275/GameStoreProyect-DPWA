using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace GameStore.Models
{
    public class MProduct
    {
        [Key]
        public int idProduct { get; set; }

        [Required]
        [Display(Name = "Nombre del Video Juego")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        public string ShortDescription { get; set; }

        [Required]
        [Display (Name = "Descripcion")]
        public string Description { get; set; }

        [Required]
        [Display (Name = "Precio")]
        public double Price { get; set; }

        [Required]
        [Display (Name = "Imagen")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Categoria")]
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
