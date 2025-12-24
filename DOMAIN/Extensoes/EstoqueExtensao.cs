using DOMAIN.Model;

namespace DOMAIN.Extensoes
{
    public static class EstoqueExtensao
    {
        public static void ValidadarCadastro(this EstoqueModel? estoque)
        {

            if (estoque?.QuantidadeEstoque < 0)
            {
                throw new ArgumentException("Quantidade de produtos não pode ser negativa");
            }      
        }

        public static void EstoqueExiste(this EstoqueModel? estoque)
        {
            if (estoque?.CodigoEstoque == 0 || estoque == null)
            {
                throw new ArgumentException("Estoque inválido ou não encontrado para esta operação.");
            }
        }
    }
}
