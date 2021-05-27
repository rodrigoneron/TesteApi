using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiRodrigoNeronFranca
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage ="Este campo é obrigatório!")]
        public string Descricao { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero!")]
        public decimal Valor { get; set; }
        public string Foto { get; set; }
    }
}
