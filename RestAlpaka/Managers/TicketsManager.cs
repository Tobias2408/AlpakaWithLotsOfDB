using RestAlpaka.Model;

namespace RestAlpaka.Managers
{
    public class TicketsManager : BaseDBContext<Tickets>
    {
        public TicketsManager(AlpakaDbContext context) : base(context) { }
    
    }
}
