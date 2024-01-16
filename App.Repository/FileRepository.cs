using App.Domain.Interfaces.Repositories;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace App.Repository
{
    public class FileRepository : BaseDbContext, IFileRepository
    {
        public DbSet<Domain.Entities.File> File { get; set; }

        public Domain.Entities.File Find(Guid id)
        {
            using (var context = new FileRepository())
            {
      
                var entity = context.File.Find(id);
                return entity = entity ?? throw new Exception("Objeto não encontrado [ARQUIVO]");
            }
        }

        public IEnumerable<Domain.Entities.File> FindAll(Func<Domain.Entities.File, bool>? predicate = null)
        {
            using (var context = new FileRepository())
            {
                if (predicate != null)
                {
                    return context.File.Where(predicate).ToList();
                }

                return context.File.ToList();
            }
        }

        public void Create(Domain.Entities.File entity)
        {
            using var context = new FileRepository();
            context.Database.EnsureCreated();
            context.File.Add(entity);
            context.SaveChanges();
        }

        public async Task CreateAsync(Domain.Entities.File entity)
        {
            using var context = new FileRepository();
            context.Database.EnsureCreated();
            context.File.Add(entity);
            await context.SaveChangesAsync();
        }

        public Domain.Entities.File Update(Guid id, Domain.Entities.File entity)
        {
            using var context = new FileRepository();
            var queue = context.File.Find(id) ?? throw new Exception("Objeto não encontrado [FILA]");
            context.Entry(queue).CurrentValues.SetValues(entity);
            context.SaveChanges();
            return entity;
        }


        public void CreateMass(IList<Domain.Entities.File> entities)
        {
            using var context = new FileRepository();
            context.Database.EnsureCreated();
            context.File.AddRange(entities);
            context.SaveChanges();
        }

    }
}
