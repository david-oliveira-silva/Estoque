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
            List<AlunoModel> aluno = [.. _alunoRepository.Listar().OrderBy(a => a.Nome)];
            return aluno;
        }
    }
}
