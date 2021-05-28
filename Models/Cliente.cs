using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiRodrigoNeronFranca
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(60, ErrorMessage = "Este campo deve conter maximo de 60 caracteres!")]
        [MinLength(3, ErrorMessage = "Este campo deve conter minimo de 3 caracteres!")]
        public string Nome { get; set; }
        [Required]
        [MaxLength(120, ErrorMessage = "Este campo é obrigatório!")]        
        [MinLength(3, ErrorMessage = "Este campo deve conter minimo de 3 caracteres!")]
        public string Email { get; set; }
        [Required]
        [MaxLength(60, ErrorMessage = "Este campo deve conter maximo de 60 caracteres!")]
        [MinLength(3, ErrorMessage = "Este campo deve conter minimo de 3 caracteres!")]
        public string Aldeia { get; set; }
    }
}
