using RestAlpaka.Model;

namespace RestAlpaka.Managers
{
    public class PaymentsManager : BaseDBContext<Payments>
    {
        public PaymentsManager(AlpakaDbContext context) : base(context) { }
    
    }
}
