using DOMAIN.Enum;
using DOMAIN.Model;
using REPOSITORY.Produto;

namespace SERVICE.Produto;
public class ProdutoService(IProdutoRepository produtoRepository)
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository;

    public void CadastrarProduto(string NomeProduto, decimal ValorProduto, DisponibilidadeEnum Disponibilidade)
    {
        if (string.IsNullOrWhiteSpace(NomeProduto))
        {
            throw new Exception("Nome não pode ser vazio");
        }
        if (ValorProduto < 0)
        {
            throw new Exception("Valor não pode ser negativo");
        }

        ProdutoModel ProdutoModel = new(NomeProduto, ValorProduto, Disponibilidade);
        _produtoRepository.Cadastrar(ProdutoModel);

    }
    public void EditarProduto(ProdutoModel produtoModel)
    {
        if (string.IsNullOrWhiteSpace(produtoModel.NomeProduto))
        {
            throw new Exception("Nome não pode ser vazio");
        }
        if (produtoModel.ValorProduto <= 0)
        {
            throw new Exception("Valor não pode ser negativo");
        }

        _produtoRepository.Editar(produtoModel);
    }

    public void DeletarProduto(ProdutoModel produtoModel)
    {
        if(produtoModel == null)
        {
            throw new Exception("Produto não encontrado");
        }
        if(produtoModel.CodigoProduto == 0)
        {
            throw new Exception("Produto não encontrado");
        }
        _produtoRepository.Deletar(produtoModel);
    }

    public List<ProdutoModel> ListarProdutos()
    {
        List<ProdutoModel> ProdutosList =  _produtoRepository.Listar();
        ProdutosList = [.. ProdutosList.OrderBy(p => p.CodigoProduto)];
        return ProdutosList;
    }
    public ProdutoModel? BuscarProduto(int? codigo)
    {
        ProdutoModel? produto = _produtoRepository.Listar().FirstOrDefault(p => p.CodigoProduto == codigo);
        return produto;
        
    }
}
