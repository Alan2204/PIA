using Microsoft.EntityFrameworkCore;

namespace WebApiCitasMedicas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
