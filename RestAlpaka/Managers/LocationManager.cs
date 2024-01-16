using RestAlpaka.Model;

namespace RestAlpaka.Managers
{
    public class LocationManager : BaseDBContext<Location>
    {
        public LocationManager(AlpakaDbContext context) : base(context) { }
    

    }
}
