using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class MUser
    {
        [Key]
        public int IdUser { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Nombre de Usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [Display (Name = "Correo Electronico")]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        [Display (Name = "Contraseña")]
        public string Password { get; set; }


        public bool Reset { get; set; }

        public bool Signed { get; set; }

        public string Token { get; set; }
    }
}
