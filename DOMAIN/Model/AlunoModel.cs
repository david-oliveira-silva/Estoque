using DOMAIN.Enum;

namespace DOMAIN.Model
{
    public class AlunoModel : Pessoa
    {
        public int Matricula { get; set; }
        public bool AlunoNovo {  get; set; }

        public AlunoModel()
        {

        }

        public AlunoModel(int matricula, string nome, DateOnly nascimento, SexoEnum sexo, string cpf)
            : base(nome, nascimento, sexo, cpf)
        {
            Matricula = matricula;
        }
    }
}

