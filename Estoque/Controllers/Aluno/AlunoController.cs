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

                if (aluno == null)
                {
                    TempData["Erro"] = "Aluno não encontrado";
                    return RedirectToAction("ListarAluno");
                }

                aluno.AlunoNovo = false;
                return View(aluno);

            }
            else
            {
                aluno = new()
                {
                    Matricula = _alunoService.ObterProximaMatriculaDisponivel(),
                    AlunoNovo = true
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
                return RedirectToAction("ListarAluno");
            }
            catch (Exception ex)
            {
                aluno.AlunoNovo = true;
                TempData["Erro"] = ex.Message;
                return View("UpsertAluno", aluno);
            }
        }

        public IActionResult EditarAluno(AlunoModel aluno)
        {
            try
            {
                _alunoService.Editar(aluno);
                TempData["Sucess"] = "Aluno editado com sucesso";
                return RedirectToAction("ListarAluno");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View("UpsertAluno", aluno);
            }
        }

        [HttpGet]
        public IActionResult DeletarAluno(int matricula)
        {
            AlunoModel? aluno = _alunoService.BuscarAluno(matricula);
            return View(aluno);
        }

        [HttpPost]
        public IActionResult DeletarAluno(AlunoModel aluno)
        {
            try
            {
                _alunoService.Deletar(aluno);
                TempData["Sucesso"] = "Aluno deletado com sucesso";
                return RedirectToAction("ListarAluno");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View("UpsertAluno", aluno);
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
