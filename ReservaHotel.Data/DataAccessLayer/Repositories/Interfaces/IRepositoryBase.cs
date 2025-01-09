using ReservaHotel.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.DataAccessLayer.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        void Adicionar(T entity);
        bool Remover (Guid id);
        void Atualizar(T entity);
        T? BuscarPorId(Guid id);
        IEnumerable<T> BuscarTodos(Expression<Func<T, bool>> where);
        T? BuscarUm(Expression<Func<T, bool>> where);
    }
}
