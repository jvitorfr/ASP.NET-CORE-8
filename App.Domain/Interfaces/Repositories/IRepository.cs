using System.Collections;
using System.Collections.Generic;

namespace App.Domain.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        public T Find(Guid id);

        public IEnumerable<T> FindAll(Func<T, bool>? predicate = null);

        public void Create(T entity);

        public T Update(Guid id, T entity);

        public void CreateMass(IList<T> entities);

        public Task CreateAsync(T entity);

    }
}
