namespace DOMAIN.Model
{
    public class EstoqueModel
    {
        public int CodigoEstoque;
        public int? QuantidadeEstoque;
        public ProdutoModel? Produto;

        public EstoqueModel() { }

        public EstoqueModel(int QuantidadeEstoque,ProdutoModel? Produto)
        {
            this.QuantidadeEstoque = QuantidadeEstoque;
            this.Produto = Produto;
        }
    }
}
