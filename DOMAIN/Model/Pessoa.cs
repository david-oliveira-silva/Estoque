using DOMAIN.Enum;

namespace DOMAIN.Model
{
    
    public class Pessoa
    {
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