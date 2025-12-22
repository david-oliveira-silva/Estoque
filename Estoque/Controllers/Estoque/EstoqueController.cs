using DOMAIN.Model;
using Microsoft.AspNetCore.Mvc;
using SERVICE.Estoque;
using SERVICE.Produto;

namespace Web.Controllers.Estoque
{
    public class EstoqueController(EstoqueService estoqueService, ProdutoService produtoService) : Controller
    {
        private readonly EstoqueService _estoqueService = estoqueService;
        private readonly ProdutoService _produtoService = produtoService;

        [HttpGet]
        public IActionResult CadastrarEstoque()
        {
            ViewModel viewModel = new()
            {
                Produto = _produtoService.ListarProdutos()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult CadastrarEstoque(ViewModel? viewModel)
        {
            try
            {
                _estoqueService.CadastrarEstoque(viewModel?.Estoque);
                TempData["Sucesso"] = "Produto adicionado ao estoque com sucesso";
                return RedirectToAction("ListarEstoque");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View(viewModel);
            }

        }
        public IActionResult ListarEstoque()
        {
            List<EstoqueModel> estoque = _estoqueService.ListarEstoque();
            return View(estoque);
        }
    }
}
