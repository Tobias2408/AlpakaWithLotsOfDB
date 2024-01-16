using RestAlpaka.Model;

namespace RestAlpaka.Managers
{
    public class ReviewsManager : BaseDBContext<Reviews>
    {
        public ReviewsManager(AlpakaDbContext context) : base(context) { }
    
    }
}
