using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiJogos.InputModel
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(1000,MinimumLength = 3, ErrorMessage = "O Nome do jogo precisa ter no mínimo 3 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "O Nome da produtora precisa ter no mínimo 3 caracteres")]
        public string Produtora { get; set; }
        [Required]
        public double Preco { get; set; }
        [Required]
        public double Nota { get; set; }
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "O Tipo precisa ter no mínimo 3 caracteres")]
        public string Tipo { get; set; }
    }
}
