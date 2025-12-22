using DOMAIN.Extensoes;
using DOMAIN.Model;
using REPOSITORY.Estoque;

namespace SERVICE.Estoque
{
    public class EstoqueService(IEstoqueRepository estoqueRepository)
    {
        private readonly IEstoqueRepository _estoqueRepository = estoqueRepository;

        public void CadastrarEstoque(EstoqueModel? estoque)
        {
            estoque.ValidadarCadastro();
           
            EstoqueModel? produtoExiste = _estoqueRepository.Listar().FirstOrDefault(e => e.CodigoProduto == estoque?.CodigoProduto);

            if (produtoExiste != null)
            {

                throw new Exception("Produto já foi cadastrado no estoque");
            }
            _estoqueRepository.Cadastrar(estoque!);
        }

        public List<EstoqueModel> ListarEstoque()
        {
            List<EstoqueModel> estoque = _estoqueRepository.Listar();
            return [.. estoque.OrderBy(e => e.CodigoEstoque)];
        }
    }
}
