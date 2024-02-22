using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class MCategory
    {
        [Key]
        public int idCategory { get; set; }

        [Required]
        [Display (Name = "Nombre de la Categoria")]
        public string CategoryName { get; set; }
    }
}
