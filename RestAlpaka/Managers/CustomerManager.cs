using RestAlpaka.Model;

namespace RestAlpaka.Managers
{
    public class CustomerManager : BaseDBContext<Customers>
    {
        public CustomerManager(AlpakaDbContext context) : base(context) { } 
    }
}
