using API.Contexts;
using API.Models;

namespace API.Repositories.Data
{
    public class ProfillingRepository : GeneralRepository<int, Profilling>
    {
        private readonly MyContext context;

        public ProfillingRepository(MyContext context) : base(context)
        {
            this.context = context;
        }
    }
}
