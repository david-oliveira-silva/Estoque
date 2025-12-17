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
        public IActionResult UpsertProduto(int? codigo)
        {
            ProdutoModel? produto;
            if (codigo.HasValue)
            {
                produto = produtoService.BuscarProduto(codigo.Value);
            }
            else
            {
                produto = new ProdutoModel();
            }
            return View(produto);
        }

        [HttpPost]
        public IActionResult CadastrarProduto(ProdutoModel produtoModel)
        {

            try
            {
                List<DisponibilidadeEnum> disponibilidade = [.. Enum.GetValues(typeof(DisponibilidadeEnum)).Cast<DisponibilidadeEnum>()];
                ViewBag.disponibilidade = disponibilidade;

                produtoService.CadastrarProduto(produtoModel.NomeProduto!, produtoModel.ValorProduto, produtoModel.Disponibilidade);
                TempData["Sucesso"] = "Produto cadastrado com sucesso";
                return RedirectToAction("ListarProdutos");

            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View("UpsertProduto", produtoModel);
            }
        }

        public IActionResult EditarProduto(ProdutoModel produtoModel)
        {
            try
            {
                produtoService.EditarProduto(produtoModel);
                return RedirectToAction("ListarProdutos");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                 View("UpsertProduto", produtoModel);
            }
            return View("UpsertProduto", produtoModel);
        }

        [HttpGet]
        public IActionResult DeletarProduto(int codigo)
        {
            ProdutoModel? produto = produtoService.BuscarProduto(codigo);


            if (produto == null)
            {
                TempData["Erro"] = "Produto não encontrado.";
                return RedirectToAction("ListarProdutos");
            }

            return View(produto);
        }

        [HttpPost]
        public IActionResult DeletarProduto(ProdutoModel produtoModel)
        {
            try
            {
                produtoService.DeletarProduto(produtoModel);
                TempData["Sucesso"] = "Produto deletado com sucesso";
                return RedirectToAction("ListarProdutos");
            }
            catch (Exception ex)
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
