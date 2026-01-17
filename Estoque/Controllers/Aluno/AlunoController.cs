using DOMAIN.Model;
using Microsoft.AspNetCore.Mvc;
using SERVICE.Aluno;

namespace Web.Controllers.Aluno
{
    public class AlunoController(AlunoService alunoService) : Controller
    {
        readonly AlunoService _alunoService = alunoService;

        [HttpGet]
        public IActionResult UpsertALuno(int matricula)
        {
            AlunoModel? aluno;
            if (matricula != 0)
            {
                aluno = _alunoService.BuscarAluno(matricula);
                return View(aluno);
            }
            else
            {
                aluno = new()
                {
                    Matricula = _alunoService.ObterProximaMatriculaDisponivel()
                };

                return View(aluno);
            }

        }

        [HttpPost]
        public IActionResult CadastrarAluno(AlunoModel aluno)
        {
            try
            {
                _alunoService.Cadastrar(aluno);
                TempData["Sucesso"] = "Aluno cadastrado com sucesso";
                return RedirectToAction("ListarAlunos");
            }
            catch (Exception ex)
            {

                TempData["Erro"] = ex.Message;
                return View(aluno);
            }
        }

        [HttpGet]
        public IActionResult ListarAluno()
        {
            List<AlunoModel> aluno;

            aluno = _alunoService.Listar();
            return View(aluno);

        }
    }
}
