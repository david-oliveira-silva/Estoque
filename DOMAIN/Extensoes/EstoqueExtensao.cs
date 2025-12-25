using DOMAIN.Model;

namespace DOMAIN.Extensoes
{
    public static class EstoqueExtensao
    {
        public static void ValidarCadastro(this EstoqueModel? estoque)
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

        public static void ValidarSaida(this EstoqueModel? estoque, int quantidade)
        {

            if (estoque?.QuantidadeEstoque < quantidade)
            {
                throw new ArgumentException($"Quantidade indisponível. No momento, o saldo em estoque é menor que {quantidade}.");
            }

        }

        public static void ValidarExclusao(this EstoqueModel? estoque)
        {
            if (estoque?.QuantidadeEstoque > 0)
            {
                throw new ArgumentException("Não é possível excluir este produto porque ele já possui movimentações de entrada registradas");
            }
        }
    } 
}
