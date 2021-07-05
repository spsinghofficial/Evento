using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistance;

namespace Application.Activities
{
    public class Delete
    {
        public class Command:IRequest{
           public Guid Id  { get; set; }
            
        }
        public class handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public handler(DataContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);
                _context.Remove(activity);
                await _context.SaveChangesAsync();
                return Unit.Value;

            }
        }
    }
}