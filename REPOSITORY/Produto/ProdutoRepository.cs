using DOMAIN.Enum;
using DOMAIN.Model;
using FirebirdSql.Data.FirebirdClient;
using REPOSITORY.Data;

namespace REPOSITORY.Produto
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly FbConnection fbConnection;

        public ProdutoRepository()
        {
            fbConnection = FirebirdConnection.GetFbConnection();
        }
        public void Cadastrar(ProdutoModel produtoModel)
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                string queryInsert = "INSERT INTO PRODUTOS(NomeProduto,ValorProduto,Disponibilidade) VALUES (@NomeProduto,@ValorProduto,@Disponivel)";

                using FbCommand cmdInsert = new(queryInsert, fbConnection);

                cmdInsert.Parameters.AddWithValue(@"NomeProduto", produtoModel.NomeProduto);
                cmdInsert.Parameters.AddWithValue(@"ValorProduto", produtoModel.ValorProduto);
                cmdInsert.Parameters.AddWithValue(@"Disponivel", produtoModel.Disponibilidade);
                cmdInsert.ExecuteNonQuery();

            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }

        }
        public void Editar(ProdutoModel produtoModel)
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                string queryUpdate = "UPDATE PRODUTOS SET NomeProduto = @NomeProduto,ValorProduto = @ValorProduto, Disponibilidade = @Disponibilidade WHERE CodigoProduto = @CodigoProduto";

                using FbCommand cmdUpdate = new(queryUpdate, fbConnection);

                cmdUpdate.Parameters.AddWithValue(@"CodigoProduto", produtoModel.CodigoProduto);
                cmdUpdate.Parameters.AddWithValue(@"NomeProduto", produtoModel.NomeProduto);
                cmdUpdate.Parameters.AddWithValue(@"ValorProduto", produtoModel.ValorProduto);
                cmdUpdate.Parameters.AddWithValue(@"Disponivel", produtoModel.Disponibilidade);
                cmdUpdate.ExecuteNonQuery();
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
        }

        public void Deletar(ProdutoModel produtoModel)
        {

            try
            {
                FirebirdConnection.OpenConnection(fbConnection);
                string queryDelete = "DELETE FROM PRODUTOS WHERE CodigoProduto = @CodigoProduto";

                using FbCommand cmdDelete = new(queryDelete, fbConnection);
                cmdDelete.Parameters.AddWithValue(@"CodigoProduto", produtoModel.CodigoProduto);
                cmdDelete.ExecuteNonQuery();
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
        }

        public List<ProdutoModel> Listar()
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);
                List<ProdutoModel> ListaProdutos = [];

                string querySelect = "SELECT * FROM PRODUTOS";
                using FbCommand cmdSelect = new(querySelect, fbConnection);
                using var Reader = cmdSelect.ExecuteReader();

                int CodigoOrdinal = Reader.GetOrdinal("CodigoProduto");
                int ValorOrdinal = Reader.GetOrdinal("ValorProduto");
                int NomeOrdinal = Reader.GetOrdinal("NomeProduto");
                int DisponibilidadeOrdinal = Reader.GetOrdinal("Disponibilidade");


                while (Reader.Read())
                {
                    ProdutoModel produto = new() {
                        CodigoProduto = Reader.GetInt32(CodigoOrdinal),
                        NomeProduto = Reader.GetString(NomeOrdinal),
                        ValorProduto = Reader.GetDecimal(ValorOrdinal),
                        Disponibilidade = (DisponibilidadeEnum)Reader.GetInt32(DisponibilidadeOrdinal)
                    };
                    ListaProdutos.Add(produto);
                }
                return ListaProdutos;
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
        }
    }
}
