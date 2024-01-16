using RestAlpaka.Model;

namespace RestAlpaka.Managers
{
    public class AlpakaManager : BaseDBContext<Alpaka>
    {

        public AlpakaManager(AlpakaDbContext context) : base(context)
        {

        }

        // Here you can override any virtual methods from BaseDBContext<T> if needed.
        // You can also add any additional methods specific to AlpakaManager.
    }
}