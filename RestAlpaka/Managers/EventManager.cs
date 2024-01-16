using RestAlpaka.Model;

namespace RestAlpaka.Managers
{
    public class EventManager : BaseDBContext<Event>
    {
        public EventManager(AlpakaDbContext context) : base(context) { }

    }
}
