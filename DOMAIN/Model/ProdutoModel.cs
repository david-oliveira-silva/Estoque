using DOMAIN.Enum;

namespace DOMAIN.Model
{
    public class ProdutoModel
    {
        public int CodigoProduto { get; set;}
        public string? NomeProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public DisponibilidadeEnum Disponibilidade { get; set; }
        public ProdutoModel() { }

        public ProdutoModel(string? NomeProduto, decimal ValorProduto,DisponibilidadeEnum Disponibilidade)
        {
            this.NomeProduto = NomeProduto;
            this.ValorProduto = ValorProduto;
            this.Disponibilidade = Disponibilidade;
        }
    }
}
