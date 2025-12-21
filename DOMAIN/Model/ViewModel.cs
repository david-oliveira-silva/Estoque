namespace DOMAIN.Model
{
    public class ViewModel
    {
        public List<ProdutoModel> Produto = [];
        public EstoqueModel? Estoque { get; set; }
        public int QuantidadeAdicionar {  get; set; }

    }
}
