using DOMAIN.Model;

namespace DOMAIN.Extensoes
{
    public static class EstoqueExtensao
    {
        public static void ValidadarCadastro(this EstoqueModel? estoque)
        {
          
            if(estoque?.QuantidadeEstoque < 0)
            {
                throw new ArgumentException("Quantidade de produtos não pode ser negativa");
            }
            
        }
    }
}
