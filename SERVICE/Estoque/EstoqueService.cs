using DOMAIN.Extensoes;
using DOMAIN.Model;
using REPOSITORY.Estoque;
using REPOSITORY.Produto;

namespace SERVICE.Estoque
{
    public class EstoqueService(IEstoqueRepository estoqueRepository, IProdutoRepository produtoRepository)
    {
        private readonly IEstoqueRepository _estoqueRepository = estoqueRepository;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public void CadastrarEstoque(EstoqueModel? estoque)
        {
            estoque.ValidarCadastro();

            EstoqueModel? produtoExiste = _estoqueRepository.Listar().FirstOrDefault(e => e.CodigoProduto == estoque?.CodigoProduto);
            ProdutoModel? produtoNoBanco = _produtoRepository.Listar().FirstOrDefault(p => p.CodigoProduto == estoque?.CodigoProduto);

            if (produtoExiste != null)
            {

                throw new ArgumentException("Produto já foi cadastrado no estoque");
            }

            if (produtoNoBanco?.Disponibilidade == DOMAIN.Enum.DisponibilidadeEnum.INDISPONÍVEL)
            {
                throw new ArgumentException("Não é possível cadastrar estoque: o produto está INDISPONÍVEL.");
            }
            _estoqueRepository.Cadastrar(estoque!);
        }

        public void DeletarEstoque(EstoqueModel? estoque)
        {
            estoque = BuscarEstoque(estoque?.CodigoEstoque);
            estoque.EstoqueExiste();
            estoque.ValidarExclusao();
            _estoqueRepository.Deletar(estoque!);
        }

        public List<EstoqueModel> ListarEstoque()
        {
            List<EstoqueModel> estoque = _estoqueRepository.Listar();
            return [.. estoque.OrderBy(e => e.CodigoEstoque)];
        }

        public EstoqueModel? BuscarEstoque(int? codigo)
        {
            EstoqueModel? estoque = _estoqueRepository.Listar().FirstOrDefault(e => e.CodigoEstoque == codigo);
            return estoque;
        }

        public void Adicionar(int? codigo,int quantidade)
        {
            EstoqueModel? estoque = _estoqueRepository.Listar().FirstOrDefault(e => e?.Produto?.CodigoProduto == codigo) ?? throw new ArgumentException("Item de estoque não encontrado para atualização.");
            estoque.QuantidadeEstoque += quantidade;
            _estoqueRepository.Editar(estoque);
        }

        public void Remover(int? codigo, int quantidade)
        {
            EstoqueModel? estoque = _estoqueRepository.Listar().FirstOrDefault(e => e?.Produto?.CodigoProduto == codigo) ?? throw new ArgumentException("Item de estoque não encontrado para atualização.");

            estoque.ValidarSaida(quantidade);
            estoque.QuantidadeEstoque -= quantidade;
            _estoqueRepository.Editar(estoque);
        }
    }

}
