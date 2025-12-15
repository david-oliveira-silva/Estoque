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

            if (codigo.HasValue)
            {
                produtoService.BuscarProduto(codigo.Value);
            }


            return View();
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
                return View(produtoModel);
            }
        }
      
        [HttpGet]
        public IActionResult DeletarProduto(int codigo) // Recebe apenas o código (ID)
        {
            ProdutoModel produto = produtoService.BuscarProduto(codigo);

    
            if (produto == null)
            {
                TempData["Erro"] = "Produto não encontrado.";
                return RedirectToAction("ListarProdutos"); // Redireciona para a listagem
            }

       
            return View(produto);
        }
        [HttpGet]
        public IActionResult ListarProdutos()
        {
            List<ProdutoModel> produtos = produtoService.ListarProdutos();
            return View(produtos);
        }
    }
}
