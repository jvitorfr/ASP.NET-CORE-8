using App.Domain.Interfaces.Repositories;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repository
{
    public class QueueRepository : BaseDbContext, IQueueRepository
    {
        public DbSet<Queue> Queue { get; set; }

        public Queue Find(Guid id)
        {
            using (var context = new QueueRepository())
            {
                context.Database.EnsureCreated();
                var entity = context.Queue.Find(id);
                return entity = entity ?? throw new Exception("Objeto não encontrado [FILA]");
            }
        }

        public IEnumerable<Queue> FindAll(Func<Queue, bool>? predicate = null)
        {
            using (var context = new QueueRepository())
            {
                if (predicate != null)
                {
                    return context.Queue.Where(predicate).ToList();
                }

                return context.Queue.ToList();
            }
        }

        public void Create(Queue entity)
        {
            using var context = new QueueRepository();
            context.Database.EnsureCreated();
            context.Queue.Add(entity);
            context.SaveChanges();
        }

        public Queue Update(Guid id, Queue entity)
        {
            using var context = new QueueRepository();
            var queue = context.Queue.Find(id) ?? throw new Exception("Objeto não encontrado [FILA]");
            context.Entry(queue).CurrentValues.SetValues(entity);
            context.SaveChanges();
            return entity;
        }


        public void CreateMass(IList<Queue> entities)
        {
            using var context = new QueueRepository();
            context.Database.EnsureCreated();
            context.Queue.AddRange(entities);
            context.SaveChanges();
        }


        public void StartDb()
        {
            using (var context = new QueueRepository())
            {
                context.Database.EnsureCreated();

                var queues = new List<Queue>
                {
                new() {
                    Id = Guid.NewGuid(),
                    ConsumedBy ="SYSTEM",
                    Label ="INIT DB",
                    From = "SYSTEM",
                    Message = "START DB",
                    Requested = DateTime.Now,
                    Sended = DateTime.Now
                },
                new() {
                    Id = Guid.NewGuid(),
                    ConsumedBy ="SYSTEM 2",
                    Label ="INIT DB 2 ",
                    From = "SYSTEM 2",
                    Message = "START DB 2",
                    Requested = DateTime.Now,
                    Sended = DateTime.Now
                },

                };

                context.Queue.AddRange(queues);
                context.SaveChanges();
            }

            using (var context = new QueueRepository())
            {
                var list = context.Queue
                    .ToList();

                foreach (var queue in list)
                {
                    Console.WriteLine("Consumido por: " + queue.ConsumedBy + ":" + queue.Requested);

                }
            }
        }

        public async Task CreateAsync(Queue entity)
        {
            using var context = new QueueRepository();
            context.Database.EnsureCreated();
            context.Queue.Add(entity);
            await context.SaveChangesAsync();
        }
    }
}
