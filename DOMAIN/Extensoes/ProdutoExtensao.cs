
using DOMAIN.Model;

namespace DOMAIN.Extensoes
{
    public  static class ProdutoExtensao
    {

        public static void ValidarProduto(this ProdutoModel produto)
        {
            if (string.IsNullOrWhiteSpace(produto.NomeProduto))
            {
                throw new Exception("Nome não pode ser vazio");
            }
            if (produto.ValorProduto < 0)
            {
                throw new Exception("Valor não pode ser negativo");
            }

        }

        public static void ValidarExistencia(this ProdutoModel produto) {

            if (produto == null || produto.CodigoProduto <= 0)
            {
                throw new Exception("Produto inválido ou não encontrado para esta operação.");
            }
          
        }
    }
}
