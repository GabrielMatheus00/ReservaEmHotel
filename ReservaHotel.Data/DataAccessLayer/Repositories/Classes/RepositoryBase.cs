using Microsoft.EntityFrameworkCore;
using ReservaHotel.Data.DataAccessLayer.Repositories.Interfaces;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.DataAccessLayer.Repositories.Classes
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : BaseEntity
    {
        private HotelDbContext _dbContext;
        public RepositoryBase(HotelDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public void Adicionar(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Atualizar(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public T? BuscarPorId(Guid id)
        {
            return _dbContext.Set<T>().FirstOrDefault(t=> t.Ativo && id == t.Id);
        }

        public IEnumerable<T> BuscarTodos(Expression<Func<T, bool>> where)
        {
            return _dbContext.Set<T>().Where(where);
        }
        public T? BuscarUm(Expression<Func<T, bool>> where) => _dbContext.Set<T>().FirstOrDefault(where);
        public bool Remover(Guid id)
        {
            var entity = BuscarPorId(id);
            if(entity != null)
            {
                entity.Ativo = false;
                return true;
            }    
            return false;
        }
    }
}
