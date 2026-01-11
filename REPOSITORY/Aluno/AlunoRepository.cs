using DOMAIN.Enum;
using DOMAIN.Model;
using FirebirdSql.Data.FirebirdClient;
using REPOSITORY.Data;

namespace REPOSITORY.Aluno
{
    public class AlunoRepository : IAlunoRepository
    {
        public readonly FbConnection fbConnection;

        public AlunoRepository()
        {
            fbConnection = FirebirdConnection.GetFbConnection();
        }
        public void Cadastrar(AlunoModel alunoModel)
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                string QueryInsert = "INSERT INTO ALUNO(MATRICULA,ALUNONOME,DATANASCIMENTO,SEXO,CPF) VALUES(@MATRICULA,@ALUNONOME,@DATANASCIMENTO,@SEXO,@CPF)";
                using FbCommand cmdInsert = new(QueryInsert, fbConnection);

                cmdInsert.Parameters.AddWithValue("@MATRICULA", alunoModel.Matricula);
                cmdInsert.Parameters.AddWithValue("@ALUNONOME", alunoModel.Nome);
                cmdInsert.Parameters.AddWithValue("@DATANASCIMENTO", alunoModel.DataNascimento);
                cmdInsert.Parameters.AddWithValue("@SEXO", alunoModel.Sexo);
                cmdInsert.Parameters.AddWithValue("@CPF", alunoModel.CPF);
                cmdInsert.ExecuteNonQuery();
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
        }

        public void Deletar(AlunoModel Entity)
        {
            throw new NotImplementedException();
        }

        public void Editar(AlunoModel Entity)
        {
            throw new NotImplementedException();
        }

        public List<AlunoModel> Listar()
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                List<AlunoModel> ListaAluno = [];
                string QuerySelect = "SELECT * FROM ALUNO";

                using FbCommand cmdSelect = new(QuerySelect, fbConnection);
                using var Reader = cmdSelect.ExecuteReader();

                while (Reader.Read())
                {
                    int MatriculaOrdinal = Reader.GetOrdinal("Matricula");
                    int NomeOrdinal = Reader.GetOrdinal("AlunoNome");
                    int DataOrdinal = Reader.GetOrdinal("DataNascimento");
                    int SexoOrdinal = Reader.GetOrdinal("Sexo");
                    int CPFOrdinal = Reader.GetOrdinal("CPF");

                    AlunoModel aluno = new()
                    {
                        Matricula = Reader.GetInt32(MatriculaOrdinal),
                        Nome = Reader.GetString(NomeOrdinal),
                        DataNascimento = DateOnly.FromDateTime(Reader.GetDateTime(DataOrdinal)),
                        Sexo = (SexoEnum)Reader.GetInt32(SexoOrdinal),
                        CPF = Reader.GetString(CPFOrdinal)
                    };
                    ListaAluno.Add(aluno);
                }
                return ListaAluno;
            }
            finally
            {

                FirebirdConnection.CloseConnection(fbConnection);
            }
        }
    }
}
