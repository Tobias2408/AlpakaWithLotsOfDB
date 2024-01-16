using RestAlpaka.Model;

namespace RestAlpaka.Managers
{
    public class UsresManager : BaseDBContext<Users>
    {
        public UsresManager(AlpakaDbContext context) : base(context) { }
    
    }
}
