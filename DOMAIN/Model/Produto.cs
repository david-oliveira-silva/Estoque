using DOMAIN.Enum;

namespace DOMAIN.Model
{
    public class Produto
    {
        public int CodigoProduto;
        public string? NomeProduto;
        public double ValorProduto;
        public DisponibilidadeEnum Disponibilidade;
        public Produto() { }

        public Produto(string? NomeProduto, double ValorProduto,DisponibilidadeEnum Disponibilidade)
        {
            this.NomeProduto = NomeProduto;
            this.ValorProduto = ValorProduto;
            this.Disponibilidade = Disponibilidade;
        }
    }
}
