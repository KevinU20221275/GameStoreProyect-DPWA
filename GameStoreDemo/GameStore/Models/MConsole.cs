using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class MConsole
    {
        [Key]
        public int idConsole { get; set; }

        [Required]
        [Display (Name = "Nombre de la Consola" )]
        public string consoleName { get; set; }
    }
}
