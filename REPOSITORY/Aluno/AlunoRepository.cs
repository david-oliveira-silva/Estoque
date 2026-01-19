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
        public void Editar(AlunoModel alunoModel)
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                string queryUpdate = "UPDATE ALUNO SET ALUNONOME = @ALUNONOME,DATANASCIMENTO = @DATANASCIMENTO, SEXO = @SEXO,CPF = @CPF WHERE MATRICULA = @MATRICULA";

                using FbCommand cmdUpdate = new(queryUpdate, fbConnection);
                cmdUpdate.Parameters.AddWithValue(@"ALUNONOME", alunoModel.Nome);
                cmdUpdate.Parameters.AddWithValue(@"DATANASCIMENTO", alunoModel.DataNascimento);
                cmdUpdate.Parameters.AddWithValue(@"SEXO", alunoModel.Sexo);
                cmdUpdate.Parameters.AddWithValue(@"CPF", alunoModel.CPF);
                cmdUpdate.Parameters.AddWithValue(@"MATRICULA",alunoModel.Matricula);
                cmdUpdate.ExecuteNonQuery();
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

        public List<AlunoModel> Listar()
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                List<AlunoModel> ListaAluno = [];
                string QuerySelect = "SELECT * FROM ALUNO";

                using FbCommand cmdSelect = new(QuerySelect, fbConnection);
                using var Reader = cmdSelect.ExecuteReader();

                int MatriculaOrdinal = Reader.GetOrdinal("Matricula");
                int NomeOrdinal = Reader.GetOrdinal("AlunoNome");
                int DataOrdinal = Reader.GetOrdinal("DataNascimento");
                int SexoOrdinal = Reader.GetOrdinal("Sexo");
                int CPFOrdinal = Reader.GetOrdinal("CPF");
                while (Reader.Read())
                {
                    AlunoModel aluno = new()
                    {
                        Matricula = Reader.IsDBNull(MatriculaOrdinal) ? 0 : Reader.GetInt32(MatriculaOrdinal),
                        Nome = Reader.IsDBNull(NomeOrdinal) ? null : Reader.GetString(NomeOrdinal),
                        DataNascimento = Reader.IsDBNull(DataOrdinal) ? null : DateOnly.FromDateTime(Reader.GetDateTime(DataOrdinal)),
                        Sexo = Reader.IsDBNull(SexoOrdinal) ? null : (SexoEnum)Reader.GetInt32(SexoOrdinal),
                        CPF = Reader.IsDBNull(CPFOrdinal) ? null : Reader.GetString(CPFOrdinal)
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
