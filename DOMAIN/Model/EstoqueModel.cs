namespace DOMAIN.Model
{
    public class EstoqueModel
    {
        public int CodigoEstoque { get; set; }
        public int CodigoProduto { get; set; }
        public int QuantidadeEstoque { get; set; } = 0;

        public ProdutoModel? Produto { get; set; }
        public EstoqueModel() { }

        public EstoqueModel(int QuantidadeEstoque, ProdutoModel? Produto)
        {
            this.QuantidadeEstoque = QuantidadeEstoque;
            this.Produto = Produto;
            CodigoProduto = Produto?.CodigoProduto ?? 0;
        }
    }
}
