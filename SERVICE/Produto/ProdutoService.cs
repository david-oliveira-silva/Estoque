using DOMAIN.Extensoes;
using DOMAIN.Model;
using REPOSITORY.Produto;

namespace SERVICE.Produto;
public class ProdutoService(IProdutoRepository produtoRepository)
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository;

    public void CadastrarProduto(ProdutoModel produto)
    { 
        produto.ValidarProduto();
        _produtoRepository.Cadastrar(produto);
    }
    public void EditarProduto(ProdutoModel produto)
    {
        produto.ValidarProduto();
        _produtoRepository.Editar(produto);
    }

    public void DeletarProduto(ProdutoModel produto)
    {
        produto.ValidarExistencia();
        _produtoRepository.Deletar(produto);
    }
    
    public List<ProdutoModel> ListarProdutos()
    {
        List<ProdutoModel> ProdutosList = _produtoRepository.Listar();
        ProdutosList = [.. ProdutosList.OrderBy(p => p.CodigoProduto)];
        return ProdutosList;
    }
    public ProdutoModel? BuscarProduto(int? codigo)
    {
        ProdutoModel? produto = _produtoRepository.Listar().FirstOrDefault(p => p.CodigoProduto == codigo);
        return produto;
    }
}
