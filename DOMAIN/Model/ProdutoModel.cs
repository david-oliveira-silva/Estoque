using DOMAIN.Enum;

namespace DOMAIN.Model
{
    public class ProdutoModel
    {
        public int CodigoProduto;
        public string? NomeProduto;
        public double ValorProduto;
        public DisponibilidadeEnum Disponibilidade;
        public ProdutoModel() { }

        public ProdutoModel(string? NomeProduto, double ValorProduto,DisponibilidadeEnum Disponibilidade)
        {
            this.NomeProduto = NomeProduto;
            this.ValorProduto = ValorProduto;
            this.Disponibilidade = Disponibilidade;
        }
    }
}
