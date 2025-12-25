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

        [HttpGet]
        public IActionResult DeletarEstoque(int? codigo)
        {
            EstoqueModel? estoque = _estoqueService.BuscarEstoque(codigo);
            ViewModel viewModel = new()
            {
                Estoque = estoque,
                Produto = _produtoService.ListarProdutos()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeletarEstoque(ViewModel viewModel)
        {
            try
            {
                _estoqueService.DeletarEstoque(viewModel.Estoque);
                TempData["Sucesso"] = "Estoque deletado com sucesso";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                viewModel.Produto = _produtoService.ListarProdutos();
            }
            return RedirectToAction("ListarEstoque");
        }

        public IActionResult ListarEstoque()
        {
            List<EstoqueModel> estoque = _estoqueService.ListarEstoque();
            return View(estoque);
        }

        [HttpGet]
        public IActionResult EntradaEstoque()
        {
            ViewModel viewModel = new()
            {
                Produto = _produtoService.ListarProdutos()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EntradaEstoque(int codigo, int quantidade)
        {
            try
            {
                _estoqueService.Adicionar(codigo, quantidade);
                TempData["Sucesso"] = $"Foi adicionado {quantidade} unidades ao estoque";
                return RedirectToAction("ListarEstoque");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                ViewModel viewModel = new()
                {
                    Produto = _produtoService.ListarProdutos()
                };
                return View(viewModel);
            }

        }

        [HttpGet]
        public IActionResult SaidaEstoque()
        {
            ViewModel viewModel = new()
            {
                Produto = _produtoService.ListarProdutos()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaidaEstoque(int codigo, int quantidade)
        {
            try
            {
                _estoqueService.Remover(codigo,quantidade);
                TempData["Sucesso"] = $"Foi removido {quantidade} unidades do estoque";
                return RedirectToAction("ListarEstoque");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                ViewModel viewModel = new()
                {
                    Produto = _produtoService.ListarProdutos()
                };
                return View(viewModel);
            }
        }
    }
}
