using DOMAIN.Model;
using Microsoft.AspNetCore.Mvc;
using SERVICE.Aluno;

namespace Web.Controllers.Aluno
{
    public class AlunoController(AlunoService alunoService) : Controller
    {
        readonly AlunoService _alunoService = alunoService;

        [HttpGet]
        public IActionResult CadastrarAluno()
        {
            AlunoModel? aluno = new();
            return View();
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
catch (Exception ex) {

                TempData["Erro"] = ex.Message;
                return View(aluno);
            }
        }
    }
}
