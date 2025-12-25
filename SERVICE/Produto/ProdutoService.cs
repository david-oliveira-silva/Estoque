using DOMAIN.Extensoes;
using DOMAIN.Model;
using REPOSITORY.Estoque;
using REPOSITORY.Produto;

namespace SERVICE.Produto;
public class ProdutoService(IProdutoRepository produtoRepository,IEstoqueRepository estoqueRepository)
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository;
    private readonly IEstoqueRepository _estoqueRepository = estoqueRepository;

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
        EstoqueModel? estoque = _estoqueRepository.Listar().FirstOrDefault(e => e.CodigoProduto == produto.CodigoProduto);

        if (estoque != null || estoque?.QuantidadeEstoque > 0)
        {
            throw new InvalidOperationException($"Não é possível excluir o produto '{produto.NomeProduto}' porque ainda existem {estoque.QuantidadeEstoque} unidades em estoque.");
        }
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
