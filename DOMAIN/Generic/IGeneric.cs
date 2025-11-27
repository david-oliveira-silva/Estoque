namespace DOMAIN.Generic
{
    public interface IGeneric<T>
    {
        void Cadastrar(T Entity);
        void Editar(T Entity);
        void Deletar(T Entity);
        List<T> Listar();
    }
}
