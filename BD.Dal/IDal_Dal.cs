using System.Collections.Generic;
namespace BD.Dal
{
    public interface IDal<T> : System.IDisposable
    {
        T Insert(T value);
        bool Update(T value);
        bool Delete(T value);
        bool Delete(object id);
        T FindByCodigoBarrasORNome(object codigoBarras, object nome);
        IEnumerable<T> List();
    }
}