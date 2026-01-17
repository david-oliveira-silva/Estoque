using DOMAIN.Extensoes;
using DOMAIN.Model;
using REPOSITORY.Aluno;

namespace SERVICE.Aluno
{
    public class AlunoService(IAlunoRepository alunoRepository)
    {
        readonly IAlunoRepository _alunoRepository = alunoRepository;

        public void Cadastrar(AlunoModel aluno)
        {
            aluno.ValidarALuno();
            _alunoRepository.Cadastrar(aluno);
        }

        public List<AlunoModel> Listar()
        {
            List<AlunoModel> aluno = [.. _alunoRepository.Listar().OrderBy(a => a.Matricula)];
            return aluno;
        }

        public AlunoModel? BuscarAluno(int matricula)
        {
            AlunoModel? aluno = _alunoRepository.Listar().FirstOrDefault(a => a.Matricula == matricula);
            return aluno;
        }

        public int ObterProximaMatriculaDisponivel()
        {
            List<AlunoModel> aluno = _alunoRepository.Listar();
            
            if(aluno == null || aluno.Count == 0 )
            {
                return 1;
            }

            int UltimaMatricula = aluno.Max(a => a.Matricula) + 1;
            return UltimaMatricula;
    
        }
    }
}
