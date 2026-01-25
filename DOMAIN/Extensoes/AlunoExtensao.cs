 using DOMAIN.Model;

namespace DOMAIN.Extensoes
{
    public static class AlunoExtensao
    {
        public static void ValidarALuno(this AlunoModel alunoModel)
        {
            if (alunoModel.Matricula <= 0)
            {
                throw new ArgumentException("O número da matrícula deve ser maior que 0");
            }
            if (string.IsNullOrEmpty(alunoModel.Nome))
            {
                throw new ArgumentException("Nome não pode ser vazio");
            }
            if (alunoModel.DataNascimento > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("Data de nascimento não pode ser maior que a data atual");
            }
            
            if(alunoModel.DataNascimento == null)
            {
                throw new ArgumentException("Data de nascimento não pode ser nula");
            }
            if(alunoModel.Sexo == null)
            {
                throw new ArgumentException("Data de nascimento não pode ser nula");
            }

           alunoModel.Nome =  alunoModel.Nome.ToUpper().Trim();
        }

        public static void AlunoExiste(this AlunoModel alunoModel)
        {
            if(alunoModel == null)
            {
                throw new FileNotFoundException("Aluno não encontrado");
            }  
        }
    }
}
