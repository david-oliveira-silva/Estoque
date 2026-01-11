using DOMAIN.Enum;
using Org.BouncyCastle.Asn1.Ocsp;
using System.ComponentModel.DataAnnotations;

namespace DOMAIN.Model
{
    
    public class Pessoa
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100,MinimumLength = 3, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string? Nome { get; set; } 
        public DateOnly? DataNascimento { get; set; } 
        public SexoEnum? Sexo { get; set; } 
        public string? CPF { get; set; }

        public Pessoa()
        {

        }
        public Pessoa(string Nome, DateOnly DataNascimento, SexoEnum Sexo, string CPF)
        {
            this.Nome = Nome;
            this.DataNascimento = DataNascimento;  
            this.Sexo = Sexo;
            this.CPF = CPF;
        }
    }
}