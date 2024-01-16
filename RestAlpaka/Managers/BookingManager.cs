using RestAlpaka.Model;

namespace RestAlpaka.Managers
{
    public class BookingManager : BaseDBContext<Bookings>
    {
        
        

            public BookingManager(AlpakaDbContext context) : base(context)
            {

            }

            // Here you can override any virtual methods from BaseDBContext<T> if needed.
            // You can also add any additional methods specific to AlpakaManager.
        
    }
}
