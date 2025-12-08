using DOMAIN.Enum;
using DOMAIN.Model;
using Microsoft.AspNetCore.Mvc;
using SERVICE.Produto;

namespace Web.Controllers.Produto
{
    public class ProdutoController(ProdutoService produtoService) : Controller
    {
        private readonly ProdutoService produtoService = produtoService;

        [HttpGet]
        public IActionResult CadastrarProduto()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult CadastrarProduto(ProdutoModel produtoModel) {

            try
            {
                List<DisponibilidadeEnum> disponibilidade = [..Enum.GetValues(typeof(DisponibilidadeEnum)).Cast<DisponibilidadeEnum>()];
                ViewBag.disponibilidade = disponibilidade;

                produtoService.CadastrarProduto(produtoModel.NomeProduto!, produtoModel.ValorProduto, produtoModel.Disponibilidade);
                TempData["Sucesso"] = "Produto cadastrado com sucesso";
                return View(produtoModel);

            }catch(Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View(produtoModel);
            }
        }
        [HttpGet]
        public IActionResult ListarProdutos()
        {
            List<ProdutoModel> produtos = produtoService.ListarProdutos();
            return View(produtos);
        }
    }
}
