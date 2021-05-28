using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiRodrigoNeronFranca
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(14, ErrorMessage = "Este campo é obrigatório!")]
        public int Numero { get; set; }
        public DateTime Data { get; set; }
        [Required]
        public int ProdutosId { get; set; }
        public Produto Produtos { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal => ((decimal)(Quantidade * Valor) - Desconto);
    }
}
