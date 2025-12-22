using DOMAIN.Enum;
using DOMAIN.Model;
using FirebirdSql.Data.FirebirdClient;
using REPOSITORY.Data;

namespace REPOSITORY.Estoque
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly FbConnection fbConnection;
        public EstoqueRepository()
        {
            fbConnection = FirebirdConnection.GetFbConnection();
        }

        public void Cadastrar(EstoqueModel estoqueModel)
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                string QueryInsert = "INSERT INTO ESTOQUE(CodigoProduto,QuantidadeEstoque) VALUES (@CodigoProduto,@QuantidadeEstoque)";

                using FbCommand cmdInsert = new(QueryInsert, fbConnection);
                cmdInsert.Parameters.AddWithValue(@"CodigoProduto", estoqueModel.CodigoProduto);
                cmdInsert.Parameters.AddWithValue(@"QuantidadeEstoque", estoqueModel.QuantidadeEstoque);
                cmdInsert.ExecuteNonQuery();

            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
        }

        public void Deletar(EstoqueModel estoqueModel)
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                string QueryDelete = "DELETE FROM ESTOQUE WHERE CodigoEstoque = @CodigoEstoque";
                using FbCommand cmdDelete = new(QueryDelete, fbConnection);
                cmdDelete.Parameters.AddWithValue(@"CodigoEstoque", estoqueModel.CodigoEstoque);
                cmdDelete.ExecuteNonQuery();
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
        }

        public void Editar(EstoqueModel Entity)
        {
            
        }

        public List<EstoqueModel> Listar()
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                List<EstoqueModel> listarEstoque = [];

                string QuerySelect = "SELECT * FROM ESTOQUE E INNER JOIN PRODUTOS P ON E.CodigoProduto = P.CodigoProduto";
                using FbCommand cmdSelect = new(QuerySelect, fbConnection);
                using FbDataReader reader = cmdSelect.ExecuteReader();

                int CodigoOrdinal = reader.GetOrdinal("CodigoEstoque");
                int QuantidadeEstoqueOrdinal = reader.GetOrdinal("QuantidadeEstoque");
                int CodigoProdutoOrdinal = reader.GetOrdinal("CodigoProduto");
                int NomeProdutoOrdinal = reader.GetOrdinal("NomeProduto");
                int ValorProdutoOrdinal = reader.GetOrdinal("ValorProduto");
                int DisponibilidadeOrdinal = reader.GetOrdinal("Disponibilidade");
                while (reader.Read())
                {
                    EstoqueModel estoque = new()
                    {
                        CodigoEstoque = reader.GetInt32(CodigoOrdinal),
                        QuantidadeEstoque = reader.GetInt32(QuantidadeEstoqueOrdinal),
                        CodigoProduto = reader.GetInt32(CodigoProdutoOrdinal),
                        Produto = new ProdutoModel()
                        {
                            CodigoProduto = reader.GetInt32(CodigoProdutoOrdinal),
                            NomeProduto  = reader.GetString(NomeProdutoOrdinal),
                            ValorProduto = reader.GetDecimal(ValorProdutoOrdinal),
                            Disponibilidade = (DisponibilidadeEnum)reader.GetInt32(DisponibilidadeOrdinal)
                           
                        }

                    };

                    listarEstoque.Add(estoque);

                }

                return listarEstoque;
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
        }
    }
}
